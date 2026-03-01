using AutoMapper;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using SchedFrex.Api.Extensions;
using SchedFrex.Application.Abstractions;
using SchedFrex.Application.Authorization;
using SchedFrex.Application.Profiles;
using SchedFrex.Application.Services;
using SchedFrex.Core.Abstractions;
using SchedFrex.DataAccess;
using SchedFrex.DataAccess.Profiles;
using SchedFrex.DataAccess.Repositories;

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

// Сервисы и репозитории

builder.Services.AddScoped<IProblemService, ProblemService>();
builder.Services.AddScoped<IProblemsRepository, ProblemsRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<ICalendarRepository, CalendarRepository>();

// Хелперы
builder.Services.AddScoped<JwtProvider>();

// Методы расширений
builder.Services.AddJwtAuthentication(builder.Configuration);

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
    Secure =  securePolicy
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();