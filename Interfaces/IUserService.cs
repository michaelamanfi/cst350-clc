using Minesweeper.Models;

namespace Minesweeper
{
    /// <summary>
    /// Defines the contract for user services.
    /// This interface encapsulates the business logic for user operations and interacts with the data access layer.
    /// </summary>
    public interface IUserService
    {
            /// <summary>
            /// Deletes a user from the system.
            /// </summary>
            /// <param name="userId">The unique identifier of the user to be deleted.</param>
        void DeleteUser(int userId);

        /// <summary>
        /// Retrieves a specific user's details.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve.</param>
        /// <returns>A UserModel containing the details of the requested user. Returns null if the user is not found.</returns>
        UserModel GetUser(int userId);

        /// <summary>
        /// Retrieves a specific user's details.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>A UserModel containing the details of the requested user. Returns null if the user is not found.</returns>
        UserModel GetUser(string username);

        /// <summary>
        /// Checks if a username already exists.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username exists; otherwise, false.</returns>
        bool UserExists(string username);
        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="user">The UserModel containing the details of the user to be registered.</param>
        void RegisterUser(UserModel user);

        /// <summary>
        /// Updates the details of an existing user.
        /// </summary>
        /// <param name="user">The UserModel containing the updated information of the user. The user is identified by the UserId within the model.</param>
        void UpdateUser(UserModel user);
        /// <summary>
        /// Retrieves a user's password based on username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user password.</returns>
        string GetPasswordByUserName(string username);
    }

}