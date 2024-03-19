using FishMarket.Api.Data;
using FishMarket.Api.Data.Entities;
using FishMarket.Api.Extensions;
using FishMarket.Api.Mapper;
using FishMarket.Api.Models.Requests;
using FishMarket.Api.Models.Responses;
using FishMarket.Api.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FishMarket.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(IAuthenticationService authenticationService,
    MarketDbContext marketDbContext,
    IEmailService emailService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterAsync(LoginRequest request)
    {
        var isRegisteredUser = await marketDbContext.Users.AsNoTracking().AnyAsync(x => x.Email == request.Email);
        if (isRegisteredUser)
        {
            return Conflict(new ProblemDetails()
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Conflict",
                Detail = "There is already an registered user with the given email"
            });
        }

        var userToRegister = new User()
        {
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            IsActive = false,
            Otp = Random.Shared.Next(100_000, 999_999).ToString()
        };

        await marketDbContext.AddAsync(userToRegister);

        await Task.WhenAll(
            marketDbContext.SaveChangesAsync(),
            emailService.SendAsync(userToRegister.Otp, userToRegister.Email));

        return Ok();
    }

    [AllowAnonymous]
    [HttpPatch("otp")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmOtpAsync(OtpRequest request)
    {
        var user = await marketDbContext.Users.FirstOrDefaultAsync(x =>
            x.Email == request.Email
            && x.Otp == request.Otp);

        if (user is null
            && !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return BadRequest(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "BadRequest",
                Detail = "Wrong credentials"
            });
        }

        user.IsActive = true;
        await marketDbContext.SaveChangesAsync();

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResponse))]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var user = await marketDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user is null
            || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return BadRequest(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "BadRequest",
                Detail = "Wrong credentials"
            });
        }

        if (!user.IsActive)
        {
            return BadRequest(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "BadRequest",
                Detail = "Email is not confirmed yet"
            });
        }

        var authentication = authenticationService.GenerateToken(request.Email);
        var response = authentication.MapToResponse();
        return Ok(response);
    }

    [HttpGet("refresh")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Refresh()
    {
        var isRefreshToken = HttpContext.User.IsRefreshToken();
        if (!isRefreshToken)
        {
            return Unauthorized("Invalid token");
        }

        var email = HttpContext.User.GetEmail();
        var authentication = authenticationService.GenerateToken(email);
        var response = authentication.MapToResponse();
        return Ok(response);
    }
}
