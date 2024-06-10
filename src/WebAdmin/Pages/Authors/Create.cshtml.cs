using BlogApp.WebAdmin.Models;
using BlogApp.WebAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.WebAdmin.Pages.Authors;

public class AuthorCreatePage(WebAuthorService authorService) : PageModel
{
    [BindProperty] public AuthorModel Form { get; set; } = null!;

    public async Task<IActionResult> OnPost()
    {
        await authorService.Post(Request.Cookies[Constants.AUTH_TOKEN] ?? "", Form);
        return RedirectToPage("Index");
    }
}
