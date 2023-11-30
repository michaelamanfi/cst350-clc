using Microsoft.AspNetCore.Mvc;
using Minesweeper.Models;
using Minesweeper.Service;

namespace Minesweeper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Minesweeper.Models; // Assuming UserModel is in this namespace

    /// <summary>
    /// Controller for handling user login.
    /// </summary>
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/>.
        /// </summary>
        /// <param name="authenticationService">The authentication service for handling user login operations.</param>
        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Displays the initial login form.
        /// </summary>
        /// <returns>The login view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Processes the user login request.
        /// </summary>
        /// <param name="user">The user model containing the login information.</param>
        /// <returns>
        /// The LoginSuccess view if the user is authenticated;
        /// otherwise, the LoginFailure view.
        /// </returns>
        /// <remarks>
        /// This method authenticates the user using the provided credentials.
        /// If the credentials are valid, the LoginSuccess view is returned.
        /// If the credentials are invalid, the LoginFailure view is returned.
        /// </remarks>
        public IActionResult ProcessLogin(UserModel user)
        {
            bool valid = _authenticationService.Authenticate(user);
            if (valid)
                return View("LoginSuccess", user);
            else
                return View("LoginFailure", user);
        }
    }

}
