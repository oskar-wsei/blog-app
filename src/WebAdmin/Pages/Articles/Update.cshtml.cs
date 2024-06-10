using BlogApp.WebAdmin.Models;
using BlogApp.WebAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.WebAdmin.Pages.Articles;

public class ArticleUpdatePage(WebArticleService articleService) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    
    [BindProperty] public ArticleModel Form { get; set; } = null!;
    
    public async Task<IActionResult> OnGetAsync()
    {
        var article = await articleService.Get(Request.Cookies[Constants.AUTH_TOKEN] ?? "", Id);
        if (article is null) return RedirectToPage("Index");
        Form = article;
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        await articleService.Put(Request.Cookies[Constants.AUTH_TOKEN] ?? "", Id, Form);
        return RedirectToPage("Index");
    }
}
