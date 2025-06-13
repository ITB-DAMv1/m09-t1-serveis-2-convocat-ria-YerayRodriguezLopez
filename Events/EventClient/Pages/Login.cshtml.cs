using EventClient.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using EventClient.DTOs;

namespace EventClient.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IApiService _apiService;

        public LoginModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public LoginDto LoginData { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var authResponse = await _apiService.LoginAsync(LoginData);
            if (authResponse != null)
            {
                // Store token in session
                HttpContext.Session.SetString("Token", authResponse.Token);
                HttpContext.Session.SetString("UserRole", authResponse.User.Role);
                HttpContext.Session.SetInt32("UserId", authResponse.User.Id);

                // Create authentication cookie
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, authResponse.User.Username),
                    new Claim(ClaimTypes.Email, authResponse.User.Email),
                    new Claim(ClaimTypes.Role, authResponse.User.Role),
                    new Claim("UserId", authResponse.User.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                // Redirect based on role
                if (authResponse.User.Role == "Client")
                {
                    return RedirectToPage("/Profile");
                }
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Email o contrasenya incorrectes.");
            return Page();
        }
    }
}
