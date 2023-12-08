using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minesweeper.Common;
using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper.Controllers
{
    public class HomeController : Controller
    {              
        private readonly IGameService _gameService;
        public HomeController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Load and initialize the game board.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {            
            var buttons = _gameService.GetAllButtons();

            // Reset the game if not already initialized.
            if (!_gameService.IsInitialized())
            {
                this._gameService.ResetGame();
            }

            // Send the button list to the "Index" page
            return View("Index", buttons);
        }

        /// <summary>
        /// Reset the game.
        /// </summary>
        /// <returns></returns>
        public IActionResult Reset()
        {
            //Reset the game.
            this._gameService.ResetGame();

            // Get the buttons to display.
            var buttons = this._gameService.GetAllButtons();

            // Send the button list to the "Index" page.
            return View("Index", buttons);
        }

        /// <summary>
        /// Handles each button's right click.
        /// </summary>
        /// <param name="buttonNumber"></param>
        /// <returns></returns>
        public IActionResult HandleButtonClick(string buttonNumber)
        {
            //Check the game status
            bool success = this._gameService.GetSuccessStatus();
            bool failure = this._gameService.GetFailureStatus();    
            if (success || failure)
            {
                //Set the game play message to display to the user.
                ViewBag.SuccessMessage = this._gameService.GetGamePlayMessage();

                // Send the button list to the "Index" page.
                var allButtons = this._gameService.GetAllButtons();
                return View("Index", allButtons);
            }

            string[] location = buttonNumber.Split(',');

            // Convert from strings to ints.
            int row = int.Parse(location[0]);
            int col = int.Parse(location[1]);

            var currentButton = this._gameService.GetButton(row, col);

            // Get the buttons.
            var buttons = this._gameService.GetAllButtons();

            // Ignore the button click, if button is already visited.
            if (currentButton.Visited)
                return View("Index", buttons);

            if (currentButton.Live)
            {
                // Hit a bomb, reveal bombs.
                this._gameService.UpdateBoardEndOfGame(false);

                // Display the failed game results.                
                ViewBag.SuccessMessage = this._gameService.GetGameFailedMessage();     
            }
            else
            {
                // Apply the flood fill to the game faster.
                this._gameService.FillAdjacentCells(row, col);

                // Update the buttons
                this._gameService.UpdateBoardDuringGame();

                //Check if is a successfull (i.e., all non-bomb cells visited).
                if (this._gameService.IsGameSuccess())
                {
                    //Game won
                    this._gameService.UpdateBoardEndOfGame(true);

                    //TODO: Save play state.

                    //Display the results.                    
                    ViewBag.SuccessMessage = this._gameService.GetGameSuccessMessage();
                }
            }

            // re-display the buttons
            return View("Index", buttons);
        }

        /// <summary>
        /// Error action.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
