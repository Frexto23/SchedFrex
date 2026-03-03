using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using SchedFrex.Api.Extensions;
using SchedFrex.Application.Authorization;
using SchedFrex.Application.Contracts.Validation;
using SchedFrex.Application.Logic.SchedulerStrategies;
using SchedFrex.Application.Profiles;
using SchedFrex.Application.Services;
using SchedFrex.Core.Abstractions;
using SchedFrex.DataAccess;
using SchedFrex.DataAccess.Profiles;
using SchedFrex.DataAccess.Repositories;
using FluentValidation;
using SchedFrex.Application.Abstractions.Commands;
using SchedFrex.Application.Abstractions.Queries;
using SchedFrex.Application.Contracts.Request;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры
builder.Services.AddControllers();

// Регистрируем Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SCHEDFREX API",
        Version = "v1",
        Description = "Smart schedule ASP.NET Core Web API"
    });
});

builder.Services.AddDbContext<CalendarDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CalendarDbContext)));
});

builder.Services.AddAutoMapper(cfg => { },
    typeof(CalendarProfile).Assembly,
    typeof(CalendarDtoProfile).Assembly);

// Command Repositories
builder.Services.AddScoped<IProblemsWriteRepository, ProblemsWriteRepository>();
builder.Services.AddScoped<ICalendarsWriteRepository, CalendarsWriteRepository>();
builder.Services.AddScoped<IUsersWriteRepository, UsersWriteRepository>();

// Queries Repositories
builder.Services.AddScoped<IProblemsReadRepository, ProblemsReadRepository>();
builder.Services.AddScoped<ICalendarsReadRepository, CalendarsReadRepository>();

// Services
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<ISchedulerStrategy, GreedyScheduler>();

// Хелперы
builder.Services.AddScoped<JwtProvider>();

// Методы расширений
builder.Services.AddJwtAuthentication(builder.Configuration);

// Валидаторы
builder.Services.AddValidatorsFromAssemblyContaining<CreateTimeIntervalDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Указываем, чтобы слушать порт 80
app.Urls.Add("http://0.0.0.0:80");

// Включаем Swagger (обычно только в Development)
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SchedFrex API v1");
    options.RoutePrefix = string.Empty; // Swagger на корне (http://localhost:5000/)
});
// }

app.UseHttpsRedirection();

// Cookie Policy
var securePolicy = app.Environment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = securePolicy
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();