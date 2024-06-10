using BlogApp.WebAdmin.Models;
using BlogApp.WebAdmin.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.WebAdmin.Pages.Authors;

public class AuthorIndexPage(WebAuthorService authorService) : PageModel
{
    public List<AuthorModel> Authors = [];
    
    public async Task OnGetAsync()
    {
        Authors = await authorService.GetAll(Request.Cookies[Constants.AUTH_TOKEN] ?? "");
    }
}
