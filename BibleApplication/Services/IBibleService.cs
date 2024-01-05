using BibleApplication.Models;
using System.Collections.Generic;

namespace BibleApplication.Services
{
    /// <summary>
    /// Defines a contract for a service to search within the Bible text.
    /// </summary>
    public interface IBibleService
    {
        /// <summary>
        /// Searches for Bible verses based on a specified testament type and search term.
        /// </summary>
        /// <param name="testamentType">The testament type (Old, New, or Both) to search within.</param>
        /// <param name="searchTerm">The term or phrase to search for in the Bible verses.</param>
        /// <returns>A list of BibleVerseModel objects that match the search criteria.</returns>
        List<BibleVerseModel> Search(TestermentType testamentType, string searchTerm);
    }
}
