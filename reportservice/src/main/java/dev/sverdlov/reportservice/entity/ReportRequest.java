package dev.sverdlov.reportservice.entity;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

import java.time.LocalDateTime;
import java.util.UUID;

/**
 * Значение X (таймаут) фиксируется на момент создания запроса,
 * а не читается заново из конфига при каждом расчёте percent.
 */
@Entity
@Table(name = "report_request")
public class ReportRequest {
    @Id
    @Column(name = "id", nullable = false, updatable = false)
    private UUID id;

    @Column(name = "user_id", nullable = false, updatable = false)
    private UUID userId;

    @Column(name = "period_from", nullable = false, updatable = false)
    private LocalDateTime periodFrom;

    @Column(name = "period_to", nullable = false, updatable = false)
    private LocalDateTime periodTo;

    @Column(name = "created_at", nullable = false, updatable = false)
    private LocalDateTime createdAt;

    @Column(name = "timeout_ms", nullable = false, updatable = false)
    private Long timeoutMs;

    @Column(name = "result_count_sign_in")
    private Integer resultCountSignIn;

    @Column(name = "completed_at")
    private LocalDateTime completedAt;

    protected ReportRequest() {
    }

    public ReportRequest(
            UUID id,
            UUID userId,
            LocalDateTime periodFrom,
            LocalDateTime periodTo,
            LocalDateTime createdAt,
            Long timeoutMs
    ) {
        this.id = id;
        this.userId = userId;
        this.periodFrom = periodFrom;
        this.periodTo = periodTo;
        this.createdAt = createdAt;
        this.timeoutMs = timeoutMs;
    }

    public UUID getId() {
        return id;
    }

    public UUID getUserId() {
        return userId;
    }

    public LocalDateTime getPeriodFrom() {
        return periodFrom;
    }

    public LocalDateTime getPeriodTo() {
        return periodTo;
    }

    public LocalDateTime getCreatedAt() {
        return createdAt;
    }

    public Long getTimeoutMs() {
        return timeoutMs;
    }

    public Integer getResultCountSignIn() {
        return resultCountSignIn;
    }

    public void setResultCountSignIn(Integer resultCountSignIn) {
        this.resultCountSignIn = resultCountSignIn;
    }

    public LocalDateTime getCompletedAt() {
        return completedAt;
    }

    public void setCompletedAt(LocalDateTime completedAt) {
        this.completedAt = completedAt;
    }
}