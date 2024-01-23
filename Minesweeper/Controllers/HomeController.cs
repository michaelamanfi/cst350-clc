using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.Common;
using Minesweeper.Logger;
using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Minesweeper.Controllers
{
    public class HomeController : Controller
    {              
        private readonly IGameService _gameService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        /// <summary>
        /// Initialize the controller with IGameService. 
        /// </summary>
        /// <param name="gameService"></param>
        public HomeController(IUserService userService, IGameService gameService, ILogger logger)
        {
            _userService = userService;
               _gameService = gameService;
            _logger = logger;
        }
        /// <summary>
        /// Gets the username of the currently authenticated user.
        /// </summary>
        /// <returns>The username if the user is authenticated; otherwise, null.</returns>
        public string GetCurrentUsername()
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.FindFirst(ClaimTypes.Name)?.Value;
            }

            return null;
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

            _logger.Info(ViewBag.SuccessMessage);

            //Update game if it is a saved game.
            if (this._gameService.HasValidGameId())
            {
                if (success)
                    this._gameService.UpdateGame(GameStatus.Completed);
                else
                    this._gameService.UpdateGame(GameStatus.Failed);
            }

            // Send the button list to the "Index" page.
            return View("Index", buttons);
        }

        /// <summary>
        /// Reset the game.
        /// </summary>
        /// <returns></returns>
        public IActionResult Reset()
        {
            _logger.Info("Resetting the game");
            //Reset the game.
            this._gameService.ResetGame();

            // Get the buttons to display.
            var buttons = this._gameService.GetAllButtons();

            // Send the button list to the "Index" page.
            return View("Index", buttons);
        }

        /// <summary>
        /// Saves the current game state.
        /// </summary>
        /// <returns>A view showing either the updated game or the Save page if no valid game ID is present.</returns>
        /// <remarks>
        /// If a valid game ID exists, this method updates the game status to InProgress,
        /// retrieves all the game buttons, and displays them on the Index page.
        /// If no valid game ID is found, it redirects to the Save page.
        /// </remarks>
        public IActionResult Save()
        {
            if (_gameService.HasValidGameId())
            {
                _gameService.UpdateGame(GameStatus.InProgress);

                // Retrieve the updated buttons for display
                var buttons = _gameService.GetAllButtons();

                ViewBag.SuccessMessage = "Saved!";
                return View("Index", buttons);
            }
            else
            {

                bool gameEnded = this._gameService.GameEnded();
                if (gameEnded)
                {
                    var buttons = _gameService.GetAllButtons();
                    ViewBag.SuccessMessage = "Error: You cannot save a completed game.";

                    _logger.Error("You cannot save a completed game.");

                    return View("Index", buttons);
                }
                else
                {
                    return View("Save");
                }
            }
        }

        /// <summary>
        /// Displays all the games.
        /// </summary>
        /// <returns>A view showing all the games.</returns>
        public IActionResult Games()
        {
            _logger.Info("Displaying all games.");
            var games = _gameService.GetAllGames();
            return View("ViewGame", games);
        }

        /// <summary>
        /// Processes the saving of a game.
        /// </summary>
        /// <param name="model">The game model to save.</param>
        /// <returns>A view showing all games after processing the save.</returns>
        /// <remarks>
        /// This method handles the POST request to save a game. If the request is not POST,
        /// it simply returns the ViewGame view with all games. Otherwise, it saves the game
        /// and displays the updated list of games.
        /// </remarks>
        public IActionResult SaveGame(GameModel model)
        {
            if (!HttpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                var games = _gameService.GetAllGames();
                return View("ViewGame", games);
            }
            else
            {
                string user = GetCurrentUsername();
                _logger.Info($"Saving current game for {user}");

                _gameService.Save(user, model);

                var games = _gameService.GetAllGames();
                return View("ViewGame", games);
            }
        }

        /// <summary>
        /// Continues an existing game.
        /// </summary>
        /// <param name="id">The ID of the game to continue.</param>
        /// <returns>A view of the game to be continued.</returns>
        [HttpGet("ContinueGame/{id}")]
        public IActionResult ContinueGame(int id)
        {
            _gameService.Open(new GameModel { ID = id });

            var buttons = _gameService.GetAllButtons();
            return View("Index", buttons);
        }

        /// <summary>
        /// Deletes a game.
        /// </summary>
        /// <param name="id">The ID of the game to delete.</param>
        /// <returns>A view showing all games after deletion.</returns>
        [HttpGet("DeleteGame/{id}")]
        public IActionResult DeleteGame(int id)
        {
            _gameService.DeleteGame(id);

            var games = _gameService.GetAllGames();
            return View("ViewGame", games);
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
