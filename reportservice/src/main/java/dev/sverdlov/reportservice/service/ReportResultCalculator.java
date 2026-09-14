package dev.sverdlov.reportservice.service;

import dev.sverdlov.reportservice.repository.ReportRequestRepository;
import dev.sverdlov.reportservice.repository.UserSignInRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;
import java.util.UUID;

/**
 * Вынесен в отдельный бин, а не метод внутри ReportService, чтобы @Transactional
 * применялся через прокси. Вызов метода из своего же класса
 * прокси не перехватывает (транзакция не сработает)
 */
@Service
public class ReportResultCalculator {
    private final ReportRequestRepository reportRequestRepository;
    private final UserSignInRepository userSignInRepository;

    @Autowired
    public ReportResultCalculator(ReportRequestRepository reportRequestRepository,
                                  UserSignInRepository userSignInRepository) {
        this.reportRequestRepository = reportRequestRepository;
        this.userSignInRepository = userSignInRepository;
    }

    @Transactional
    public void computeAndStore(UUID id) {
        reportRequestRepository.findById(id).ifPresent(reportRequest -> {
            if (reportRequest.getResultCountSignIn() != null) {
                return;
            }

            long count = userSignInRepository.countByUserIdAndSignedInAtBetween(
                    reportRequest.getUserId(),
                    reportRequest.getPeriodFrom(),
                    reportRequest.getPeriodTo()
            );

            reportRequest.setResultCountSignIn((int) count);
            reportRequest.setCompletedAt(LocalDateTime.now());
            reportRequestRepository.save(reportRequest);

        });
    }
}