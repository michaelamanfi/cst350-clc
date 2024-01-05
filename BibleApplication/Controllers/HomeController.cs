using BibleApplication.Models;
using BibleApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BibleApplication.Controllers
{
    /// <summary>
    /// Controller for handling requests related to home, privacy, and Bible search functionality.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IBibleService _bibleService;

        /// <summary>
        /// Initializes a new instance of the HomeController class.
        /// </summary>
        /// <param name="bibleService">The service for accessing Bible data.</param>
        public HomeController(IBibleService bibleService)
        {
            _bibleService = bibleService;
        }

        /// <summary>
        /// Displays the home page.
        /// </summary>
        /// <returns>The Index view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Searches the Bible based on the provided search model and displays the results.
        /// </summary>
        /// <param name="searchModel">The search criteria.</param>
        /// <returns>A view with the search results; redirects to Index with an error message if no results are found.</returns>
        public IActionResult SearchResults(SearchModel searchModel)
        {
            // Trim the search term to remove leading/trailing whitespaces
            var list = _bibleService.Search(searchModel.Testament, searchModel.Text.Trim());

            // Check if any results are found
            if (list.Count > 0)
            {
                // Return the SearchResults view with the list of Bible verses
                return View(list);
            }
            else
            {
                // Set an error message and return to the Index view if no results are found
                ViewBag.SuccessMessage = "Error: No verse found for the search term.";
                return View("Index");
            }
        }
    }
}
