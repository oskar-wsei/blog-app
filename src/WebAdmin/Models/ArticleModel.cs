namespace BlogApp.WebAdmin.Models;

public class ArticleModel : BaseModel
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int? AuthorId { get; set; }
}
