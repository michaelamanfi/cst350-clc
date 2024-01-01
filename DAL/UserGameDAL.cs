namespace Minesweeper.DAL
{
    using Minesweeper.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Configuration;

    public class UserGameDAL : IUserGameDAL
    {
        private readonly string _connectionString;

        public UserGameDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MinesweeperDB");
        }

        public UserGameModel GetGameById(int gameId)
        {
            UserGameModel userGame = null;

            using (var conn = new SqlConnection(_connectionString))
            {
                const string sql = @"SELECT g.*, u.* FROM Game g 
                                     INNER JOIN Users u ON g.UserId = u.UserId 
                                     WHERE g.GameId = @GameId";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GameId", gameId);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
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

        public List<UserGameModel> GetAllGames()
        {
            var userGames = new List<UserGameModel>();

            using (var conn = new SqlConnection(_connectionString))
            {
                const string sql = @"SELECT g.*, u.* FROM Game g 
                                     INNER JOIN Users u ON g.UserId = u.UserId";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
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
