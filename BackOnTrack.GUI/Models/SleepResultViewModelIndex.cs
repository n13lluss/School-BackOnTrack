using System.ComponentModel.DataAnnotations;

namespace BackOnTrack.GUI.Models
{
    public class SleepResultViewModelIndex
    {
        public double AverageTimeSlept { get; set; }
        public List<SleepResultViewModel> AllResults { get; set; }
    }
}
