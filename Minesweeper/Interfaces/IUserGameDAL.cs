using Minesweeper.Models;
using System.Collections.Generic;

namespace Minesweeper
{
    public interface IUserGameDAL
    {
        /// <summary>
        /// Retrieves a list of all games along with their associated user details.
        /// </summary>
        /// <returns>A list of <see cref="UserGameModel"/> objects, each representing a game and its associated user details.</returns>
        /// <remarks>
        /// This method is used to fetch all games from the database, including information about the users who are associated with each game.
        /// The returned list includes comprehensive details for each game, as represented by the UserGameModel.
        /// </remarks>
        List<UserGameModel> GetAllGames();

        /// <summary>
        /// Retrieves a specific game and its associated user details by the game ID.
        /// </summary>
        /// <param name="gameId">The unique identifier of the game to retrieve.</param>
        /// <returns>A <see cref="UserGameModel"/> object containing details of the specified game and its associated user; returns null if the game is not found.</returns>
        /// <remarks>
        /// This method fetches detailed information about a specific game and the user associated with it, based on the provided game ID.
        /// If no game with the given ID is found in the database, the method returns null.
        /// </remarks>
        UserGameModel GetGameById(int gameId);
    }

}