namespace BackOnTrack.Core.Models
{
    public class StressResult
    { 
        public int Id { get; set; }
        public int StressLevel { get; set; }
        public int HoursSlept { get; set; }
        public DateTime date { get; set; }
        public string UserId { get; set; }

        public string GetStressAsString()
        {
            return StressLevel switch
            {
                0 => "None",
                1 => "Very Low",
                2 => "Low",
                3 => "Medium",
                4 => "High",
                5 => "Very High",
                6 => "Extremly High",
                _ => "Unknown",
            };
        }

    }
}
