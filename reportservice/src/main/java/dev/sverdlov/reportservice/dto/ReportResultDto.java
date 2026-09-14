package dev.sverdlov.reportservice.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

public class ReportResultDto {
    @JsonProperty("user_id")
    private final String userId;

    @JsonProperty("count_sign_in")
    private final String countSignIn;

    public ReportResultDto(String userId, String countSignIn) {
        this.userId = userId;
        this.countSignIn = countSignIn;
    }

    public String getUserId() {
        return userId;
    }

    public String getCountSignIn() {
        return countSignIn;
    }
}