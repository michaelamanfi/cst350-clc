using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Common
{
    /// <summary>
    /// This class represents the results of a players game.
    /// </summary>
    public class PlayerStats : IComparable
    {
        public PlayerStats()
        {
        }
        /// <summary>
        /// The name of the player
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Play time
        /// </summary>
        public TimeSpan TimeElapsed { get; set; }
        /// <summary>
        /// The game's difficulty level.
        /// </summary>
        public DifficultyLevel Difficulty { get; set; }
        /// <summary>
        /// Player's score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Calculates the game score. Points are awarded based on the difficulty level 
        /// and the time taken to complete the game successfully.
        /// </summary>
        /// <returns></returns>
        public int CalculateGameStore()
        {
            int score;
            int scoreMultiplier = 5;
            if (Difficulty == DifficultyLevel.Difficult)
            {
                score = 50;
                if (this.TimeElapsed < TimeSpan.FromSeconds(15))
                    score += scoreMultiplier * 60;
                else if (this.TimeElapsed < TimeSpan.FromSeconds(30))
                    score += scoreMultiplier * 30;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(1))
                    score += scoreMultiplier * 14;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(2))
                    score += scoreMultiplier * 13;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(3))
                    score += scoreMultiplier * 12;
                if (this.TimeElapsed < TimeSpan.FromMinutes(5))
                    score += scoreMultiplier * 11;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(15))
                    score += scoreMultiplier * 10;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(30))
                    score += scoreMultiplier * 9;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(40))
                    score += scoreMultiplier * 8;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(60))
                    score += scoreMultiplier * 7;
            }
            else if (Difficulty == DifficultyLevel.Moderate)
            {
                score = 25;
                if (this.TimeElapsed < TimeSpan.FromSeconds(15))
                    score += scoreMultiplier * 30;
                else if (this.TimeElapsed < TimeSpan.FromSeconds(30))
                    score += scoreMultiplier * 14;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(1))
                    score += scoreMultiplier * 11;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(2))
                    score += scoreMultiplier * 10;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(3))
                    score += scoreMultiplier * 9;
                if (this.TimeElapsed < TimeSpan.FromMinutes(5))
                    score += scoreMultiplier * 8;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(15))
                    score += scoreMultiplier * 7;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(30))
                    score += scoreMultiplier * 6;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(40))
                    score += scoreMultiplier * 5;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(60))
                    score += scoreMultiplier * 4;
            }
            else
            {
                score = 10;
                if (this.TimeElapsed < TimeSpan.FromSeconds(5))
                    score += scoreMultiplier * 8;
                else if (this.TimeElapsed < TimeSpan.FromSeconds(15))
                    score += scoreMultiplier * 7;
                else if (this.TimeElapsed < TimeSpan.FromSeconds(30))
                    score += scoreMultiplier * 6;
                else if (this.TimeElapsed < TimeSpan.FromSeconds(50))
                    score += scoreMultiplier * 5;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(1))
                    score += scoreMultiplier * 4;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(2))
                    score += scoreMultiplier * 3;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(3))
                    score += scoreMultiplier * 2;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(4))
                    score += scoreMultiplier * 1;
                else if (this.TimeElapsed < TimeSpan.FromMinutes(5))
                    score += scoreMultiplier;
            }

            return score;
        }
        /// <summary>
        /// Compares two PlayerStats.
        /// </summary>
        /// <param name="obj">The PlayerStats to compare to</param>
        /// <returns>The comparision value</returns>
        public int CompareTo(object obj)
        {
            TimeSpan score1 = this.TimeElapsed;
            TimeSpan score2 = ((PlayerStats)obj).TimeElapsed;            
            if (score1 > score2)
                return 1;
            else if (score1 < score2)
                return -1;
            else
                return 0;
        }
    }
}
