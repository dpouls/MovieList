using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieList.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.Controllers
{
    public class HomeController : Controller
    {
        //this property holds the information needed for our database connection
        private MovieContext context { get; set; }

        public HomeController(MovieContext ctx)
        {
            //we assign the database information to this context variable.
            context = ctx;
        }
        public IActionResult Index()
        {
            //we list out the movies from the context (from MovieContext class) and include name and genre. 
            List<Movie> movies = context.Movies.Include(m => m.Genre).OrderBy(m => m.Name).ToList();
            //returns a view with the movie list passed in
            return View(movies);
        }
    }
}
