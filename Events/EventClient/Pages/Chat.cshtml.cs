using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventClient.Pages
{
    [Authorize]
    public class ChatModel : PageModel
    {
        [BindProperty]
        public string Message { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;
        public string ApiUrl { get; set; } = string.Empty;

        private readonly IConfiguration _configuration;

        public ChatModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            Username = User.Identity?.Name ?? "Usuari Anònim";
            ApiUrl = _configuration.GetConnectionString("ApiUrl")?.Replace("/api", "") ?? "https://localhost:7001";
        }
    }
}
