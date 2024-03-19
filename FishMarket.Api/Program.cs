using FishMarket.Api.Data;
using FishMarket.Api.Extensions;
using FishMarket.Api.Middlewares;
using FishMarket.Api.Services.Contracts;
using FishMarket.Api.Services.Implementations;
using FishMarket.Api.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection(nameof(JWTSettings)));
builder.Services.Configure<Smtp>(builder.Configuration.GetSection(nameof(Smtp)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MarketDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FishMarket")));

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

var jwtSettings = builder.Configuration.GetOptions<JWTSettings>();
builder.Services.RegisterJWTAuthentication(jwtSettings);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<AuthenticationMiddlewareForUserState>();
app.UseMiddleware<AuthenticationMiddlewareForRefreshToken>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
