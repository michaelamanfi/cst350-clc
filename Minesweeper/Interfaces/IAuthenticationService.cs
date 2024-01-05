using Minesweeper.Models;

namespace Minesweeper
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="passwordHasherService">The service used for hashing and verifying passwords.</param>
        /// <param name="userService">The service used for retrieving user data.</param>
        /// <remarks>
        /// The AuthenticationService is responsible for authenticating users based on their username and password.
        /// It utilizes the IPasswordHasherService for password verification and the IUserService for retrieving user information.
        /// </remarks>
        bool Authenticate(UserModel user);
    }
}