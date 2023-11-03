using System.ComponentModel.DataAnnotations;

namespace BackOnTrackGUI.Models.ToDo
{
    public class ToDoDeletionViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime Planned { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
