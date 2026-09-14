package dev.sverdlov.reportservice.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.UUID;

public class ReportInfoResponseDto {
    @JsonProperty("query")
    private final UUID query;

    @JsonProperty("percent")
    private final int percent;

    @JsonProperty("result")
    private final ReportResultDto result;

    public ReportInfoResponseDto(UUID query, int percent, ReportResultDto result) {
        this.query = query;
        this.percent = percent;
        this.result = result;
    }

    public UUID getQuery() {
        return query;
    }

    public int getPercent() {
        return percent;
    }

    public ReportResultDto getResult() {
        return result;
    }
}