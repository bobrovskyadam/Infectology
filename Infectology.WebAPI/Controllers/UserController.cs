using Infectology.Database.Contexts;
using Infectology.Library.DTOs.Requests.Users;
using Infectology.Library.DTOs.Responses.Users;
using Infectology.Library.Entities.Users;
using Infectology.WebAPI.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infectology.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(UserContext context) : ControllerBase
{
    [HttpGet, AuthorizeRoles(Role.Admin)]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();
        return Ok(users.Select(UserToResponse));
    }

    [HttpGet("{id}"), AuthorizeRoles(Role.Admin)]
    public async Task<ActionResult<UserResponse>> GetUser(uint id)
    {
        var user = await context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(UserToResponse(user));
    }

    [HttpPut("{id}"), AuthorizeRoles(Role.Admin)]
    public async Task<IActionResult> PutUser(uint id, UserUpdateRequest userUpdateRequest)
    {
        if (id != userUpdateRequest.Id)
        {
            return BadRequest();
        }

        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        user.Role = userUpdateRequest.Role;
        user.DateTimeVerified = userUpdateRequest.IsVerified ? user.DateTimeVerified ?? DateTime.UtcNow : null;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}"), AuthorizeRoles(Role.Admin)]
    public async Task<IActionResult> DeleteUser(uint id)
    {
        var user = await context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(uint id)
    {
        return context.Users.Any(e => e.Id == id);
    }

    private static UserResponse UserToResponse(User u) => new(
        Id: u.Id,
        FirstName: u.FirstName,
        LastName: u.LastName,
        Role: u.Role.ToString(),
        Email: u.PersonalContact?.EmailAddress ?? string.Empty,
        PhoneNumber: u.PersonalContact?.PhoneNumber,
        IsVerified: u.IsVerified,
        DateTimeCreated: u.DateTimeCreated.ToString("o")
    );
}