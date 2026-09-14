using Microsoft.EntityFrameworkCore;
using ReportServiceUsingCsharp.Entities;

namespace ReportServiceUsingCsharp.Data;

public class ReportDbContext : DbContext
{
    public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
    {
    }

    public DbSet<ReportRequest> ReportRequests => Set<ReportRequest>();
    public DbSet<UserSignIn> UserSignIns => Set<UserSignIn>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReportRequest>(entity =>
        {
            entity.ToTable("report_request");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.PeriodFrom).HasColumnName("period_from").HasColumnType("timestamp without time zone");
            entity.Property(e => e.PeriodTo).HasColumnName("period_to").HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone");
            entity.Property(e => e.TimeoutMs).HasColumnName("timeout_ms");
            entity.Property(e => e.ResultCountSignIn).HasColumnName("result_count_sign_in");
            entity.Property(e => e.CompletedAt).HasColumnName("completed_at").HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<UserSignIn>(entity =>
        {
            entity.ToTable("user_sign_in");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.SignedInAt).HasColumnName("signed_in_at").HasColumnType("timestamp without time zone");
        });
    }
}