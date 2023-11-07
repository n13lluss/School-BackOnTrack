using BackOnTrack.Core.Models;

namespace BackOnTrack.Core.Interfaces
{
    public interface IStressRepository
    {
        bool CreateResult(StressResult result);
        bool EditResult(StressResult result);
        bool DeleteResult(StressResult result);
        List<StressResult> GetAllStresss(string userId);
        StressResult GetStressByDate(string userId, DateTime date);
    }
}
