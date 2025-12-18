using BolascoProel4.Data;
using BolascoProel4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BolascoProel4.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly BolascoProel4Context _context;

        public CreateModel(BolascoProel4Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);

                    Project.ImageData = memoryStream.ToArray(); // Save bytes
                    Project.ImageType = ImageFile.ContentType;  // Save mime type
                }
            }

            _context.Project.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
