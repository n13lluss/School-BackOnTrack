using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;

namespace BackOnTrack.Core.Services
{
    public class StressService : IStressService
    {
        private readonly IStressRepository _repository;

        public StressService(IStressRepository stress)
        {
            _repository = stress;
        }

        public bool CreateStressResult(StressResult stressResult)
        {
            throw new NotImplementedException();
        }

        public bool DeleteStressResult(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<StressResult> GetAllStressResults(string userId)
        {
            List<StressResult> results = _repository.GetAllStresss(userId);
            if(results == null)
            {
                return new List<StressResult>();
            }
            return results;
        }

        public StressResult GetStressResultById(int Id, string UserId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStressResult(StressResult stressResult)
        {
            throw new NotImplementedException();
        }
    }
}
