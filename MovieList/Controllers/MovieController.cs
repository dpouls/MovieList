using Microsoft.AspNetCore.Mvc;
using MovieList.Models;
using System.Linq;

namespace MovieList.Controllers
{
    public class MovieController : Controller
    {
        //allows us to have the database data by using the movie context class
        private MovieContext context { get; set; }
            /// <summary>
            /// Assigns the database information to a new variable called context
            /// </summary>
            /// <param name="ctx"></param>
        public MovieController(MovieContext ctx)
        {
            /// Assigns the database information to a new variable called context
            context = ctx;
        }
        //defines the method is for an HTTP GET request
        [HttpGet]
        //Assigns viewbag content and returns the edit view renamed with Add
        public IActionResult Add()
        {
            //change viewbag.action to Add
            ViewBag.Action = "Add";
            //change viewbag.genres to what the genres are in the database via the context variable defined in the constructor method
            ViewBag.Genres = context.Genres.OrderBy(g => g.Name).ToList();
            //returns the view "Edit" and creates a new instance of the Movie Class.
            return View("Edit", new Movie()) ;
        }
        //defines the method is for an HTTP GET request

        [HttpGet]
        //Assigns viewbag content and returns the edit view with the movie information we are going to edit
        public IActionResult Edit(int id)
        {
            //change viewbag.action to Edit
            ViewBag.Action = "Edit";
            //change viewbag.genres to what the genres are in the database via the context variable defined in the constructor method
            ViewBag.Genres = context.Genres.OrderBy(g => g.Name).ToList();
            //finds the movie from the database given the id that was sent in the asp-route-id
            Movie movie = context.Movies.Find(id);
            //returns the view with the movie information
            return View(movie);
        }
        //defines the method is for an HTTP Post request

        [HttpPost]
        //submits the edited movie information
        public IActionResult Edit(Movie movie)
        {
            //checks to make sure there were no validation errors in the class.
            if (ModelState.IsValid)
            {
                //if the movie id is zero, it means it is a new movie, not one being edited.
                if (movie.MovieId == 0)
                {
                    //we add the new movie to the database throught the context defined in the constructor.
                    context.Movies.Add(movie);
                }
                else
                {
                    //else means it is an edited movie so we will update the movie via the context variable defined in the constructor.
                    context.Movies.Update(movie);
                }
                //save the changes to the database.
                context.SaveChanges();

                //redirects the user to the home page (index)
                return RedirectToAction("Index","Home");
            }
            else
            {
                //There was a problem and we return them to the form to correct it. If the id is zero, it is add, else it is edit.
                ViewBag.Action = (movie.MovieId == 0) ? "Add" : "Edit";
                //generate the genres from the database information into a list
                ViewBag.Genres = context.Genres.OrderBy(g => g.Name).ToList();
                //returns the appropriate view (Edit but relabeled if it was Add)
            return View(movie);
            }
        }
        //defines the method is for an HTTP GET request

        [HttpGet]
        //finds the movie and displays it in a view
        public IActionResult Delete(int id)
        {
            //finds the movie in the database
            Movie movie = context.Movies.Find(id);
            //returns the view with the movie information
            return View(movie);
        }
        //defines the method is for an HTTP Post request

        [HttpPost]
        //deletes the movie from the database
        public IActionResult Delete(Movie movie)
        {
            //goes to the database and removes the movie
            context.Movies.Remove(movie);
            //save the changes to the database
            context.SaveChanges();
            //redirects the user to the index or home page via the home controller.
            return RedirectToAction("Index", "Home");
        }
    }
}
