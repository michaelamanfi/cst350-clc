namespace Minesweeper
{
    /// <summary>
    /// Interface for a service providing password hashing and verification.
    /// </summary>
    public interface IPasswordHasherService
    {
        /// <summary>
        /// Hashes a plaintext password.
        /// </summary>
        /// <param name="password">The plaintext password to hash.</param>
        /// <returns>The hashed version of the password.</returns>
        /// <remarks>
        /// This method is used to convert a plaintext password into a hashed format for secure storage.
        /// The hashing algorithm used is defined in the implementation of this interface.
        /// </remarks>
        string HashPassword(string password);

        /// <summary>
        /// Verifies a plaintext password against a hashed password.
        /// </summary>
        /// <param name="hashedPassword">The hashed password to compare against.</param>
        /// <param name="providedPassword">The plaintext password to verify.</param>
        /// <returns>True if the plaintext password matches the hashed password; otherwise, false.</returns>
        /// <remarks>
        /// This method is used to compare a plaintext password with its hashed version, typically during user authentication processes.
        /// It should return true if the provided plaintext password, when hashed, matches the hashedPassword parameter.
        /// </remarks>
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }

}