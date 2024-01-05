using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.Models;

namespace Minesweeper.Controllers
{

    /// <summary>
    /// Controller for handling user registration.
    /// </summary>
    ///
    [AllowAnonymous]
    public class RegistrationController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/>.
        /// </summary>
        /// <param name="userService">The user service for handling user-related operations.</param>
        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Displays the initial registration form.
        /// </summary>
        /// <returns>The registration view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Handles the user registration request.
        /// </summary>
        /// <param name="user">The user model containing the registration information.</param>
        /// <returns>
        /// The RegistrationSuccess view if registration is successful;
        /// otherwise, the RegistrationFailed view.
        /// </returns>
        /// <remarks>
        /// If the user already exists, the registration fails and the RegistrationFailed view is returned.
        /// If the user does not exist, the user is registered and the RegistrationSuccess view is returned.
        /// </remarks>
        public IActionResult RegisterUser(UserModel user)
        {
            if (_userService.UserExists(user.Username))
            {
                return View("RegistrationFailed", user);
            }
            else
            {
                // Register the user.
                _userService.RegisterUser(user);

                return View("RegistrationSuccess", user);
            }
        }
    }

}
