using Infectology.Database.Contexts;
using Infectology.Library.DTOs.Responses.Users;
using Infectology.Library.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Infectology.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController(UserContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<UserProfileResponse>> GetProfile()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Unauthorized();
        }
        var doctor = await context.Users
            .OfType<Doctor>()
            .Include(d => d.PersonalContact)
            .Include(d => d.Workplace)
            .FirstOrDefaultAsync(d => d.Id.ToString() == userIdClaim);

        if (doctor == null)
        {
            return NotFound();
        }

        var response = new UserProfileResponse(
                FirstName: doctor.FirstName,
                LastName: doctor.LastName,
                Email: doctor.PersonalContact?.EmailAddress ?? "",
                Workplace: $"{doctor.Workplace?.Street}, {doctor.Workplace?.City}",
                Specialization: doctor.Focus
            );

        return Ok(response);
    }
}