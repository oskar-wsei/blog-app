using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("articles")]
public class ArticleController(IApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public IActionResult OnGet()
    {
        return Ok(context.Articles.ToList());
    }
    
    [HttpGet("{id:int}")]
    public IActionResult OnGet(int id)
    {
        var article = context.Articles.Find(id);
        if (article is null) return NotFound();
        return Ok(article);
    }

    [HttpPost]
    public async Task<IActionResult> OnPost(ArticleRequestData data)
    {
        var article = new Article { Title = data.Title, Content = data.Content, AuthorId = data.AuthorId };
        await context.Articles.AddAsync(article);
        await context.SaveChangesAsync();
        return CreatedAtAction("OnGet", new { id = article.Id }, article);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> OnPut(int id, ArticleRequestData data)
    {
        var article = await context.Articles.FindAsync(id);
        if (article is null) return NotFound();
        context.Entry(article).State = EntityState.Modified;
        article.AuthorId = data.AuthorId;
        article.Title = data.Title;
        article.Content = data.Content;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> OnDelete(int id)
    {
        var article = await context.Articles.FindAsync(id);
        if (article is null) return NotFound();
        context.Articles.Remove(article);
        await context.SaveChangesAsync();
        return NoContent();
    }

    public record ArticleRequestData
    {
        public string? Title { get; init; }
        
        public string? Content { get; init; }
        
        public int? AuthorId { get; init; }
    }
}
