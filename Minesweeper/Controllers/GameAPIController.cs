using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.Models;
using System.Collections.Generic;

namespace Minesweeper.Controllers
{
    [Route("api")]
    [ApiController]
    [AllowAnonymous]
    public class GameAPIController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameAPIController(IGameService gameService)
        {
            this._gameService = gameService;
        }
        /// <summary>
        /// GET: api/showSavedGames - Displays all saved games.
        /// </summary>
        /// <returns></returns>
        [HttpGet("showSavedGames")]
        public IActionResult Get()
        {
            return new JsonResult(this._gameService.GetAllGames());
        }

        /// <summary>
        /// GET api/showSavedGames/5 - Displays the contents of a single game specified by the trailing number.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("showSavedGames/{id}")]
        public IActionResult Get(int id)
        {
            return new JsonResult(this._gameService.GetGameById(id));
        }

        /// <summary>
        /// DELETE api/deleteOneGame/5 – Deletes one game from the database.         
        /// </summary>
        /// <param name="id"></param>        
        [HttpDelete("deleteOneGame/{id}")]
        public IActionResult Delete(int id)
        {
            this._gameService.DeleteGame(id);
            return Ok();
        }
    }
}
