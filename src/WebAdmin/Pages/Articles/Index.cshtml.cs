using BlogApp.WebAdmin.Models;
using BlogApp.WebAdmin.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.WebAdmin.Pages.Articles;

public class ArticlesIndexPage(WebArticleService articleService) : PageModel
{
    public List<ArticleModel> Articles = [];
    
    public async Task OnGetAsync()
    {
        Articles = await articleService.GetAll(Request.Cookies[Constants.AUTH_TOKEN] ?? "");
    }
}
