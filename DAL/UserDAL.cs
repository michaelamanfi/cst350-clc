namespace Minesweeper.DAL
{
    using Minesweeper.Models;
    using System;
    using System.Data.SqlClient;
    using System.Configuration;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Represents the Data Access Layer for 'User'.
    /// Contains methods for Create, Read, Update, and Delete operations for users.
    /// </summary>
    public class UserDAL : IUserDAL
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the UserDAL class.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        public UserDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MinesweeperDB");
        }

        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="user">The user object to be created.</param>
        public void CreateUser(UserModel user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Users (FirstName, LastName, Sex, Age, State, EmailAddress, Username, Password) VALUES (@FirstName, @LastName, @Sex, @Age, @State, @EmailAddress, @Username, @Password)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Sex", user.Sex);
                    cmd.Parameters.AddWithValue("@Age", user.Age);
                    cmd.Parameters.AddWithValue("@State", user.State ?? (object)DBNull.Value); // Handling possible null for State
                    cmd.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password); // Ensure this is a hashed password

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Checks if a username already exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username exists; otherwise, false.</returns>
        public bool UserExists(string username)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT COUNT(1) FROM Users WHERE Username = @Username";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());

                    return result > 0;
                }
            }
        }
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>A UserModel if found, otherwise null.</returns>
        public UserModel GetUser(int userId)
        {
            UserModel user = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Users WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserModel
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Sex = reader["Sex"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                State = reader["State"].ToString(),
                                EmailAddress = reader["EmailAddress"].ToString(),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString() // Note: Password should be securely hashed
                            };
                        }
                    }
                }
            }

            return user;
        }

        /// <summary>
        /// Updates an existing user's information in the database.
        /// </summary>
        /// <param name="user">The user object with updated information.</param>
        public void UpdateUser(UserModel user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Sex = @Sex, Age = @Age, State = @State, EmailAddress = @EmailAddress, Username = @Username, Password = @Password WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", user.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Sex", user.Sex);
                    cmd.Parameters.AddWithValue("@Age", user.Age);
                    cmd.Parameters.AddWithValue("@State", user.State ?? (object)DBNull.Value); // Handling possible null for State
                    cmd.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password); // Ensure this is a hashed password

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        public void DeleteUser(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Users WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Retrieves a user's password based on username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user password.</returns>
        public string GetPasswordByUserName(string username)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Password FROM Users WHERE Username = @Username";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        // Assuming password is hashed, compare the hashed password
                        return result.ToString();                        
                    }
                }
            }

            return null;
        }
        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>A UserModel if found, otherwise null.</returns>
        public UserModel GetUser(string username)
        {
            UserModel user = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Users WHERE Username = @Username";                

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserModel
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Sex = reader["Sex"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                State = reader["State"].ToString(),
                                EmailAddress = reader["EmailAddress"].ToString(),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString()
                            };
                        }
                    }
                }
            }

            return user;
        }
    }

}
