using Infectology.Database.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infectology.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogoutController(UserContext context) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var token = Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
        var session = await context.Sessions.FirstOrDefaultAsync(s => s.Token == token);

        if (session != null)
        {
            context.Sessions.Remove(session);
            await context.SaveChangesAsync();
        }

        return Ok();
    }
}