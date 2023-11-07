using BackOnTrack.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BackOnTrack.Core.Interfaces
{
    public interface IStressRepository
    {
        bool CreateResult(StressResult result);
        bool EditResult(StressResult result);
        bool DeleteResult(StressResult result);
        StressResult GetResultById(int id);
        List<StressResult> GetAllStresss(string userId);
        StressResult GetStressByDate(string userId, DateTime date);
        StressResult GetStressByDateAndId(DateTime date, string Id);
    }
}
