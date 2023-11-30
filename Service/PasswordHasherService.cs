namespace Minesweeper.Service
{
    using Microsoft.AspNetCore.Identity;
    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly IPasswordHasher<object> _passwordHasher;

        public PasswordHasherService(IPasswordHasher<object> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Hashes a password.
        /// </summary>
        /// <param name="password">The plaintext password to hash.</param>
        /// <returns>The hashed password.</returns>
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        /// <summary>
        /// Verifies a plaintext password against a hashed password.
        /// </summary>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <param name="providedPassword">The plaintext password to verify.</param>
        /// <returns>True if the password is correct; otherwise, false.</returns>
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }

}
