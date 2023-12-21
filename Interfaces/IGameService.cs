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
        void UpdateButton(Common.ButtonModel button);

        /// <summary>
        /// Updates the board's state during the game after each move.
        /// </summary>
        void UpdateBoardDuringGame();

        /// <summary>
        /// Updates the board's state at the end of the game.
        /// </summary>
        /// <param name="winner">Indicates if the game ended in a win.</param>
        void UpdateBoardEndOfGame(bool winner);

        bool GameEnded();
    }
}
