using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Common
{
    /// <summary>
    /// This class represents a board of a particular size.
    /// </summary>
    public class Board<T> where T : ButtonModel
    {
        /// <summary>
        /// Initialize the grid, the size of the game, and the percentage live bombs.
        /// </summary>
        /// <param name="size">The size of the game.</param>
        public Board(int size)
        {
            this.Size = size;

            //Initialize the grid
            this.Grid = new ButtonModel[size, size];

            Iterator.ForEachContinueOnFalse(size, (int r, int c) =>
            {
                ButtonModel button = (ButtonModel)Activator.CreateInstance(typeof(T));
                button.ButtonState = (int)ButtonType.BLUE;
                button.Row = r;
                button.Column = c;
                this.Grid[r, c] = button;
            }, null);
        }
        /// <summary>
        /// Message to display.
        /// </summary>
        public string SuccessMessage { get; set; }
        /// <summary>
        /// Determines if game failed.
        /// </summary>
        public bool Failed { get; set; }
        /// <summary>
        /// Determines if game won.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Determines if initialized.
        /// </summary>
        public bool Initialized { get; set; }
        /// <summary>
        /// The name of the current player.
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// The elapsed time playing the game.
        /// </summary>
        public TimeSpan Elapsed { get; set; }

        /// <summary>
        /// The size of the game board (i.e., the grid).
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// The grid of the game board.
        /// </summary>
        public ButtonModel[,] Grid { get; set; }

        /// <summary>
        /// The percentage of buttons that should be set to live.
        /// </summary>
        public int Difficulty { get; set; }

        /// <summary>
        ///  Randomly initialize the grid with live bombs utilizing the Difficult property.        
        /// </summary>
        public void SetupLiveNeighbors()
        {
            //Calculate the number of lives bomb.
            int numberOfBombs = (int)Math.Ceiling((decimal)(int)Math.Pow(Size, 2) * ((decimal)Difficulty / 100));

            List<ButtonModel> buttons = new List<ButtonModel>();

            Iterator.ForEachContinueOnFalse(this.Size, (int r, int c) =>
            {
                ButtonModel button = this.Grid[r, c];
                buttons.Add(button);
            }, null);

            //Initialize random value generator.
            Random random = new Random((int)DateTime.Now.Ticks);

            //Randomly assign live bombs to buttons on the grid.
            while (numberOfBombs > 0)
            {
                int randomIndex = random.Next(1, buttons.Count);
                ButtonModel button = buttons[randomIndex];
                button.Live = true;

                //Remove button from consideration
                buttons.RemoveAt(randomIndex);

                numberOfBombs--;
            }
        }

        /// <summary>
        /// Calculate the neighbors with live bombs for every button.
        /// </summary>
        public void CalculateLiveNeighbors()
        {
            Iterator.ForEachContinueOnFalse(this.Size, (int r, int c) =>
            {
                ButtonModel button = this.Grid[r, c];
                button.LiveNeighbors = GetLiveNeighbors(button);

            }, null);
        }

        /// <summary>
        /// Calculates the number of neighbor buttons with live bombs.
        /// </summary>
        /// <param name="currentButton">The button to calculate the live neighbors for.</param>
        /// <returns>The number of neighbor buttons with live bombs.</returns>
        public int GetLiveNeighbors(ButtonModel currentButton)
        {
            int neighborsWithLiveBombs = currentButton.Live ? 1 : 0;

            int r = currentButton.Row;
            int c = currentButton.Column;

            //Get the top button.
            ButtonModel button = this.GetButtonByLocation(r - 1, c);
            if(button != null && button.Live)
                neighborsWithLiveBombs++;

            //Get the top right button.
            button = this.GetButtonByLocation(r - 1, c + 1);
            if (button != null && button.Live)
                neighborsWithLiveBombs++;

            //Get the left button.
            button = this.GetButtonByLocation(r, c + 1);
            if (button != null && button.Live)
                neighborsWithLiveBombs++;

            //Get the bottom right button.
            button = this.GetButtonByLocation(r + 1, c + 1);
            if (button != null && button.Live)
                neighborsWithLiveBombs++;

            //Get the bottom button.
            button = this.GetButtonByLocation(r + 1, c);
            if (button != null && button.Live)
                neighborsWithLiveBombs++;

            //Get the bottom left button.
            button = this.GetButtonByLocation(r + 1, c - 1);
            if (button != null && button.Live)
                neighborsWithLiveBombs++;

            //Mark the right button.
            button = this.GetButtonByLocation(r, c - 1);
            if (button != null && button.Live)
                neighborsWithLiveBombs++;

            //Get the top left button.
            button = this.GetButtonByLocation(r - 1, c - 1);
            if (button != null && button.Live)
                neighborsWithLiveBombs++;

            return neighborsWithLiveBombs;
        }


        /// <summary>
        /// Determine if all non-bomb buttons visited.
        /// </summary>
        /// <returns>T/F all non-bomb buttons visited.</returns>
        public bool AllNonBombButtonsVisited()
        {
            return Iterator.ForEachTermiateOnFalse(this.Size,null,(int r, int c) =>
             {
                 ButtonModel button = this.Grid[r, c];
                 if (!button.Live && !button.Visited)
                     return false;

                 return true;
             });

        }

        /// <summary>
        /// Retrieves a button.
        /// </summary>
        /// <param name="row">Specifies the row of the button.</param>
        /// <param name="col">Specifies the column of the button.</param>
        /// <returns></returns>
        public T GetButtonByLocation(int row, int col)
        {
            if (this.IsButtonValid(row, col))
                return (T)this.Grid[row, col];

            return null;
        }

        /// <summary>
        /// Retrieves all buttons on the grid.
        /// </summary>
        /// <returns>Buttons in an array</returns>
        public List<T> GetButtons()
        {
            int gridSize = this.Grid.GetLength(0);
            List<T> buttonList = new List<T>();

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    buttonList.Add(this.GetButtonByLocation(i, j));
                }
            }

            return buttonList;
        }

        /// <summary>
        /// Determines if the row and column are within the grid.
        /// </summary>
        /// <param name="row">Specifies the row</param>
        /// <param name="col">Specifies the column</param>
        /// <returns></returns>
        public bool IsButtonValid(int row, int col)
        {
            return row < this.Size && col < this.Size && row >= 0 && col >= 0;
        }

        /// <summary>
        /// Use recursion to flood fill buttons.
        /// </summary>
        /// <param name="row">Specifies the row of the current button.</param>
        /// <param name="col">Specifies the column of the current button.</param>
        public void FloodFill(int row, int col)
        {
            //Button is not valid, so ignore
            if (!IsButtonValid(row, col))
                return;

            //Button already visited, so ignore
            if (Grid[row, col].Visited)
                return;

            //Mark button as visited
            Grid[row, col].Visited = true;

            //Button has live neighbors including self, so ignore
            if (Grid[row, col].LiveNeighbors > 0)
                return;            

            //Go South and continue floodfill
            if (IsButtonValid(row, col + 1))
                FloodFill(row, col + 1);

            //Go West and continue floodfill
            if (IsButtonValid(row - 1, col))
                FloodFill(row - 1, col);

            //Go North and continue floodfill
            if (IsButtonValid(row, col - 1))
                FloodFill(row, col - 1);

            //Go East and continue floodfill
            if (IsButtonValid(row + 1, col))
                FloodFill(row + 1, col);

            //Go South-East and continue floodfill
            if (IsButtonValid(row + 1, col + 1))
                FloodFill(row + 1, col + 1);

            //Go South-West and continue floodfill
            if (IsButtonValid(row - 1, col + 1))
                FloodFill(row - 1, col + 1);

            //Go North-East and continue floodfill
            if (IsButtonValid(row + 1, col - 1))
                FloodFill(row + 1, col - 1);

            //Go North-West and continue floodfill
            if (IsButtonValid(row - 1, col - 1))
                FloodFill(row - 1, col - 1);

        }
    }
}
