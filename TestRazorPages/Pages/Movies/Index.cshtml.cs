using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestRazorPages.Data;
using TestRazorPages.Models;

namespace TestRazorPages.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly TestRazorPages.Data.TestRazorPagesContext _context;

        public IndexModel(TestRazorPages.Data.TestRazorPagesContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string ? SearchingString { get; set; }
        public SelectList ? Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ? MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<string> genreQuery = from m in _context.Movie orderby m.Genre select m.Genre;

            var movies = from m in _context.Movie select m;

            if (!string.IsNullOrEmpty(SearchingString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchingString));
            }

            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre ==  MovieGenre);
            }

            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    }
}
