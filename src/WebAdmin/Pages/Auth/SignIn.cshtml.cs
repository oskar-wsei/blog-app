using System.ComponentModel.DataAnnotations;
using BlogApp.WebAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.WebAdmin.Pages.Auth;

public class AuthSignInPage(ILogger<AuthSignInPage> logger, WebAuthService authService) : PageModel
{
    [BindProperty] public AuthForm Form { get; set; } = null!;
    
    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        
        try
        {
            var token = await authService.Authenticate(Form.Username, Form.Password);

            if (token is null)
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }
            
            HttpContext.Response.Cookies.Append(Constants.AUTH_TOKEN, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddHours(6)
            });

            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during authentication.");
            ErrorMessage = "An error occurred while processing your request.";
            return Page();
        }
    }

    public class AuthForm
    {
        [Required] public string Username { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }
    
    public class AuthResponse
    {
        public string Token { get; set; } = "";
    }
}
