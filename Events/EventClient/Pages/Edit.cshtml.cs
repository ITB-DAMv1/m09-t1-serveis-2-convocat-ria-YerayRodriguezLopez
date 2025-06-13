using EventClient.DTOs;
using EventClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventClient.Pages
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IApiService _apiService;

        public EditModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public EditUserDto EditData { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");

            // Only allow editing own profile for clients, or any profile for PR/Admin
            if (userRole == "Client" && currentUserId != id)
            {
                return Forbid();
            }
            else if (userRole != "Client" && userRole != "PR" && userRole != "Admin")
            {
                return Forbid();
            }

            var user = await _apiService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            EditData = new EditUserDto
            {
                Id = user.Id,
                CompanyName = user.CompanyName,
                Username = user.Username,
                Email = user.Email,
                NumberOfPeople = user.NumberOfPeople,
                IsVip = user.IsVip
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var success = await _apiService.UpdateUserAsync(EditData);
            if (success)
            {
                TempData["Message"] = "Dades actualitzades correctament";

                var userRole = HttpContext.Session.GetString("UserRole");
                if (userRole == "Client")
                {
                    return RedirectToPage("/Profile");
                }
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Error en actualitzar les dades.");
            return Page();
        }
    }
}
