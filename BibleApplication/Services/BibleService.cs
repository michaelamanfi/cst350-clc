using BibleApplication.DAL;
using BibleApplication.Models;
using System.Collections.Generic;

namespace BibleApplication.Services
{/// <summary>
 /// Provides services for searching Bible verses.
 /// </summary>
    public class BibleService : IBibleService
    {
        private readonly IBibleDAL _bibleDAL;

        /// <summary>
        /// Initializes a new instance of the BibleService class.
        /// </summary>
        /// <param name="bibleDAL">The data access layer used for accessing Bible data.</param>
        public BibleService(IBibleDAL bibleDAL)
        {
            _bibleDAL = bibleDAL;
        }

        /// <summary>
        /// Searches for Bible verses based on a testament type and search term.
        /// </summary>
        /// <param name="testamentType">The testament type to search in (Old, New, or Both).</param>
        /// <param name="searchTerm">The term to search for in the Bible verses.</param>
        /// <returns>A list of BibleVerse objects that match the search criteria.</returns>
        public List<BibleVerseModel> Search(TestermentType testamentType, string searchTerm)
        {
            return _bibleDAL.Search(testamentType, searchTerm);
        }
    }

}
