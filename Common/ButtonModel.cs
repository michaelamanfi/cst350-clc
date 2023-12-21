using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Common
{
    /// <summary>
    /// This class represents a button in the grid.
    /// </summary>
    public class ButtonModel
    {
        public ButtonModel()
        {
            this.Row = -1;
            this.Column = -1;
            this.Visited = false;
            this.Live = false;
            this.LiveNeighbors = 0;
        }

        public int ButtonPreviousState { get; set; }
        public int ButtonState { get; set; }
        /// <summary>
        /// The row location
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// The colulmn location
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Whether or not a button has been visited.
        /// </summary>
        public bool Visited { get; set; }

        /// <summary>
        /// Whether or not a button is live.
        /// </summary>
        public bool Live { get; set; }

        /// <summary>
        /// The number live neighbors to the button.
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
        /// Returns a string output for the button (button coordinates).
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}_{1}", this.Row, this.Column);
        }
    }
}
