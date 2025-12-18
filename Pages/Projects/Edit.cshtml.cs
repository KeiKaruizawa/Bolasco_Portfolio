using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BolascoProel4.Data;
using BolascoProel4.Models;

namespace BolascoProel4.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly BolascoProel4Context _context;

        public EditModel(BolascoProel4Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        // For image upload
        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }
            Project = project;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Update image if new one uploaded
            if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    Project.ImageData = memoryStream.ToArray();
                    Project.ImageType = ImageFile.ContentType;
                }
            }
            else
            {
                // Keep existing image if no new image uploaded
                var existingProject = await _context.Project.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.ProjectId == Project.ProjectId);
                if (existingProject != null)
                {
                    Project.ImageData = existingProject.ImageData;
                    Project.ImageType = existingProject.ImageType;
                }
            }

            _context.Attach(Project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(Project.ProjectId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectId == id);
        }
    }
}
