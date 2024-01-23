using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Minesweeper.Logger;
using Minesweeper.Models;
using Newtonsoft.Json;

namespace Minesweeper.Filters
{
    /// <summary>
    /// Action filter to process logging for login actions.
    /// </summary>
    public class ProcessLoginLogActionFilter : IActionFilter
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessLoginLogActionFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger used for logging information.</param>
        public ProcessLoginLogActionFilter(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called after the action method executes to retrieve and log the user data.
        /// </summary>
        /// <param name="context">The <see cref="ActionExecutedContext"/> context for the action.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Retrieve and log user data.
            string userData = ((Controller)context.Controller).HttpContext.Session.GetString("user");

            UserModel user = JsonConvert.DeserializeObject<UserModel>(userData);
            _logger.Info("Authenticated user: " + user.ToString());

            _logger.Info("Leaving the login process.");
        }

        /// <summary>
        /// Called before the action method executes.
        /// </summary>
        /// <param name="context">The <see cref="ActionExecutingContext"/> context for the action.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.Info("Entering the login process.");
        }
    }
}
