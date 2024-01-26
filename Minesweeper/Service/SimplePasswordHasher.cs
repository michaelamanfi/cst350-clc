namespace Minesweeper.Service
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.AspNetCore.Identity;

    public class SimplePasswordHasher<T> : IPasswordHasher<T> where T : class
    {
        public string HashPassword(T user, string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a hexadecimal string.
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public PasswordVerificationResult VerifyHashedPassword(T user, string hashedPassword, string providedPassword)
        {
            // Hash the provided password and compare it to the stored hash.
            string hashedProvidedPassword = HashPassword(user, providedPassword);

            if (hashedProvidedPassword == hashedPassword)
            {
                return PasswordVerificationResult.Success;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }

}
