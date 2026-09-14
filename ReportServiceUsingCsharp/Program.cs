using Microsoft.EntityFrameworkCore;
using ReportServiceUsingCsharp.Data;
using ReportServiceUsingCsharp.Repositories;
using ReportServiceUsingCsharp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ReportDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IReportRequestRepository, ReportRequestRepository>();
builder.Services.AddScoped<IUserSignInRepository, UserSignInRepository>();
builder.Services.AddScoped<ReportResultCalculator>();
builder.Services.AddScoped<ReportService>();

builder.Services.AddSingleton<ReportScheduler>();
builder.Services.AddHostedService<ReportRescheduleHostedService>();

var app = builder.Build();

app.MapControllers();

app.Run();