﻿using Minesweeper.Common;
using Minesweeper.Models;
using System.Collections.Generic;

namespace Minesweeper.Service
{
    /// <summary>
    /// Provides services and game logic for the Minesweeper game.
    /// </summary>
    public class GameService : IGameService
    {
        const int GridSize = 9;
        private Board<Models.ButtonModel> board;
        public GameService()
        {
            board = new Board<Models.ButtonModel>(GridSize) { Difficulty = (int)DifficultyLevel.Easy };
        }

        /// <summary>
        /// Resets and initializes the game board for a new game.
        /// This involves setting up the board, placing live bombs,
        /// and calculating live neighbors.
        /// </summary>
        public void ResetGame()
        {
            board = new Board<Models.ButtonModel>(GridSize) { Difficulty = (int)DifficultyLevel.Easy };
            board.Success = false;
            board.Failed = false;
            board.SetupLiveNeighbors(); // Randomly assign live bombs
            board.CalculateLiveNeighbors(); // Calculate live neighbors
            board.Initialized = true;
        }

        /// <summary>
        /// Checks if the game board has been initialized.
        /// </summary>
        /// <returns>True if initialized, false otherwise.</returns>
        public bool IsInitialized()
        {
            return board.Initialized;
        }

        /// <summary>
        /// Returns a message indicating game failure.
        /// </summary>
        /// <returns>Game failed message.</returns>
        public string GetGameFailedMessage()
        {
            this.SetSuccessMessage("You failed!");
            this.SetSuccessStatus(false);
            this.SetFailureStatus(true);
            return this.GetGamePlayMessage();
        }

        /// <summary>
        /// Returns a message indicating game success.
        /// </summary>
        /// <returns>Game success message.</returns>
        public string GetGameSuccessMessage()
        {
            this.SetSuccessMessage("Congratulations! You won!!");
            this.SetSuccessStatus(true);
            this.SetFailureStatus(false);
            return this.GetGamePlayMessage();
        }

        /// <summary>
        /// Determines if the game has been successfully completed.
        /// </summary>
        /// <returns>True if game is successful, false otherwise.</returns>
        public bool IsGameSuccess()
        {
            return board.AllNonBombButtonsVisited();
        }

        /// <summary>
        /// Fills adjacent buttons starting from the specified button if conditions are met.
        /// </summary>
        /// <param name="row">Row index of the starting button.</param>
        /// <param name="col">Column index of the starting button.</param>
        public void FillAdjacentButtons(int row, int col)
        {
            board.FloodFill(row, col);
        }

        /// <summary>
        /// Retrieves a specific button.
        /// </summary>
        /// <param name="row">Row index of the button.</param>
        /// <param name="col">Column index of the button.</param>
        /// <returns>The model of the requested button.</returns>
        public Models.ButtonModel GetButton(int row, int col)
        {
            return board.GetButtonByLocation(row, col);
        }

        /// <summary>
        /// Retrieves all buttons on the game board.
        /// </summary>
        /// <returns>A list of models representing all buttons on the board.</returns>
        public List<Models.ButtonModel> GetAllButtons()
        {
            return board.GetButtons();
        }

        /// <summary>
        /// Sets the failure status of the game.
        /// </summary>
        /// <param name="status">True to set the game as failed, false otherwise.</param>
        private void SetFailureStatus(bool status)
        {
            board.Failed = status;
        }

        /// <summary>
        /// Gets the failure status of the game.
        /// </summary>
        /// <returns>True if the game has failed, false otherwise.</returns>
        public bool GetFailureStatus()
        {
            return board.Failed;
        }

        /// <summary>
        /// Sets the success status of the game.
        /// </summary>
        /// <param name="status">True to set the game as successful, false otherwise.</param>
        private void SetSuccessStatus(bool status)
        {
            board.Success = status;
        }

        /// <summary>
        /// Gets the success status of the game.
        /// </summary>
        /// <returns>True if the game is successful, false otherwise.</returns>
        public bool GetSuccessStatus()
        {
            return board.Success;
        }

        /// <summary>
        /// Sets the success message for the game.
        /// </summary>
        /// <param name="message">The success message to set.</param>
        private void SetSuccessMessage(string message)
        {
            board.SuccessMessage = message;
        }
        /// <summary>
        /// Gets the success message for the game.
        /// </summary>
        /// <returns>The current success message.</returns>
        public string GetGamePlayMessage()
        {
            return board.SuccessMessage;
        }

        /// <summary>
        /// Updates the state of a specific button.
        /// </summary>
        /// <param name="button">The button to update.</param>
        public void UpdateButton(Common.ButtonModel button)
        {
            if (button.Visited)
            {
                if (button.LiveNeighbors > 0)
                {
                    // Update the button to reflect the number of neighbors with bombs.
                    this.board.GetButtonByLocation(button.Row, button.Column).ButtonState = 1;
                }
                else
                {
                    // Update the button to reflect visited
                    this.board.GetButtonByLocation(button.Row, button.Column).ButtonState = 1;
                }
            }
            else
            {
                // Update the button to reflect not visited.
                this.board.GetButtonByLocation(button.Row, button.Column).ButtonState = 0;
            }
        }

        /// <summary>
        /// Update the game board display while the game is being played.
        /// </summary>
        public void UpdateBoardDuringGame()
        {
            Iterator.ForEachContinueOnFalse(GridSize, (int r, int c) =>
            {
                var button = this.board.Grid[r, c];
                this.UpdateButton(button);
            }, null);
        }

        /// <summary>
        /// Update the game board display at the end of the game.
        /// </summary>
        /// <param name="winner">Indicates if the game ended in a win.</param>
        public void UpdateBoardEndOfGame(bool winner)
        {
            Iterator.ForEachContinueOnFalse(GridSize, (int r, int c) =>
            {
                var button = this.board.Grid[r, c];
                if (button.Live)
                {
                    // Reveal updated button.
                    this.board.GetButtonByLocation(button.Row, button.Column).ButtonState = winner ? 3 : 2;
                }
            }, (int r, int c) =>
            {
                var button = this.board.Grid[r, c];
                if (button.Visited)
                    return false;

                return true;
            });
        }
    }
}