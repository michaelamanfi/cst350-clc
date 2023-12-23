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
        /// <summary>
        /// Initialize the controller with IGameService. 
        /// </summary>
        /// <param name="gameService"></param>
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
            return Reset();
        }

        /// <summary>
        /// Display game success message.
        /// </summary>
        /// <returns></returns>
        public IActionResult Success()
        {
            return ProcessGame(true);
        }

        /// <summary>
        /// Display game failed message.
        /// </summary>
        /// <returns></returns>
        public IActionResult Failed()
        {
            return ProcessGame(false);
        }

        /// <summary>
        /// Display game output.
        /// </summary>
        /// <param name="success"></param>
        /// <returns></returns>
        public IActionResult ProcessGame(bool success)
        {
            //Reveal bombs.
            this._gameService.UpdateBoardEndOfGame(success);

            // Get the buttons to display.
            var buttons = this._gameService.GetAllButtons();

            if (success)
                ViewBag.SuccessMessage = this._gameService.GetGameSuccessMessage();
            else
                ViewBag.SuccessMessage = this._gameService.GetGameFailedMessage();

            // Send the button list to the "Index" page.
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
        { //Check the game status
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

            string[] location = buttonNumber.Split('_');

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
                this._gameService.FillAdjacentButtons(row, col);

                // Update the buttons
                this._gameService.UpdateBoardDuringGame();

                //Check if is a successfull (i.e., all non-bomb buttons visited).
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
        /// Retrieves a button.
        /// </summary>
        /// <param name="buttonNumber"></param>
        /// <returns></returns>
        public JsonResult GetButtonMetadata(string buttonNumber)
        {
            string[] location = buttonNumber.Split('_');

            // Convert from strings to ints.
            int row = int.Parse(location[0]);
            int col = int.Parse(location[1]);

            var currentButton = this._gameService.GetButton(row, col);
            bool gameEnded = this._gameService.GameEnded();
            bool gameSuccess = this._gameService.IsGameSuccess();

            //Game won
            if (gameSuccess)
                this._gameService.UpdateBoardEndOfGame(true);

            var response = new
            {
                live = currentButton.Live,
                visited = currentButton.Visited,
                ended = gameEnded,
                success = gameSuccess
            };

            return Json(response);
        }

        /// <summary>
        /// Update button on left-click.
        /// </summary>
        /// <param name="buttonNumber"></param>
        /// <returns></returns>
        public IActionResult ShowOneButton(string buttonNumber)
        {
            // Get the buttons.
            var buttons = this._gameService.GetAllButtons();
            string[] location = buttonNumber.Split('_');

            // Convert from strings to ints.
            int row = int.Parse(location[0]);
            int col = int.Parse(location[1]);

            var currentButton = this._gameService.GetButton(row, col);

            //Do nothing, it's a flag.
            if (currentButton.ButtonState == (int)ButtonType.FLAG)
                return PartialView(currentButton);

            //It's a live bomb
            if (currentButton.Live)
            {
                currentButton.Visited = true;
                currentButton.ButtonState = (int)ButtonType.RED;                
                return PartialView(currentButton);
            }                

            // We're vising the button.
            currentButton.ButtonState = (int)ButtonType.GREEN;
            currentButton.Visited = true;

            // Re-render the button that was clicked
            return PartialView(currentButton);
        }

        /// <summary>
        /// Update button on right-click with flag.
        /// </summary>
        /// <param name="buttonNumber"></param>
        /// <returns></returns>
        public IActionResult RightClickShowOneButton(string buttonNumber)
        {                        
            string[] location = buttonNumber.Split('_'); // Grid coordinates is in the form of x_y.

            // Convert from strings to ints.
            int row = int.Parse(location[0]);
            int col = int.Parse(location[1]);

            var currentButton = this._gameService.GetButton(row, col);

            // on right click always reset to 0.
            if (currentButton.ButtonState == (int)ButtonType.FLAG)
                currentButton.ButtonState = currentButton.ButtonPreviousState; // Restore the previous state
            else
            {
                currentButton.ButtonPreviousState = currentButton.ButtonState;  // Capture the previous state
                currentButton.ButtonState = (int)ButtonType.FLAG;
            }

            // re-display the button that was clicked
            return PartialView("ShowOneButton", currentButton);
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
