using EventClient.DTOs;
using EventClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventClient.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IApiService _apiService;

        public ProfileModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public UserDto? User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                User = await _apiService.GetUserByIdAsync(userId.Value);
                if (User == null)
                {
                    return NotFound();
                }
                return Page();
            }
            return RedirectToPage("/Login");
        }
    }
}
