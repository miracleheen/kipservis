package dev.sverdlov.reportservice.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.scheduling.concurrent.ThreadPoolTaskScheduler;

/**
 * Реализует асинхронную обработку: ThreadPoolTaskScheduler
 * планирует расчёт результата в фоне на createdAt + timeoutMs,
 * независимо от запроса (будет он или нет GET)
 */
@Configuration
public class SchedulingConfig {
    @Bean
    public ThreadPoolTaskScheduler reportTaskScheduler() {
        ThreadPoolTaskScheduler scheduler = new ThreadPoolTaskScheduler();
        scheduler.setPoolSize(10);
        scheduler.setThreadNamePrefix("report-async-");
        scheduler.initialize();

        return scheduler;
    }
}