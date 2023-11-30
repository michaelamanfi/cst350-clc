using Minesweeper.Models;

namespace Minesweeper.Service
{
    /// <summary>
    /// Provides authentication services by verifying user credentials.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="passwordHasherService">The service used for hashing and verifying passwords.</param>
        /// <param name="userService">The service used for retrieving user data.</param>
        /// <remarks>
        /// The AuthenticationService is responsible for authenticating users based on their username and password.
        /// It utilizes the IPasswordHasherService for password verification and the IUserService for retrieving user information.
        /// </remarks>
        public AuthenticationService(IPasswordHasherService passwordHasherService, IUserService userService)
        {
            _passwordHasherService = passwordHasherService;
            _userService = userService;
        }

        /// <summary>
        /// Authenticates a user based on their provided credentials.
        /// </summary>
        /// <param name="user">The user model containing the user's credentials.</param>
        /// <returns>True if the user is successfully authenticated; otherwise, false.</returns>
        /// <remarks>
        /// The method retrieves the hashed password for the given username from the user service.
        /// It then uses the password hasher service to verify if the provided password matches the hashed password.
        /// If the user's username does not exist or the password does not match, the method returns false.
        /// </remarks>
        public bool Authenticate(UserModel user)
        {
            string hashedPassword = _userService.GetPasswordByUserName(user.Username);
            if (string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            return _passwordHasherService.VerifyPassword(hashedPassword, user.Password);
        }
    }

}
