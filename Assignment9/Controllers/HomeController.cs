using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment9.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

//This is where all the magic happens
namespace Assignment9.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SignUpDbContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, SignUpDbContext con)
        {
            _logger = logger;
            context = con;
        }

        //Default Home Page
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //Podcast Page
        public IActionResult Podcast()
        {
            return View();
        }

        //New Form for new Movie to add to the list
        [HttpGet]
        public IActionResult newMovies()
        {
            return View();
        }

        //Movie is added here
        [HttpPost]
        public IActionResult newMovies(MovieModel movieSubmit)
        {
            if (movieSubmit.Title == "Independence Day")
            {
                ViewBag.Message = String.Format("You cannot submit Independence Day");
                return View();
            }

            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                TempStorage.AddApplication(movieSubmit);
                context.MovieModels.Add(movieSubmit);
                context.SaveChanges();

                return View("Confirmation", movieSubmit);
            }
        }

        //The list of movies
        public IActionResult waitlist()
        {
            return View(context.MovieModels);
        }

        //Editing the Submisions
        [HttpPost]
        public IActionResult EditSubmissionForm(int editId)
        {
            MovieModel editmovies = context.MovieModels.FirstOrDefault(s => s.MovieId == editId);
            ViewBag.editmovies = editmovies;
            return View("EditSubmissionForm");
        }

        //Edited !!!WARNING make sure that when edit is made that the changes go through (they will if you did it right)
        [HttpPost]
        public IActionResult Edit(MovieModel movieEdited)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.editmovies = movieEdited;
                return View("EditSubmissionForm");
            }
            else
            {
                var editmovies = context.MovieModels.FirstOrDefault(s => s.MovieId == movieEdited.MovieId);
                editmovies.Category = movieEdited.Category;
                editmovies.Title = movieEdited.Title;
                editmovies.Year = movieEdited.Year;
                editmovies.Director = movieEdited.Director;
                editmovies.Rating = movieEdited.Rating;
                editmovies.Edited = movieEdited.Edited;
                editmovies.LentTo = movieEdited.LentTo;
                editmovies.Notes = movieEdited.Notes;
                context.SaveChanges();
                return View("waitlist", context.MovieModels);
            }
        }

        //Delete something on the waitlist page
        [HttpPost]
        public IActionResult Delete(int deletionId)
        {
            context.Remove(context.MovieModels.FirstOrDefault(s => s.MovieId == deletionId));
            context.SaveChanges();
            return View("waitlist", context.MovieModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}