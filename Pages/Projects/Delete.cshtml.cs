using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BolascoProel4.Data;
using BolascoProel4.Models;

namespace BolascoProel4.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly BolascoProel4.Data.BolascoProel4Context _context;

        public DeleteModel(BolascoProel4.Data.BolascoProel4Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

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
            else
            {
                Project = project;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project != null)
            {
                Project = project;
                _context.Project.Remove(Project);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
