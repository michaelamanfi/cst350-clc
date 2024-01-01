using Minesweeper.Models;
using System.Collections.Generic;

namespace Minesweeper
{
    public interface IGameDAL
    {
        bool GameExists(int gameId);
        void CreateGame(GameDbModel game);
        void DeleteGame(int gameId);
        List<GameDbModel> GetAllGames();
        GameDbModel GetGameById(int gameId);
        void UpdateGame(GameDbModel game);
    }
}