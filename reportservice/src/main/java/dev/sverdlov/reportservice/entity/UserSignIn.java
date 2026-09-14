package dev.sverdlov.reportservice.entity;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

import java.time.LocalDateTime;
import java.util.UUID;


/**
 * Таблица-источник для расчёта count_sign_in.
 * В тз она не описана: сама структура отчёта требует реальных данных о входах
 * пользователя, поэтому источник придуман и засеян мок-данными под пример из тз-шки (сама миграция 003).
 */
@Entity
@Table(name = "user_sign_in")
public class UserSignIn {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", nullable = false, updatable = false)
    private Long id;

    @Column(name = "user_id", nullable = false, updatable = false)
    private UUID userId;

    @Column(name = "signed_in_at", nullable = false, updatable = false)
    private LocalDateTime signedInAt;

    protected UserSignIn() {
    }

    public UserSignIn(UUID userId, LocalDateTime signedInAt) {
        this.userId = userId;
        this.signedInAt = signedInAt;
    }

    public Long getId() {
        return id;
    }

    public UUID getUserId() {
        return userId;
    }

    public LocalDateTime getSignedInAt() {
        return signedInAt;
    }
}