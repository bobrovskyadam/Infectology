using Humanizer;
using Infectology.Database.Contexts;
using Infectology.Library.DTOs.Requests.Content;
using Infectology.Library.DTOs.Responses.Content;
using Infectology.Library.Entities.Content;
using Infectology.Library.Entities.Users;
using Infectology.WebAPI.Attributes;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infectology.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController(ContentContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentResponse>>> GetDocuments()
    {
        var documents = await context.Documents.ProjectToType<DocumentResponse>().ToListAsync();

        foreach (var document in documents)
        {
            try
            {
                var fileInfo = new FileInfo($"documents/{document.FileName}");
                document.Size = fileInfo.Length;
                document.DateTimeCreated = fileInfo.CreationTimeUtc;
                document.DateTimeEdited = fileInfo.LastWriteTimeUtc;
            }
            catch (Exception)
            {
                documents.Remove(document);
            }           
        }

        return Ok(documents);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentResponse>> GetDocument(uint id)
    {
        var document = await context.Documents.FindAsync(id);

        if (document == null) return NotFound();

        var response = document.Adapt<DocumentResponse>();
        try
        {
            var fileInfo = new FileInfo($"documents/{document.FileName}");
            response.Size = fileInfo.Length;
            response.DateTimeCreated = fileInfo.CreationTimeUtc;
            response.DateTimeEdited = fileInfo.LastWriteTimeUtc;

            return Ok(response);
        }
        catch (Exception)
        {
            return BadRequest();
        } 
    }

    [HttpPut("{id}"), AuthorizeRoles(Role.Editor, Role.Admin)]
    public async Task<IActionResult> PutDocument(uint id, [FromBody] DocumentUpdateRequest documentUpdateRequest)
    {
        if (id != documentUpdateRequest.Id)
        {
            return BadRequest();
        }

        var document = await context.Documents.FindAsync(id);
        if (document == null)
        {
            return NotFound();
        }

        document.Name = documentUpdateRequest.Name;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DocumentExists(id))
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
    public async Task<ActionResult<DocumentResponse>> PostDocument([FromBody] DocumentCreateRequest documentCreateRequest)
    {
        var authorIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (authorIdClaim == null || !uint.TryParse(authorIdClaim, out var authorId))
        {
            return Unauthorized();
        }

        var document = documentCreateRequest.Adapt<Document>();
        document.AuthorId = authorId;
        context.Documents.Add(document);
        await context.SaveChangesAsync();

        var response = document.Adapt<DocumentResponse>();

        return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, response);
    }

    [HttpDelete("{id}"), AuthorizeRoles(Role.Editor, Role.Admin)]
    public async Task<IActionResult> DeleteDocument(uint id)
    {
        var document = await context.Documents.FindAsync(id);                               
        if (document == null)
        {
            return NotFound();
        }

        context.Documents.Remove(document);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool DocumentExists(uint id)
    {
        return context.Documents.Any(e => e.Id == id);
    }
}
