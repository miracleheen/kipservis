package dev.sverdlov.reportservice.controller;

import dev.sverdlov.reportservice.dto.CreateReportRequestDto;
import dev.sverdlov.reportservice.dto.CreateReportResponseDto;
import dev.sverdlov.reportservice.dto.ReportInfoResponseDto;
import dev.sverdlov.reportservice.service.ReportService;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.server.ResponseStatusException;

import java.util.UUID;

@RestController
@RequestMapping("/report")
public class ReportController {
    private final ReportService reportService;

    @Autowired
    public ReportController(ReportService reportService) {
        this.reportService = reportService;
    }

    @PostMapping("/user_statistics")
    public ResponseEntity<CreateReportResponseDto> createUserStatisticsReport(
            @Valid @RequestBody CreateReportRequestDto request
    ) {
        UUID query = reportService.createReport(request);

        return ResponseEntity.ok(new CreateReportResponseDto(query));
    }

    @GetMapping("/info")
    public ResponseEntity<ReportInfoResponseDto> getReportInfo(@RequestParam("query") UUID query) {
        ReportInfoResponseDto response = reportService.getReportInfo(query)
                .orElseThrow(() -> new ResponseStatusException(
                        HttpStatus.NOT_FOUND, "Report request not found: " + query));
        return ResponseEntity.ok(response);
    }
}