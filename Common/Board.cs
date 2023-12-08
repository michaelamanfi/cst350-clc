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
    public class Board<T> where T : Cell
    {
        /// <summary>
        /// Initialize the grid, the size of the game, and the percentage live bombs.
        /// </summary>
        /// <param name="size">The size of the game.</param>
        public Board(int size)
        {
            this.Size = size;

            //Initialize the cells in the grid
            this.Grid = new Cell[size, size];

            Iterator.ForEachContinueOnFalse(size, (int r, int c) =>
            {
                Cell cell = (Cell)Activator.CreateInstance(typeof(T));
                cell.Row = r;
                cell.Column = c;
                this.Grid[r, c] = cell;
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
        public Cell[,] Grid { get; set; }

        /// <summary>
        /// The percentage of cells that should be set to live.
        /// </summary>
        public int Difficulty { get; set; }

        /// <summary>
        ///  Randomly initialize the grid with live bombs utilizing the Difficult property.        
        /// </summary>
        public void SetupLiveNeighbors()
        {
            //Calculate the number of lives bomb.
            int numberOfBombs = (int)Math.Ceiling((decimal)(int)Math.Pow(Size, 2) * ((decimal)Difficulty / 100));

            List<Cell> cells = new List<Cell>();

            Iterator.ForEachContinueOnFalse(this.Size, (int r, int c) =>
            {
                Cell cell = this.Grid[r, c];
                cells.Add(cell);
            }, null);

            //Initialize random value generator.
            Random random = new Random((int)DateTime.Now.Ticks);

            //Randomly assign live bombs to cells in the grid.
            while (numberOfBombs > 0)
            {
                int randomIndex = random.Next(1, cells.Count);
                Cell cell = cells[randomIndex];
                cell.Live = true;

                //Remove cell from consideration
                cells.RemoveAt(randomIndex);

                numberOfBombs--;
            }
        }

        /// <summary>
        /// Calculate the neighbors with live bombs for every cell.
        /// </summary>
        public void CalculateLiveNeighbors()
        {
            Iterator.ForEachContinueOnFalse(this.Size, (int r, int c) =>
            {
                Cell cell = this.Grid[r, c];
                cell.LiveNeighbors = GetLiveNeighbors(cell);

            }, null);
        }

        /// <summary>
        /// Calculates the number of neighbor cells with live bombs.
        /// </summary>
        /// <param name="cell">The cell to calculate the live neighbors for.</param>
        /// <returns>The number of neighbor cells with live bombs.</returns>
        public int GetLiveNeighbors(Cell currentCell)
        {
            /*
             * Below is the best way to visualize the cell ([r, c]) and the cell locations around it.
             [r-1,c-1] [r-1,c]  [r-1,c+1]
               [r,c-1]  [r,c]   [r,c+1]
             [r+1,c-1] [r+1,c]  [r+1,c+1]
             */

            int neighborsWithLiveBombs = currentCell.Live ? 1 : 0;

            int r = currentCell.Row;
            int c = currentCell.Column;

            //Get the top cell.
            Cell cell = this.GetCell(r - 1, c);
            if(cell != null && cell.Live)
                neighborsWithLiveBombs++;

            //Get the top right cell.
            cell = this.GetCell(r - 1, c + 1);
            if (cell != null && cell.Live)
                neighborsWithLiveBombs++;

            //Get the left cell.
            cell = this.GetCell(r, c + 1);
            if (cell != null && cell.Live)
                neighborsWithLiveBombs++;

            //Get the bottom right cell.
            cell = this.GetCell(r + 1, c + 1);
            if (cell != null && cell.Live)
                neighborsWithLiveBombs++;

            //Get the bottom cell.
            cell = this.GetCell(r + 1, c);
            if (cell != null && cell.Live)
                neighborsWithLiveBombs++;

            //Get the bottom left cell.
            cell = this.GetCell(r + 1, c - 1);
            if (cell != null && cell.Live)
                neighborsWithLiveBombs++;

            //Mark the right cell.
            cell = this.GetCell(r, c - 1);
            if (cell != null && cell.Live)
                neighborsWithLiveBombs++;

            //Get the top left cell.
            cell = this.GetCell(r - 1, c - 1);
            if (cell != null && cell.Live)
                neighborsWithLiveBombs++;

            return neighborsWithLiveBombs;
        }


        /// <summary>
        /// Determine if all non-bomb cells visited.
        /// </summary>
        /// <returns>T/F all non-bomb cells visited.</returns>
        public bool AllNonBombCellsVisited()
        {
            return Iterator.ForEachTermiateOnFalse(this.Size,null,(int r, int c) =>
             {
                 Cell cell = this.Grid[r, c];
                 if (!cell.Live && !cell.Visited)
                     return false;

                 return true;
             });

        }

        /// <summary>
        /// Retrieves a cell.
        /// </summary>
        /// <param name="row">Specifies the row of the cell.</param>
        /// <param name="col">Specifies the column of the cell.</param>
        /// <returns></returns>
        public T GetCell(int row, int col)
        {
            if (this.IsCellValid(row, col))
                return (T)this.Grid[row, col];

            return null;
        }

        /// <summary>
        /// Retrieves all cells in the grid.
        /// </summary>
        /// <returns>Cell in an array</returns>
        public List<T> GetCells()
        {
            int gridSize = this.Grid.GetLength(0);
            List<T> buttonList = new List<T>();

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    buttonList.Add(this.GetCell(i, j));
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
        public bool IsCellValid(int row, int col)
        {
            return row < this.Size && col < this.Size && row >= 0 && col >= 0;
        }

        /// <summary>
        /// Use recursion to flood fill cells.
        /// </summary>
        /// <param name="row">Specifies the row of the current cell.</param>
        /// <param name="col">Specifies the column of the current cell.</param>
        public void FloodFill(int row, int col)
        {
            //Cell is not valid, so ignore
            if (!IsCellValid(row, col))
                return;

            //Cell already visited, so ignore
            if (Grid[row, col].Visited)
                return;

            //Mark cell as visited
            Grid[row, col].Visited = true;

            //Cell has live neighbors including self, so ignore
            if (Grid[row, col].LiveNeighbors > 0)
                return;            

            //Go South and continue floodfill
            if (IsCellValid(row, col + 1))
                FloodFill(row, col + 1);

            //Go West and continue floodfill
            if (IsCellValid(row - 1, col))
                FloodFill(row - 1, col);

            //Go North and continue floodfill
            if (IsCellValid(row, col - 1))
                FloodFill(row, col - 1);

            //Go East and continue floodfill
            if (IsCellValid(row + 1, col))
                FloodFill(row + 1, col);

            //Go South-East and continue floodfill
            if (IsCellValid(row + 1, col + 1))
                FloodFill(row + 1, col + 1);

            //Go South-West and continue floodfill
            if (IsCellValid(row - 1, col + 1))
                FloodFill(row - 1, col + 1);

            //Go North-East and continue floodfill
            if (IsCellValid(row + 1, col - 1))
                FloodFill(row + 1, col - 1);

            //Go North-West and continue floodfill
            if (IsCellValid(row - 1, col - 1))
                FloodFill(row - 1, col - 1);

        }
    }
}
