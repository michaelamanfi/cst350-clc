using Minesweeper.Models;

namespace Minesweeper
{
    /// <summary>
    /// Represents the data access layer interface for user operations.
    /// This interface defines the basic CRUD operations for managing users.
    /// </summary>
    public interface IUserDAL
    {
        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="user">The user model containing the details of the user to be created.</param>
        void CreateUser(UserModel user);
        /// <summary>
        /// Checks if a username already exists in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username exists; otherwise, false.</returns>
        bool UserExists(string username);
        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to be deleted.</param>
        void DeleteUser(int userId);

        /// <summary>
        /// Retrieves a user's details from the database.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve.</param>
        /// <returns>The user model containing the details of the user. Returns null if the user is not found.</returns>
        UserModel GetUser(int userId);

        /// <summary>
        /// Retrieves a user's details from the database.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The user model containing the details of the user. Returns null if the user is not found.</returns>
        UserModel GetUser(string username);

        /// <summary>
        /// Updates the details of an existing user in the database.
        /// </summary>
        /// <param name="user">The user model containing the updated details of the user. The user is identified by the UserId within the model.</param>
        void UpdateUser(UserModel user);

        /// <summary>
        /// Retrieves a user's password based on username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user password.</returns>
        string GetPasswordByUserName(string username);
    }

}