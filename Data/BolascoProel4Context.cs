using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BolascoProel4.Models;

namespace BolascoProel4.Data
{
    public class BolascoProel4Context : DbContext
    {
        public BolascoProel4Context (DbContextOptions<BolascoProel4Context> options)
            : base(options)
        {
        }

        public DbSet<BolascoProel4.Models.Project> Project { get; set; } = default!;
    }
}
