namespace Minesweeper.DAL
{
    using Minesweeper.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Data Access Layer for interacting with User and Game data.
    /// </summary>
    public class UserGameDAL : IUserGameDAL
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserGameDAL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration object for retrieving the connection string.</param>
        public UserGameDAL(IConfiguration configuration)
        {
            // Getting the database connection string from app settings.
            _connectionString = configuration.GetConnectionString("MinesweeperDB");
        }

        /// <summary>
        /// Retrieves a game by its ID along with associated user details.
        /// </summary>
        /// <param name="gameId">The ID of the game to retrieve.</param>
        /// <returns>A <see cref="UserGameModel"/> object containing game and user details.</returns>
        public UserGameModel GetGameById(int gameId)
        {
            UserGameModel userGame = null;

            // Establishing a connection to the database.
            using (var conn = new SqlConnection(_connectionString))
            {
                // SQL query to fetch game data joined with user data.
                const string sql = @"SELECT g.*, u.* FROM Game g 
                                     INNER JOIN Users u ON g.UserId = u.UserId 
                                     WHERE g.GameId = @GameId";

                // Executing the SQL command.
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GameId", gameId);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Mapping the data to a UserGameModel object.
                            userGame = new UserGameModel
                            {
                                GameId = Convert.ToInt32(reader["GameId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                StatusId = Convert.ToInt32(reader["StatusId"]),
                                Json = reader["Json"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"]),
                                DisplayName = reader["DisplayName"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Sex = reader["Sex"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                State = reader["State"].ToString(),
                                EmailAddress = reader["EmailAddress"].ToString(),
                                Username = reader["Username"].ToString()
                            };
                        }
                    }
                }
            }

            return userGame;
        }

        /// <summary>
        /// Retrieves all games along with associated user details.
        /// </summary>
        /// <returns>A list of <see cref="UserGameModel"/> objects.</returns>
        public List<UserGameModel> GetAllGames()
        {
            var userGames = new List<UserGameModel>();

            // Establishing a connection to the database.
            using (var conn = new SqlConnection(_connectionString))
            {
                // SQL query to fetch all game data joined with user data.
                const string sql = @"SELECT g.*, u.* FROM Game g 
                                     INNER JOIN Users u ON g.UserId = u.UserId";

                // Executing the SQL command.
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Mapping the data to a UserGameModel object.
                            var userGame = new UserGameModel
                            {
                                GameId = Convert.ToInt32(reader["GameId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                StatusId = Convert.ToInt32(reader["StatusId"]),
                                Json = reader["Json"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"]),
                                DisplayName = reader["DisplayName"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Sex = reader["Sex"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                State = reader["State"].ToString(),
                                EmailAddress = reader["EmailAddress"].ToString(),
                                Username = reader["Username"].ToString()
                            };
                            userGames.Add(userGame);
                        }
                    }
                }
            }

            return userGames;
        }
    }
}
