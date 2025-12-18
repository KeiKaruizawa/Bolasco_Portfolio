using BolascoProel4.Data;
using BolascoProel4.Migrations;
using BolascoProel4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolascoProel4.Pages.Projects
{
    public class CreateModel : PageModel
    {
        // Use the correct context name (not ContextModelSnapshot!)
        private readonly BolascoProel4Context _context;

        public CreateModel(BolascoProel4Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        // This property receives the uploaded image file
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

            // Convert uploaded image to byte array
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Create a memory stream to read the file
                using (var memoryStream = new MemoryStream())
                {
                    // Copy the uploaded file into memory
                    await ImageFile.CopyToAsync(memoryStream);

                    // Convert to byte array and store in Project
                    Project.ImageData = memoryStream.ToArray();

                    // Store the image type (jpeg, png, etc.)
                    // Using ImageType instead of ImageContentType
                    Project.ImageType = ImageFile.ContentType;
                }
            }

            // Use the correct DbSet name
            _context.Project.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
