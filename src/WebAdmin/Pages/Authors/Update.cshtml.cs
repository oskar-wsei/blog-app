using BlogApp.WebAdmin.Models;
using BlogApp.WebAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.WebAdmin.Pages.Authors;

public class AuthorUpdatePage(WebAuthorService authorService) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    
    [BindProperty] public AuthorModel Form { get; set; } = null!;
    
    public async Task<IActionResult> OnGetAsync()
    {
        var author = await authorService.Get(Request.Cookies[Constants.AUTH_TOKEN] ?? "", Id);
        if (author is null) return RedirectToPage("Index");
        Form = author;
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        await authorService.Put(Request.Cookies[Constants.AUTH_TOKEN] ?? "", Id, Form);
        return RedirectToPage("Index");
    }
}
