using Minesweeper.Common;

namespace Minesweeper.Models
{
    public class ButtonModel : Common.ButtonModel
    {
        // Properties
        public string Id { get { return this.ToString(); } }

        // Default constructor
        public ButtonModel()
        {
        }

        // Constructor with parameters
        public ButtonModel(int row, int column, int buttonState)
        {
            Row = row;
            Column = column;
            ButtonState = buttonState;
        }
    }
}
