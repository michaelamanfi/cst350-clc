namespace Minesweeper.Logger
{
    using NLog;

    /// <summary>
    /// Provides a singleton logger instance that wraps around NLog functionalities.
    /// </summary>
    public class NLogger : ILogger
    {
        private static NLogger instance;
        private static Logger logger;

        /// <summary>
        /// Retrieves the singleton instance of NLogger.
        /// </summary>
        /// <returns>The singleton instance of NLogger.</returns>
        public static NLogger GetInstance()
        {
            if (instance == null)
                instance = new NLogger();

            return instance;
        }

        /// <summary>
        /// Retrieves the NLog Logger instance.
        /// </summary>
        /// <returns>The NLog Logger instance.</returns>
        private Logger GetLogger()
        {
            if (NLogger.logger == null)
                NLogger.logger = LogManager.GetLogger("LoginLog");

            return NLogger.logger;
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The debug message to log.</param>
        public void Debug(string message)
        {
            GetLogger().Debug(message);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public void Error(string message)
        {
            GetLogger().Error(message);
        }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The informational message to log.</param>
        public void Info(string message)
        {
            GetLogger().Info(message);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public void Warning(string message)
        {
            GetLogger().Warn(message);
        }
    }
}
