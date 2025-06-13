using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventClient.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
