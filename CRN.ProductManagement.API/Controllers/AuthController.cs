using CRN.ProductManagement.Application.DTOs;
using CRN.ProductManagement.Application.Services;
using CRN.ProductManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRN.ProductManagement.API.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        if (request.Username != "admin" ||
            request.Password != "Admin@123")
        {
            return Unauthorized(new
            {
                message = "Invalid username or password."
            });
        }

        var user = new User
        {
            Id = 1,
            Username = "admin",
            Role = "Admin"
        };

        var token =
            _tokenService.GenerateAccessToken(user);

        return Ok(new
        {
            accessToken = token
        });
    }
}