using Minesweeper.Common;
using Minesweeper.Models;
using System.Collections.Generic;

namespace Minesweeper
{
    /// <summary>
    /// Defines the core functionalities for the Minesweeper game service.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Deletes a game record from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the game to delete.</param>
        void DeleteGame(int id);

        /// <summary>
        /// Checks if the current context has a valid game ID.
        /// </summary>
        /// <returns>True if the current context has a valid game ID; otherwise, false.</returns>
        /// <remarks>
        /// This method typically checks if the game ID in the current context (such as a session or a request) 
        /// is valid. The implementation details may vary depending on how the game ID is stored and accessed.
        /// </remarks>
        bool HasValidGameId();

        /// <summary>
        /// Updates the status of an existing game in the database.
        /// </summary>
        /// <param name="gameStatus">The new status to be set for the game.</param>
        /// <remarks>
        /// This method is used to update the status of a game, such as marking it as completed or in progress. 
        /// The <paramref name="gameStatus"/> parameter should contain the new status to be applied to the game.
        /// </remarks>
        void UpdateGame(GameStatus gameStatus);

        /// <summary>
        /// Retrieves a list of all games.
        /// </summary>
        /// <returns>A list of GameModel representing all games.</returns>
        /// <remarks>
        /// This method fetches all games from the data access layer, and then projects
        /// them into GameModel instances. It includes the game ID, display name, creation and 
        /// modification dates, game status, and the full name of the user associated with each game.
        /// </remarks>
        List<GameModel> GetAllGames();

        /// <summary>
        /// Retrieves a specific game by its unique identifier.
        /// </summary>
        /// <param name="gameId">The unique identifier of the game.</param>
        /// <returns>A GameModel representing the game if found; otherwise, null.</returns>
        /// <remarks>
        /// This method fetches a single game based on the provided game ID. It constructs
        /// a GameModel that includes the game's ID, display name, creation and modification dates,
        /// game status, and the full name of the associated user.
        /// </remarks>
        GameModel GetGameById(int gameId);

        /// <summary>
        /// Saves the current state of a game associated with a user.
        /// </summary>
        /// <param name="userName">The username associated with the game.</param>
        /// <param name="gameModel">The game model containing the game's data.</param>
        /// <remarks>
        /// This method retrieves the user information based on the provided userName,
        /// creates a new GameDbModel object with the game's state and associated user data,
        /// and then saves this data to the database.
        /// </remarks>
        void Save(string userName, GameModel gameModel);

        /// <summary>
        /// Opens and initializes a game from a stored game state.
        /// </summary>
        /// <param name="gameModel">The game model containing the game's identification data.</param>
        /// <remarks>
        /// This method retrieves the stored game state using the game's ID from the database,
        /// then deserializes the JSON data into a Board object.
        /// It's assumed that the 'Json' field in the stored game state represents the serialized board.
        /// </remarks>
        void Open(GameModel gameModel);

        /// <summary>
        /// Checks if the game has been successfully completed.
        /// </summary>
        /// <returns>True if the game is successfully completed, otherwise false.</returns>
        bool IsGameSuccess();

        /// <summary>
        /// Fills adjacent buttons around a given button location if conditions are met.
        /// </summary>
        /// <param name="row">Row index of the button.</param>
        /// <param name="col">Column index of the button.</param>
        void FillAdjacentButtons(int row, int col);

        /// <summary>
        /// Retrieves the state of a specific button.
        /// </summary>
        /// <param name="row">Row index of the button.</param>
        /// <param name="col">Column index of the button.</param>
        /// <returns>The model of the requested button.</returns>
        Models.ButtonModel GetButton(int row, int col);

        /// <summary>
        /// Determines if the game board has been initialized.
        /// </summary>
        /// <returns>True if the game board is initialized, otherwise false.</returns>
        bool IsInitialized();

        /// <summary>
        /// Retrieves all buttons of the game board.
        /// </summary>
        /// <returns>A list containing models of all buttons on the board.</returns>
        List<Models.ButtonModel> GetAllButtons();

        /// <summary>
        /// Gets the failure status of the game.
        /// </summary>
        /// <returns>True if the game is in a failed state, otherwise false.</returns>
        bool GetFailureStatus();

        /// <summary>
        /// Gets the success status of the game.
        /// </summary>
        /// <returns>True if the game is in a successful state, otherwise false.</returns>
        bool GetSuccessStatus();

        /// <summary>
        /// Resets the game to its initial state.
        /// </summary>
        void ResetGame();

        /// <summary>
        /// Retrieves the success message for the game.
        /// </summary>
        /// <returns>Success message string.</returns>
        string GetGamePlayMessage();

        /// <summary>
        /// Retrieves the game failed message.
        /// </summary>
        /// <returns>Game failed message string.</returns>
        string GetGameFailedMessage();

        /// <summary>
        /// Retrieves the game success message.
        /// </summary>
        /// <returns>Game success message string.</returns>
        string GetGameSuccessMessage();

        /// <summary>
        /// Updates the state of a specific button.
        /// </summary>
        /// <param name="button">The button to update.</param>
        void UpdateButton(ButtonModel button);

        /// <summary>
        /// Updates the board's state during the game after each move.
        /// </summary>
        void UpdateBoardDuringGame();

        /// <summary>
        /// Updates the board's state at the end of the game.
        /// </summary>
        /// <param name="winner">Indicates if the game ended in a win.</param>
        void UpdateBoardEndOfGame(bool winner);

        /// <summary>
        /// Game ended.
        /// </summary>
        /// <returns></returns>
        bool GameEnded();
    }
}
