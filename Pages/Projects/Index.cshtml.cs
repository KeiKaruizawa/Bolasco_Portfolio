using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BolascoProel4.Data;
using BolascoProel4.Models;

namespace BolascoProel4.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly BolascoProel4Context _context;

        public IndexModel(BolascoProel4Context context)
        {
            _context = context;
        }

        public IList<Project> Project { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Project = await _context.Project.ToListAsync();
        }

        // This method retrieves and returns the image as a file
        public async Task<IActionResult> OnGetImageAsync(int id)
        {
            var project = await _context.Project.FindAsync(id);

            if (project?.ImageData == null)
            {
                return NotFound();
            }

            return File(project.ImageData, project.ImageType ?? "image/jpeg");
        }
    }
}