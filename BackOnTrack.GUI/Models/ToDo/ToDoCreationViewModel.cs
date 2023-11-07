using System.ComponentModel.DataAnnotations;

namespace BackOnTrack.GUI.Models.ToDo
{
    public class ToDoCreationViewModel
    {
        [Required (ErrorMessage = "A name is required to make a To Do item"), MinLength(1), MaxLength(200, ErrorMessage = "The maximum length is 200 characters")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "A To Do item needs a date to be planned on")]
        public DateTime Planned { get; set; }
        public int Status { get; set; } = 0;
    }
}
