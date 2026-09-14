package dev.sverdlov.reportservice.repository;

import dev.sverdlov.reportservice.entity.ReportRequest;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.UUID;

public interface ReportRequestRepository extends JpaRepository<ReportRequest, UUID> {
    List<ReportRequest> findByCompletedAtIsNull();
}