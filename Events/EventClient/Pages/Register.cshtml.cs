using EventClient.DTOs;
using EventClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventClient.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IApiService _apiService;

        public RegisterModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public RegisterDto RegisterData { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var success = await _apiService.RegisterAsync(RegisterData);
            if (success)
            {
                TempData["Message"] = "Registre completat correctament. Pots iniciar sessió ara.";
                return RedirectToPage("/Login");
            }

            ModelState.AddModelError(string.Empty, "Error en el registre. Torna-ho a intentar.");
            return Page();
        }
    }
}
