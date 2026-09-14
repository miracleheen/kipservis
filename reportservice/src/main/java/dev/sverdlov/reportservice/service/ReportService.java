package dev.sverdlov.reportservice.service;

import dev.sverdlov.reportservice.dto.CreateReportRequestDto;
import dev.sverdlov.reportservice.dto.ReportInfoResponseDto;
import dev.sverdlov.reportservice.dto.ReportResultDto;
import dev.sverdlov.reportservice.entity.ReportRequest;
import dev.sverdlov.reportservice.repository.ReportRequestRepository;
import jakarta.annotation.PostConstruct;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.scheduling.concurrent.ThreadPoolTaskScheduler;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.Duration;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.List;
import java.util.Optional;
import java.util.UUID;

@Service
@Transactional(readOnly = true)
public class ReportService {
    private static final Logger log = LoggerFactory.getLogger(ReportService.class);

    private final ReportRequestRepository reportRequestRepository;
    private final ReportResultCalculator reportResultCalculator;
    private final ThreadPoolTaskScheduler taskScheduler;
    private final long defaultTimeoutMs;

    public ReportService(ReportRequestRepository reportRequestRepository,
                         ReportResultCalculator reportResultCalculator,
                         ThreadPoolTaskScheduler taskScheduler,
                         @Value("${report.processing.timeout-ms}") long defaultTimeoutMs) {
        this.reportRequestRepository = reportRequestRepository;
        this.reportResultCalculator = reportResultCalculator;
        this.taskScheduler = taskScheduler;
        this.defaultTimeoutMs = defaultTimeoutMs;
    }

    /**
     * Восстанавливает запланированные задачи после рестарта JVM: сам
     * ThreadPoolTaskScheduler хранит расписание только в мемори, поэтому
     * при рестарте оно теряется, а completed_at IS NULL в БД -нет
     */
    @PostConstruct
    public void rescheduleUnfinishedRequestsOnStartup() {
        List<ReportRequest> unfinished = reportRequestRepository.findByCompletedAtIsNull();
        unfinished.forEach(this::scheduleCompletion);

        log.info("Rescheduled {} unfinished report request(s) after startup",
                unfinished.size());
    }

    @Transactional
    public UUID createReport(CreateReportRequestDto request) {
        UUID id = UUID.randomUUID();

        ReportRequest reportRequest = new ReportRequest(
                id,
                request.getUserId(),
                request.getPeriodFrom(),
                request.getPeriodTo(),
                LocalDateTime.now(),
                defaultTimeoutMs
        );

        reportRequestRepository.save(reportRequest);
        scheduleCompletion(reportRequest);
        return id;
    }

    /**
     * Планирует асинхронное вычисление результата на момент createdAt + timeoutMs.
     * Если этот момент уже в прошлом (н-р после рестарта) -выполнится немедленно
     */
    private void scheduleCompletion(ReportRequest reportRequest) {
        Instant completionInstant = reportRequest.getCreatedAt()
                .atZone(ZoneId.systemDefault())
                .toInstant()
                .plusMillis(reportRequest.getTimeoutMs());

        taskScheduler.schedule(
                () -> reportResultCalculator.computeAndStore(reportRequest.getId()),
                completionInstant
        );
    }

    public Optional<ReportInfoResponseDto> getReportInfo(UUID query) {
        return reportRequestRepository.findById(query)
                .map(this::toResponse);
    }

    private ReportInfoResponseDto toResponse(ReportRequest reportRequest) {
        int percent = calculatePercent(reportRequest);
        ReportResultDto result = (reportRequest.getResultCountSignIn() != null)
                ? new ReportResultDto(
                reportRequest.getUserId().toString(),
                String.valueOf(reportRequest.getResultCountSignIn())) : null;

        return new ReportInfoResponseDto(reportRequest.getId(), percent, result);
    }

    private int calculatePercent(ReportRequest reportRequest) {
        long elapsedMs = Duration.between(reportRequest.getCreatedAt(), LocalDateTime.now()).toMillis();
        long timeoutMs = reportRequest.getTimeoutMs();

        long percent = (timeoutMs <= 0) ? 100 : elapsedMs * 100 / timeoutMs;
        return Math.clamp(percent, 0, 100);
    }
}