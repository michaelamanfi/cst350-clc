using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Common
{
    /// <summary>
    /// This class represents a cell in the grid.
    /// </summary>
    public class Cell
    {
        public Cell()
        {
            this.Row = -1;
            this.Column = -1;
            this.Visited = false;
            this.Live = false;
            this.LiveNeighbors = 0;
        }

        /// <summary>
        /// The row location of the cell.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// The colulmn location of the cell.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Whether or not a cell has been visited.
        /// </summary>
        public bool Visited { get; set; }

        /// <summary>
        /// Whether or not a cell is live.
        /// </summary>
        public bool Live { get; set; }

        /// <summary>
        /// The number live neighbors to the cell.
        /// </summary>
        public int LiveNeighbors { get; set; }

        /// <summary>
        /// Reset the Visited, Live, and LiveNeighbors property values to their defaults.
        /// </summary>
        public void Initialize()
        {
            this.Visited = false;
            this.Live = false;
            this.LiveNeighbors = 0;
        }
        /// <summary>
        /// Returns a string output for the cell.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0},{1}", this.Row, this.Column);
        }
    }
}
