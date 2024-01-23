namespace Minesweeper.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Minesweeper.Filters;
    using Minesweeper.Logger;
    using Minesweeper.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// Controller for handling user login.
    /// </summary>
    /// 
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly Minesweeper.IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/>.
        /// </summary>
        /// <param name="authenticationService">The authentication service for handling user login operations.</param>
        public LoginController(Minesweeper.IAuthenticationService authenticationService, IUserService userService, ILogger logger)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _logger = logger;
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
        [ServiceFilter(typeof(ProcessLoginLogActionFilter))]
        public async Task<IActionResult> ProcessLoginAsync(UserModel user)
        {
            bool valid = _authenticationService.Authenticate(user);
            if (valid)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                     IsPersistent = true,
                      AllowRefresh = true,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                //Save user data in the session.
                var userData = _userService.GetUser(user.Username);
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(userData));

                return RedirectToAction("Index", "Home");
            }
            else
                return View("LoginFailure", user);
        }

        /// <summary>
        /// Processes the user logout request.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            string userData = HttpContext.Session.GetString("user");

            if (!string.IsNullOrWhiteSpace(userData))
            {
                UserModel user = JsonConvert.DeserializeObject<UserModel>(userData);
                _logger.Info("Logged out user: " + user.ToString());
            }

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to home page or login page after logout
            return RedirectToAction("Index", "Home");
        }
    }

}
