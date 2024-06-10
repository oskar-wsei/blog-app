using BlogApp.WebAdmin.Models;
using BlogApp.WebAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.WebAdmin.Pages.Articles;

public class ArticleCreatePage(WebArticleService articleService) : PageModel
{
    [BindProperty] public ArticleModel Form { get; set; } = null!;

    public async Task<IActionResult> OnPost()
    {
        await articleService.Post(Request.Cookies[Constants.AUTH_TOKEN] ?? "", Form);
        return RedirectToPage("Index");
    }
}
