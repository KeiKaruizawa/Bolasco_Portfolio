

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
    public class IndexModel : PageModel
    {
        private readonly BolascoProel4.Data.BolascoProel4Context _context;

        public IndexModel(BolascoProel4.Data.BolascoProel4Context context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Project = await _context.Project.ToListAsync();
        }
    }
}
