using Infectology.Database.Contexts;
using Infectology.Library.DTOs.Responses.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infectology.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeController(UserContext context) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
        var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (idClaim == null || !uint.TryParse(idClaim, out var userId))
        {
            return Unauthorized();
        }

        var user = await context.Users.Include(u => u.PersonalContact).FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return Unauthorized();
        }

        var response = new LoginResponse(
            Token: string.Empty,
            Email: user.PersonalContact.EmailAddress,
            Role: user.Role.ToString(),
            FirstName: user.FirstName,
            LastName: user.LastName
        );

        return Ok(response);
    }
}