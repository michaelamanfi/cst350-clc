using System;

namespace Minesweeper.Models
{
    public class GameModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserFullName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Json { get; set; }
    }
}
