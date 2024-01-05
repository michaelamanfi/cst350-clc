using System;

namespace Minesweeper.Models
{
    public class UserGameModel
    {
        // Properties from UserModel
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string State { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }

        // Properties from GameModel
        public int GameId { get; set; }
        public int StatusId { get; set; }
        public string Json { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string DisplayName { get; set; }

        // Additional properties can be added as needed
    }
}
