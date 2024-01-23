using Minesweeper.Logger;

namespace Minesweeper.Service
{
    public class LogService : ILogger
    {
        public LogService()
        {

        }
        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The debug message to log.</param>
        public void Debug(string message)
        {
            NLogger.GetInstance().Debug(message);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public void Error(string message)
        {
            NLogger.GetInstance().Error(message);
        }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The informational message to log.</param>
        public void Info(string message)
        {
            NLogger.GetInstance().Info(message);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public void Warning(string message)
        {
            NLogger.GetInstance().Warning(message);
        }
    }
}
