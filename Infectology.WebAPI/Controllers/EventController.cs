using Infectology.Database.Contexts;
using Infectology.Library.DTOs.Requests.Content;
using Infectology.Library.DTOs.Responses.Content;
using Infectology.Library.DTOs.Responses.Shared;
using Infectology.Library.Entities.Content;
using Infectology.Library.Entities.Users;
using Infectology.WebAPI.Attributes;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infectology.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController(ContentContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResponse<EventResponse>>> GetEvents(int skip = 0, int take = int.MaxValue)
    {
        var events = await context.Events.OrderByDescending(e => e.Start).Skip(skip).Take(take).ProjectToType<EventResponse>().ToArrayAsync();
        var totalCount = await context.Events.CountAsync();
        var response = new PagedResponse<EventResponse>(events, totalCount);
        return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<EventResponse>> GetEvent(uint id)
    {
        var @event = await context.Events.FindAsync(id);

        if (@event == null) return NotFound();

        var response = @event.Adapt<EventResponse>();

        return Ok(response);
    }


    [HttpPut("{id}"), AuthorizeRoles(Role.Editor, Role.Admin)]
    public async Task<IActionResult> PutEvent(uint id, EventUpdateRequest eventUpdateRequest)
    {
        if (id != eventUpdateRequest.Id)
        {
            return BadRequest();
        }

        var @event = await context.Events.FindAsync(id);
        if (@event == null)
        {
            return NotFound();
        }

        @event = eventUpdateRequest.Adapt(@event);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EventExists(id))
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


    [HttpPost, AuthorizeRoles(Role.Editor, Role.Admin)]
    public async Task<ActionResult<EventResponse>> PostEvent(EventCreateRequest eventCreateRequest)
    {
        var authorIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (authorIdClaim == null || !uint.TryParse(authorIdClaim, out var authorId))
        {
            return Unauthorized();
        }

        var @event = new Event
        {
            Name = eventCreateRequest.Name,
            Start = eventCreateRequest.Start,
            End = eventCreateRequest.End,
            Place = eventCreateRequest.Place,
            Link = eventCreateRequest.Link,
            AuthorId = authorId
        };

        context.Events.Add(@event);
        await context.SaveChangesAsync();

        var response = @event.Adapt<EventResponse>();

        return CreatedAtAction("GetEvent", new { id = @event.Id }, response);
    }


    [HttpDelete("{id}"), AuthorizeRoles(Role.Editor, Role.Admin)]
    public async Task<IActionResult> DeleteEvent(uint id)
    {
        var @event = await context.Events.FindAsync(id);
        if (@event == null)
        {
            return NotFound();
        }

        context.Events.Remove(@event);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool EventExists(uint id)
    {
        return context.Events.Any(e => e.Id == id);
    }
}