using System.ComponentModel.DataAnnotations;

namespace BackOnTrack.GUI.Models.Stress
{
    public class CreateStressViewModel
    {
        [Required]
        public int Result { get; set; }
        [Required (ErrorMessage = "A stress result needs a date")]

        public DateTime date { get; set; } = DateTime.Now;
    }
}
