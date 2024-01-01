using System;

namespace Minesweeper.Models
{
    /// <summary>
    /// This class represents a button in the grid.
    /// </summary>
    [Serializable]
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
        // Properties
        public string Id { get { return this.ToString(); } }

        // Constructor with parameters
        public ButtonModel(int row, int column, int buttonState) : this()
        {
            Row = row;
            Column = column;
            ButtonState = buttonState;
        }

        [Newtonsoft.Json.JsonProperty]
        public int ButtonPreviousState { get; set; }
        [Newtonsoft.Json.JsonProperty]
        public int ButtonState { get; set; }
        /// <summary>
        /// The row location
        /// </summary>
        [Newtonsoft.Json.JsonProperty]
        public int Row { get; set; }

        /// <summary>
        /// The colulmn location
        /// </summary>
        [Newtonsoft.Json.JsonProperty]
        public int Column { get; set; }

        /// <summary>
        /// Whether or not a button has been visited.
        /// </summary>
        [Newtonsoft.Json.JsonProperty]
        public bool Visited { get; set; }

        /// <summary>
        /// Whether or not a button is live.
        /// </summary>
        [Newtonsoft.Json.JsonProperty]
        public bool Live { get; set; }

        /// <summary>
        /// The number live neighbors to the button.
        /// </summary>
        [Newtonsoft.Json.JsonProperty]
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
