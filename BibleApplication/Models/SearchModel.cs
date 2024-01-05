using System.ComponentModel.DataAnnotations;

namespace BibleApplication.Models
{
    public class SearchModel
    {
        [Required(ErrorMessage = "Search term is required")]
        public string Text { get; set; }

        // Assuming you want to validate that a testament type is selected
        [Required(ErrorMessage = "Please select a testament type")]
        public TestermentType Testament { get; set; }
    }
}
