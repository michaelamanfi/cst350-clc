using BibleApplication.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BibleApplication.DAL
{
    /// <summary>
    /// Data access layer for the Bible database
    /// </summary>
    public class BibleDAL : IBibleDAL
    {
        private readonly string _connectionString = @"Data Source=C:\OneDrive\BS Software Development\CST-350\Week7\BibleApplication\bible-sqlite.db;";

        /// <summary>
        /// Default constructor
        /// </summary>
        public BibleDAL()
        {
        }

        /// <summary>
        /// Searches for Bible verses based on the specified testament type and search term.
        /// </summary>
        /// <param name="testamentType">The testament type (Old, New, or Both) to filter the search.</param>
        /// <param name="searchTerm">The term or phrase to search for within the Bible verses.</param>
        /// <returns>A list of BibleVerseModel objects that contain the search term within the specified testament.</returns>
        public List<BibleVerseModel> Search(TestermentType testamentType, string searchTerm)
        {
            // First, retrieve all verses from the specified testament
            var list = GetVersesFromTestament(testamentType);

            // LINQ query to find verses containing the specified string
            // The search is case-insensitive, ensuring broader match possibilities
            var versesContainingStr = list
                .Where(verse =>
                    // The IndexOf method is used to find the search term within the verse text
                    // StringComparison.OrdinalIgnoreCase makes the search case-insensitive
                    verse.Text.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            // Return the list of verses that contain the search term
            return versesContainingStr;
        }


        /// <summary>
        /// Utility method to get verses from a specific testament
        /// </summary>
        /// <param name="testament"></param>
        /// <returns></returns>
        public List<BibleVerseModel> GetVersesFromTestament(TestermentType testament)
        {
            var verses = new List<BibleVerseModel>();
            var query = testament switch
            {
                TestermentType.Old => "SELECT * FROM t_kjv WHERE b < 40", // Book numbers 1-39 are Old Testament
                TestermentType.New => "SELECT * FROM t_kjv WHERE b >= 40", // Book numbers 40+ are New Testament
                _ => "SELECT * FROM t_kjv" // Both Testaments
            };

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = new SqliteCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                verses.Add(new BibleVerseModel
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Book = Convert.ToInt32(reader["b"]),
                    Chapter = Convert.ToInt32(reader["c"]),
                    Verse = Convert.ToInt32(reader["v"]),
                    Text = reader["t"].ToString()
                });
            }

            return verses;
        }
    }
}
