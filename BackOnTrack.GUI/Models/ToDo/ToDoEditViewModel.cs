using System.ComponentModel.DataAnnotations;

namespace BackOnTrack.GUI.Models.ToDo
{
    public class ToDoEditViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; } = "";
        [Required]
        public DateTime Planned { get; set; }
    }
}
