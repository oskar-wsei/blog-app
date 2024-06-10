using BlogApp.Domain.Common;

namespace BlogApp.Domain.Entities;

public class Article : BaseAuditableEntity
{
    public string? Title { get; set; }
    
    public string? Content { get; set; }
    
    public int? AuthorId { get; set; }
    
    public Author? Author { get; set; }
}