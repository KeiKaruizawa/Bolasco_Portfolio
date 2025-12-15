using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BolascoProel4.Pages
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string? Message { get; set; }

        public void OnGet()
        {
            // Page loads normally
        }

        public IActionResult OnPost(string name, string email, string message)
        {
            // Validate the input
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
            {
                Message = "Please fill in all fields.";
                return Page();
            }

            Message = "Thanks for reaching out! I'll get back to you soon.";

            return RedirectToPage();
        }
    }
}
