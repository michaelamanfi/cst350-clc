using Minesweeper.Common;
using Minesweeper.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
namespace Minesweeper.Service
{
    /// <summary>
    /// Provides services and game logic for the Minesweeper game.
    /// </summary>
    public class GameService : IGameService
    {
        const int GridSize = 5;
        private Board<Models.ButtonModel> board;
        private readonly IGameDAL _gameDAL;
        private readonly IUserGameDAL  _userGameDAL;
        private readonly IUserService _userService;
        public GameService(IGameDAL gameDAL, IUserGameDAL userGameDAL, IUserService userService)
        {
            this._userGameDAL = userGameDAL;
            this._gameDAL = gameDAL;
            this._userService = userService;
            board = new Board<Models.ButtonModel>(GridSize) { Difficulty = (int)DifficultyLevel.Moderate };
        }

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
        public void Save(string userName, GameModel gameModel)
        {
            UserModel userModel = _userService.GetUser(userName);
            _gameDAL.CreateGame(new GameDbModel
            {
                DisplayName = gameModel.Name,
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now,
                StatusId = (int)GameStatus.InProgress,
                UserId = userModel.UserId,
                Json = JsonConvert.SerializeObject(board)
            });
        }

        /// <summary>
        /// Opens and initializes a game from a stored game state.
        /// </summary>
        /// <param name="gameModel">The game model containing the game's identification data.</param>
        /// <remarks>
        /// This method retrieves the stored game state using the game's ID from the database,
        /// then deserializes the JSON data into a Board object.
        /// It's assumed that the 'Json' field in the stored game state represents the serialized board.
        /// </remarks>
        public void Open(GameModel gameModel)
        {
            var game = _gameDAL.GetGameById(gameModel.ID);
            board = JsonConvert.DeserializeObject<Board<Models.ButtonModel>>(game.Json);
            board.GameId = game.GameId;
        }

        /// <summary>
        /// Retrieves a list of all games.
        /// </summary>
        /// <returns>A list of GameModel representing all games.</returns>
        /// <remarks>
        /// This method fetches all games from the data access layer, and then projects
        /// them into GameModel instances. It includes the game ID, display name, creation and 
        /// modification dates, game status, and the full name of the user associated with each game.
        /// </remarks>
        public List<GameModel> GetAllGames()
        {
            var gameCollection = _userGameDAL.GetAllGames();
            var games = from game in gameCollection
                        select new GameModel
                        {
                            ID = game.GameId,
                            Name = game.DisplayName,
                            CreatedDate = game.CreatedDate,
                            ModifiedDate = game.ModifiedDate,
                            Status = ((GameStatus)game.StatusId).ToString(),
                            UserFullName = $"{game.FirstName} {game.LastName}"
                        };

            return new List<GameModel>(games);
        }

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
        public GameModel GetGameById(int gameId)
        {
            var game = _userGameDAL.GetGameById(gameId);
            if(game == null)   
                return null;

            return new GameModel
            {
                ID = game.GameId,
                Name = game.DisplayName,
                CreatedDate = game.CreatedDate,
                ModifiedDate = game.ModifiedDate,
                Status = ((GameStatus)game.StatusId).ToString(),
                UserFullName = $"{game.FirstName} {game.LastName}"
            };
        }

        public void DeleteGame(int id)
        {
            this._gameDAL.DeleteGame(id);
        }
        public bool HasValidGameId()
        {
            var game = _userGameDAL.GetGameById(board.GameId);
            return (game != null);
        }

        /// <summary>
        /// Saves the current state of a game associated with a user.
        /// </summary>
        /// <param name="gameStatus">Status of the game.</param>
        public void UpdateGame(GameStatus gameStatus)
        {
            _gameDAL.UpdateGame(new GameDbModel
            {
                GameId = board.GameId,
                ModifiedDate = System.DateTime.Now,
                StatusId = (int)gameStatus,
                Json = JsonConvert.SerializeObject(board)
            });
        }

        /// <summary>
        /// Resets and initializes the game board for a new game.
        /// This involves setting up the board, placing live bombs,
        /// and calculating live neighbors.
        /// </summary>
        public void ResetGame()
        {
            board = new Board<Models.ButtonModel>(GridSize) { Difficulty = (int)DifficultyLevel.Moderate };
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
            this.SetSuccessMessage("You hit a Bomb! You failed!");
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
            this.SetSuccessMessage("Congratulations! You won!!!");
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
        public void UpdateButton(ButtonModel button)
        {
            if (button.Visited)
            {
                button.ButtonState = (int)ButtonType.GREEN;
            }
            else
            {
                // Update the button to reflect not visited.
                button.ButtonState = (int)ButtonType.BLUE;
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
                    button.ButtonState = (int)ButtonType.RED;
                }
            }, (int r, int c) =>
            {
                var button = this.board.Grid[r, c];
                if (button.Visited)
                    return false;

                return true;
            });
        }
        /// <summary>
        /// Check if the game ended in failure.
        /// </summary>
        /// <returns></returns>
        public bool GameEnded()
        {
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    var button = board.Grid[row, col];
                    if (button.ButtonState == (int)ButtonType.RED)
                        return true;
                }
            }
            return board.AllNonBombButtonsVisited();
        }
    }
}