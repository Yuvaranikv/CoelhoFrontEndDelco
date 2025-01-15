using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TURN.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            // Clear any existing authentication
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError(string.Empty, "Username and Password are required.");
                return Page();
            }

            // Validate username and password (replace with your logic)
            if (Username == "admin" && Password == "password")
            {
                // Create claims for user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Username)
                };

                // Create identity and principal
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirect to the Index page
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }
        }
    }
}
