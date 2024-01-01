using Minesweeper.Models;
using System.Collections.Generic;

namespace Minesweeper
{
    public interface IUserGameDAL
    {
        List<UserGameModel> GetAllGames();
        UserGameModel GetGameById(int gameId);
    }
}