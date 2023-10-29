using System.ComponentModel.DataAnnotations;

namespace BackOnTrackGUI.Models.ToDo
{
    public class ToDoCreationViewModel
    {
        [Required, MinLength(1), MaxLength(200)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime Planned { get; set; }
        [Required]
        public int Status { get; set; } = 0;
    }
}
