using BackOnTrack.Core.Models;
using System;
using System.Collections.Generic;

namespace BackOnTrack.Core.Interfaces
{
    public interface IStressService
    {
        bool CreateStressResult(StressResult stressResult);
        bool UpdateStressResult(StressResult stressResult);
        bool DeleteStressResult(DateTime date);
        List<StressResult> GetAllStressResults(string userId);
        StressResult GetStressResultById(int Id, string UserId);
    }
}
