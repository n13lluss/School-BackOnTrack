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
            return _repository.CreateResult(stressResult);
        }

        public bool DeleteStressResult(StressResult stressResult)
        {
            return _repository.DeleteResult(stressResult);
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

        public StressResult GetStressResultByDateAndId(DateTime date, string userId)
        {
            var result = _repository.GetStressByDateAndId(date, userId);
            if(result == null)
            {
                return new StressResult();
            }
            return result;
        }

        public StressResult GetStressResultById(int Id)
        {
            return _repository.GetResultById(Id);
        }

        public bool UpdateStressResult(StressResult stressResult)
        {
            return _repository.EditResult(stressResult);
        }
    }
}
