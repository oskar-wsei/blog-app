using BlogApp.WebAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.WebAdmin.Pages.Articles;

public class ArticleDeletePage(WebArticleService articleService) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    
    public IActionResult OnGet()
    {
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await articleService.Delete(Request.Cookies[Constants.AUTH_TOKEN] ?? "", Id);
        return RedirectToPage("Index");
    }
}
