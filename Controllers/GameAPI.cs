using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Minesweeper.Controllers
{
    [Route("api")]
    [ApiController]
    public class GameAPI : ControllerBase
    {
        /// <summary>
        /// GET: api/showSavedGames - Displays all saved games.
        /// </summary>
        /// <returns></returns>
        [HttpGet("showSavedGames")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// GET api/showSavedGames/5 - Displays the contents of a single game specified by the trailing number.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("showSavedGames/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// DELETE api/deleteOneGame/5 – Deletes one game from the database.         
        /// </summary>
        /// <param name="id"></param>        
        [HttpDelete("deleteOneGame/{id}")]
        public void Delete(int id)
        {
        }
    }
}
