using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTracker.Models;
using System.Diagnostics;

namespace MovieTracker.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext _context;

        public HomeController(MovieContext temp) // Constructor
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult KnowJoel()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MovieForm()
        {
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList(); // Get list of categories from database

            return View(new Movie());
        }

        [HttpPost]
        public IActionResult MovieForm(Movie response)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(response); // Add record to database
                _context.SaveChanges(); // Commit changes to database

                return View("Confirmation", response);
            }
            else // Invalid data
            {
                ViewBag.Categories = _context.Categories
                    .OrderBy(x => x.CategoryName)
                    .ToList();

                return View(response);
            }
        }

        public IActionResult MovieList()
        {
            var movies = _context.Movies
                .Include(x => x.Category) // Include related category data
                .OrderBy(x => x.Title)
                .ToList(); // Get all records from database

            return View(movies);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movieToEdit = _context.Movies.Single(x => x.MovieId == id); // Get record to edit from database

            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList(); // Get list of categories from database

            return View("MovieForm", movieToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Movie response)
        {
            _context.Movies.Update(response); // Update record in database
            _context.SaveChanges(); // Commit changes to database

            return RedirectToAction("MovieList");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movieToDelete = _context.Movies.Single(x => x.MovieId == id); // Get record to delete from database
            return View(movieToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Movie response)
        {
            _context.Movies.Remove(response); // Remove record from database
            _context.SaveChanges(); // Commit changes to database

            return RedirectToAction("MovieList");
        }
    }
}