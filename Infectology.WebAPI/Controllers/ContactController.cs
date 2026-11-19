using Infectology.Library.DTOs.Requests.Users;
using Microsoft.AspNetCore.Mvc;

namespace Infectology.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    [HttpPost]
    public IActionResult SubmitMessage([FromBody] ContactMessageRequest dto)
    {
        if (!dto.GdprAccepted)
        {
            return BadRequest(new { message = "GDPR consent is required." });
        }

        return Ok();
    }
}