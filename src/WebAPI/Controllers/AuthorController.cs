using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("authors")]
public class AuthorController(IApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public IActionResult OnGet()
    {
        return Ok(context.Authors.ToList());
    }
    
    [HttpGet("{id:int}")]
    public IActionResult OnGet(int id)
    {
        var author = context.Authors.Find(id);
        if (author is null) return NotFound();
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> OnPost(AuthorRequestData data)
    {
        var author = new Author { FirstName = data.FirstName, LastName = data.LastName, Description = data.Description};
        await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();
        return CreatedAtAction("OnGet", new { id = author.Id }, author);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> OnPut(int id, AuthorRequestData data)
    {
        var author = await context.Authors.FindAsync(id);
        if (author is null) return NotFound();
        context.Entry(author).State = EntityState.Modified;
        author.FirstName = data.FirstName;
        author.LastName = data.LastName;
        author.Description = data.Description;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> OnDelete(int id)
    {
        var author = await context.Authors.FindAsync(id);
        if (author is null) return NotFound();
        context.Authors.Remove(author);
        await context.SaveChangesAsync();
        return NoContent();
    }

    public record AuthorRequestData
    {
        public string? FirstName { get; init; }
        
        public string? LastName { get; init; }
        
        public string? Description { get; init; }
    }
}
