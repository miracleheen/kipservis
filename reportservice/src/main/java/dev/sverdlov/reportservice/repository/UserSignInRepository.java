package dev.sverdlov.reportservice.repository;

import dev.sverdlov.reportservice.entity.UserSignIn;
import org.springframework.data.jpa.repository.JpaRepository;

import java.time.LocalDateTime;
import java.util.UUID;

public interface UserSignInRepository extends JpaRepository<UserSignIn, Long> {
    long countByUserIdAndSignedInAtBetween(
            UUID userId,
            LocalDateTime periodFrom,
            LocalDateTime periodTo
    );
}