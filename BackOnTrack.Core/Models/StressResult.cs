namespace BackOnTrack.Core.Models
{
    public class StressResult
    { 
        public int Id { get; set; }
        public int StressLevel { get; set; }
        public int HoursSlept { get; set; }
        public DateTime date { get; set; }
        public string UserId { get; set; }

    }
}
