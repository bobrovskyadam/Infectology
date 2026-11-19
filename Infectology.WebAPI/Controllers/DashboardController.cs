using Infectology.Database.Contexts;
using Infectology.Library.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infectology.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController(ContentContext contentContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboard()
    {
        var documents = await contentContext.Documents.CountAsync();

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var upcomingEvents = await contentContext.Events.CountAsync(e => e.Start >= today);

        var dashboard = new DashboardDto(
                NewDocuments: documents,
                UpcomingEvents: upcomingEvents,
                NewDiscussions: 0,
                Announcements: 0
            );

        return Ok(dashboard);
    }
}