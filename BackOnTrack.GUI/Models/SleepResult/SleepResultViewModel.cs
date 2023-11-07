using System.ComponentModel.DataAnnotations;

namespace BackOnTrack.GUI.Models.SleepResult
{
    public class SleepResultViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Hours slept are needed to save a result")]
        [Range(0, 24, ErrorMessage = "The number of hours slept needs to be between 0 and 24")]
        public int TimeSlept { get; set; }
        [Required(ErrorMessage = "Date is required for creating a Result")]
        public DateTime Date { get; set; }
    }
}
