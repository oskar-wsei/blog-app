using BlogApp.Domain.Common;

namespace BlogApp.Domain.Entities;

public class Author : BaseAuditableEntity
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Description { get; set; }

    public IList<Article> Articles { get; private set; } = new List<Article>();
}