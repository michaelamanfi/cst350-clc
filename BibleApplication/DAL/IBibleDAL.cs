using BibleApplication.Models;
using System.Collections.Generic;

namespace BibleApplication.DAL
{
    public interface IBibleDAL
    {
        /// <summary>
        /// Utility method to get verses from a specific testament
        /// </summary>
        /// <param name="testament"></param>
        /// <returns></returns>
        List<BibleVerseModel> GetVersesFromTestament(TestermentType testament);
        /// <summary>
        /// Searches for Bible verses based on the specified testament type and search term.
        /// </summary>
        /// <param name="testamentType">The testament type (Old, New, or Both) to filter the search.</param>
        /// <param name="searchTerm">The term or phrase to search for within the Bible verses.</param>
        /// <returns>A list of BibleVerseModel objects that contain the search term within the specified testament.</returns>
        List<BibleVerseModel> Search(TestermentType testamentType, string searchTerm);
    }
}