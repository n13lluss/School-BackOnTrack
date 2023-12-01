using BackOnTrack.Core.Models;

namespace BackOnTrack.Core.Interfaces
{
    public interface ISleepRepository
    {
        public bool CreateResult(SleepResult result);
        public bool DeleteResult(SleepResult result);
        public bool UpdateResult(SleepResult result);
        public List<SleepResult> GetAll();
        public List<SleepResult> GetLastSeven();
        public SleepResult GetById(int id);
        public SleepResult GetResultByDateAndUserId(DateTime inputDate, string userId);
    }
}