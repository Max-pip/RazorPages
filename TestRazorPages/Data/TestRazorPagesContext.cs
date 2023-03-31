using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestRazorPages.Models;

namespace TestRazorPages.Data
{
    public class TestRazorPagesContext : DbContext
    {
        public TestRazorPagesContext (DbContextOptions<TestRazorPagesContext> options)
            : base(options)
        {
        }

        public DbSet<TestRazorPages.Models.Movie> Movie { get; set; } = default!;
    }
}
