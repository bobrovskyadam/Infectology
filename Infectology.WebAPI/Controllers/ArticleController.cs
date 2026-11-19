using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Infectology.Database.Contexts;
using Infectology.WebAPI.Attributes;
using System.Security.Claims;
using Mapster;
using Infectology.Library.Entities.Content;
using Infectology.Library.Entities.Users;
using Infectology.Library.DTOs.Responses.Content;
using Infectology.Library.DTOs.Requests.Content;

namespace Infectology.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController(ContentContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleResponse>>> GetArticles()  
    {
        var articles = await context.Articles.ProjectToType<ArticleResponse>().ToListAsync();

        if (articles.Count == 0) return NoContent();

        return Ok(articles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleResponse>> GetArticle(uint id)  
    {
        var article = await context.Articles.FindAsync(id);

        if (article == null)
        {
            return NotFound();
        }

        return Ok(article.Adapt<ArticleResponse>());
    }

    [HttpPut("{id}"), AuthorizeRoles(Role.Editor, Role.Admin)]
    public async Task<IActionResult> PutArticle(uint id, [FromBody] ArticleUpdateRequest updateArticleRequest)
    {
        var article = await context.Articles.FindAsync(id);

        if (article == null) return NotFound();

        article = updateArticleRequest.Adapt(article);
        article.DateTimeEdited = DateTime.UtcNow;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ArticleExists(id))
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
    public async Task<ActionResult<ArticleResponse>> PostArticle([FromBody] ArticleCreateRequest createArticleRequest)
    {
        var authorIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (authorIdClaim == null || !uint.TryParse(authorIdClaim, out var authorId))
        {
            return Unauthorized();
        }

        var article = createArticleRequest.Adapt<Article>();

        context.Articles.Add(article);
        await context.SaveChangesAsync();

        var response = article.Adapt<ArticleResponse>();
        return CreatedAtAction(nameof(GetArticle), new { id = article.Id }, response);
    }

    [HttpDelete("{id}"), AuthorizeRoles(Role.Editor, Role.Admin)]
    public async Task<IActionResult> DeleteArticle(uint id)
    {
        var article = await context.Articles.FindAsync(id);
        if (article == null)
        {
            return NotFound();
        }

        context.Articles.Remove(article);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool ArticleExists(uint id)
    {
        return context.Articles.Any(e => e.Id == id);
    }
}
