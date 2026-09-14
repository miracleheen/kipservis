package dev.sverdlov.reportservice.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.UUID;

public class CreateReportResponseDto {
    @JsonProperty("query")
    private final UUID query;

    public CreateReportResponseDto(UUID query) {
        this.query = query;
    }

    public UUID getQuery() {
        return query;
    }
}