namespace Minesweeper.DAL
{
    using Minesweeper.Models;
    using System;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the Data Access Layer for the Game entity.
    /// Contains methods for Create, Read, Update, and Delete (CRUD) operations.
    /// </summary>
    public class GameDAL : IGameDAL
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the GameDAL class.
        /// </summary>
        /// <param name="configuration">The configuration containing the connection string.</param>
        public GameDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MinesweeperDB");
        }

        /// <summary>
        /// Creates a new game record in the database.
        /// </summary>
        /// <param name="game">The game model to be inserted.</param>
        public void CreateGame(GameDbModel game)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT INTO Game (UserId, StatusId, Json, CreatedDate, ModifiedDate, DisplayName) " +
                                   "VALUES (@UserId, @StatusId, @Json, @CreatedDate, @ModifiedDate, @DisplayName)";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", game.UserId);
                    cmd.Parameters.AddWithValue("@StatusId", game.StatusId);
                    cmd.Parameters.AddWithValue("@Json", game.Json ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedDate", game.CreatedDate);
                    cmd.Parameters.AddWithValue("@ModifiedDate", game.ModifiedDate);
                    cmd.Parameters.AddWithValue("@DisplayName", game.DisplayName ?? (object)DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Retrieves a game record by its identifier.
        /// </summary>
        /// <param name="gameId">The ID of the game to retrieve.</param>
        /// <returns>A game model if found; otherwise, null.</returns>
        public GameDbModel GetGameById(int gameId)
        {
            GameDbModel game = null;

            using (var conn = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Game WHERE GameId = @GameId";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GameId", gameId);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            game = new GameDbModel
                            {
                                GameId = Convert.ToInt32(reader["GameId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                StatusId = Convert.ToInt32(reader["StatusId"]),
                                Json = reader["Json"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"]),
                                DisplayName = reader["DisplayName"].ToString()
                            };
                        }
                    }
                }
            }

            return game;
        }
        /// <summary>
        /// Updates an existing game record in the database.
        /// </summary>
        /// <param name="game">The game model with updated information.</param>
        public void UpdateGame(GameDbModel game)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string sql = "UPDATE Game SET StatusId = @StatusId, Json = @Json, " +
                                   "ModifiedDate = @ModifiedDate WHERE GameId = @GameId";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GameId", game.GameId);
                    //cmd.Parameters.AddWithValue("@UserId", game.UserId);
                    cmd.Parameters.AddWithValue("@StatusId", game.StatusId);
                    cmd.Parameters.AddWithValue("@Json", game.Json ?? (object)DBNull.Value);
                    //cmd.Parameters.AddWithValue("@CreatedDate", game.CreatedDate);
                    cmd.Parameters.AddWithValue("@ModifiedDate", game.ModifiedDate);
                    //cmd.Parameters.AddWithValue("@DisplayName", game.DisplayName ?? (object)DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletes a game record from the database.
        /// </summary>
        /// <param name="gameId">The ID of the game to delete.</param>
        public void DeleteGame(int gameId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string sql = "DELETE FROM Game WHERE GameId = @GameId";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GameId", gameId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Retrieves all game records from the database.
        /// </summary>
        /// <returns>A list of all game models.</returns>
        public List<GameDbModel> GetAllGames()
        {
            var games = new List<GameDbModel>();

            using (var conn = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Game";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var game = new GameDbModel
                            {
                                GameId = Convert.ToInt32(reader["GameId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                StatusId = Convert.ToInt32(reader["StatusId"]),
                                Json = reader["Json"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"]),
                                DisplayName = reader["DisplayName"].ToString()
                            };
                            games.Add(game);
                        }
                    }
                }
            }

            return games;
        }
        /// <summary>
        /// Checks if a game with the specified ID exists in the database.
        /// </summary>
        /// <param name="gameId">The ID of the game to check.</param>
        /// <returns>True if the game exists; otherwise, false.</returns>
        public bool GameExists(int gameId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT COUNT(1) FROM Game WHERE GameId = @GameId";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GameId", gameId);

                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
    }
}
