using System.ComponentModel.DataAnnotations;

namespace BackOnTrack.GUI.Models.ToDo
{
    public class ToDoIndexViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(1), MaxLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Planned { get; set; }
        [Required]
        public string Status { get; set; } = "Not yet started";
    }
}
