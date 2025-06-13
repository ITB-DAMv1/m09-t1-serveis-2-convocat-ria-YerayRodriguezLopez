using EventClient.DTOs;
using EventClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IApiService _apiService;

        public IndexModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public List<UserDto> Clients { get; set; } = new();
        public int TotalPeople { get; set; }
        public string CurrentUserRole { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            Clients = await _apiService.GetClientsAsync();
            Clients = Clients.OrderBy(c => c.CompanyName).ToList();
            TotalPeople = Clients.Sum(c => c.NumberOfPeople);

            // Get current user role from session or claims
            CurrentUserRole = HttpContext.Session.GetString("UserRole") ?? "Client";
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "PR" && userRole != "Admin")
            {
                return Forbid();
            }

            var success = await _apiService.DeleteUserAsync(id);
            if (success)
            {
                TempData["Message"] = "Usuari eliminat correctament";
            }
            else
            {
                TempData["Error"] = "Error en eliminar l'usuari";
            }

            return RedirectToPage();
        }
    }
}
