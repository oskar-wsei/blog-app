using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.WebAdmin.Pages.Auth;

public class AuthSignOutPage : PageModel
{
    public IActionResult OnGet()
    {
        return Redirect("/");
    }
    
    public IActionResult OnPost()
    {
        Response.Cookies.Delete(Constants.AUTH_TOKEN);
        return Redirect("/");
    }
}
