using System;
using System.ComponentModel.DataAnnotations;

namespace Minesweeper.Models
{
    /// <summary>
    /// Represents a user in the Minesweeper application.
    /// Contains properties that correspond to the user's information.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Gets or sets the user's unique identifier.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// Must not be longer than 50 characters.
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// Must not be longer than 50 characters.
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's sex.
        /// Acceptable values are 'Male', 'Female', or 'Other'.
        /// </summary>
        [Required]
        [RegularExpression("Male|Female|Other", ErrorMessage = "Sex must be either 'Male', 'Female', or 'Other'.")]
        public string Sex { get; set; }

        /// <summary>
        /// Gets or sets the user's age.
        /// Must be a non-negative number.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Age must be a non-negative number.")]
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the user's state of residence.
        /// This field is optional and can be up to 50 characters long.
        /// </summary>
        [StringLength(50)]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// Must be a valid email format and not longer than 100 characters.
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Email address cannot be longer than 100 characters.")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the user's username.
        /// Must not be longer than 50 characters.
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// This field will store a hashed password and must not be longer than 255 characters.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Password cannot be longer than 255 characters.")]
        public string Password { get; set; }

        /// <summary>
        /// User info.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"UserId: {UserId} Username: {Username} Name: {FirstName} {LastName}";
        }
    }
}
