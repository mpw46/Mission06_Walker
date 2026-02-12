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
            return View();
        }

        [HttpPost]
        public IActionResult MovieForm(Movie response)
        {
            _context.Movies.Add(response); // Add record to database
            _context.SaveChanges(); // Commit changes to database

            return View("Confirmation", response);
        }
    }
}