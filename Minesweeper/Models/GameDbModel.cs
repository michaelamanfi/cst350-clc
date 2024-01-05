namespace Minesweeper.Models
{
    using System;

    /// <summary>
    /// Represents a game model corresponding to the Game table in the database.
    /// </summary>
    public class GameDbModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the game.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the game.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the status ID of the game.
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the JSON data representing the game state.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the game was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the game was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the display name of the game.
        /// </summary>
        public string DisplayName { get; set; }
    }
}
