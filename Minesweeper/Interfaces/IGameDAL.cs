using Minesweeper.Models;
using System.Collections.Generic;

namespace Minesweeper
{
    /// <summary>
    /// Defines the contract for the Game Data Access Layer.
    /// Provides methods for managing game data in the database.
    /// </summary>
    public interface IGameDAL
    {
        /// <summary>
        /// Checks whether a game with the specified ID exists in the database.
        /// </summary>
        /// <param name="gameId">The ID of the game to check.</param>
        /// <returns>True if the game exists; otherwise, false.</returns>
        bool GameExists(int gameId);

        /// <summary>
        /// Adds a new game record to the database.
        /// </summary>
        /// <param name="game">The game details to be added.</param>
        void CreateGame(GameDbModel game);

        /// <summary>
        /// Deletes a game record from the database based on its ID.
        /// </summary>
        /// <param name="gameId">The ID of the game to delete.</param>
        void DeleteGame(int gameId);

        /// <summary>
        /// Retrieves a list of all games from the database.
        /// </summary>
        /// <returns>A list of games represented as <see cref="GameDbModel"/>.</returns>
        List<GameDbModel> GetAllGames();

        /// <summary>
        /// Retrieves a single game from the database by its ID.
        /// </summary>
        /// <param name="gameId">The ID of the game to retrieve.</param>
        /// <returns>The game details as a <see cref="GameDbModel"/>.</returns>
        GameDbModel GetGameById(int gameId);

        /// <summary>
        /// Updates an existing game's details in the database.
        /// </summary>
        /// <param name="game">The updated game details.</param>
        void UpdateGame(GameDbModel game);
    }
}
