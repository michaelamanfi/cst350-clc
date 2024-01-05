using Microsoft.AspNetCore.Http;
using Minesweeper.DAL;
using Minesweeper.Models;
using System.Security.Claims;

namespace Minesweeper.Service
{
    public class UserService : IUserService
    {
        private readonly IUserDAL _userDAL;
        private readonly IPasswordHasherService _passwordHasherService;

        /// <summary>
        /// Initializes a new instance of the UserService class.
        /// </summary>
        /// <param name="userDAL">An instance of UserDAL for database operations.</param>
        /// /// <param name="passwordHasherService">An instance of PasswordHasherService for password hashing operations.</param>
        public UserService(IUserDAL userDAL, IPasswordHasherService passwordHasherService)
        {
            this._userDAL = userDAL;
            this._passwordHasherService = passwordHasherService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user details to register.</param>
        public void RegisterUser(UserModel user)
        {
            user.Password = this._passwordHasherService.HashPassword(user.Password);
            _userDAL.CreateUser(user);
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The user details if found, otherwise null.</returns>
        public UserModel GetUser(int userId)
        {
            return _userDAL.GetUser(userId);
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user details if found, otherwise null.</returns>
        public UserModel GetUser(string username)
        {
            return _userDAL.GetUser(username);
        }
        /// <summary>
        /// Checks if a username already exists.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username exists; otherwise, false.</returns>
        public bool UserExists(string username)
        {
            return _userDAL.UserExists(username);
        }
        /// <summary>
        /// Updates user information.
        /// </summary>
        /// <param name="user">The updated user details.</param>
        public void UpdateUser(UserModel user)
        {
            _userDAL.UpdateUser(user);
        }

        /// <summary>
        /// Deletes a user from the system.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        public void DeleteUser(int userId)
        {
            _userDAL.DeleteUser(userId);
        }

        /// <summary>
        /// Retrieves a user's password based on username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user password.</returns>
        public string GetPasswordByUserName(string username)
        {
            return _userDAL.GetPasswordByUserName(username);
        }
    }

}
