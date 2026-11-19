using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infectology.Database.Contexts;
using Infectology.Library.DTOs.Requests.Users;
using Infectology.Library.DTOs.Responses.Users;
using Infectology.Library.Entities.Users;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infectology.WebAPI.Controllers;

/// <summary>
/// Handles user authentication and issues JSON Web Tokens for successfully authenticated users.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LoginController(IConfiguration config, UserContext context) : ControllerBase
{
    /// <summary>
    /// Authenticates a user by email and password and returns a JWT access token on success.
    /// </summary>
    /// <param name="request">The login request containing the user's email and password.</param>
    /// <returns>
    /// A <see cref="LoginResponse"/> containing the access token and user details if authentication succeeds;
    /// otherwise, an unauthorized result.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        var user = await context.Users
            .Include(existingUser => existingUser.PersonalContact)
            .FirstOrDefaultAsync(existingUser => existingUser.PersonalContact.EmailAddress == request.Email);

        if (user == null || !Argon2.Verify(user.PasswordHash, request.Password))
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        var token = GenerateToken(user.Id, request.Email, user.Role);

        var session = new UserSession
        {
            Token = token,
            UserId = user.Id,
            User = user,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10)
        };

        context.Sessions.Add(session);
        await context.SaveChangesAsync();

        return Ok(new LoginResponse(
                Token: token,
                Email: request.Email,
                Role: user.Role.ToString(),
                FirstName: user.FirstName,
                LastName: user.LastName
            ));
    }

    /// <summary>
    /// Generates a signed JWT access token containing the user's identifier, email, and role claims.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="role">The role assigned to the user.</param>
    /// <returns>A serialized JWT access token string.</returns>
    private string GenerateToken(uint id, string email, Role role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}