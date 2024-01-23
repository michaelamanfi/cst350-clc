namespace Minesweeper.Logger
{
    /// <summary>
    /// Represents a logging service that provides mechanisms to log messages at different levels.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a debug message. This is typically used for internal system-level debugging.
        /// </summary>
        /// <param name="message">The debug message to log.</param>
        void Debug(string message);

        /// <summary>
        /// Logs an informational message. This is typically used for general operational information.
        /// </summary>
        /// <param name="message">The information message to log.</param>
        void Info(string message);

        /// <summary>
        /// Logs a warning message. This is typically used for non-critical issues that might require attention.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        void Warning(string message);

        /// <summary>
        /// Logs an error message. This is typically used for logging errors and exceptions.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        void Error(string message);
    }

}
