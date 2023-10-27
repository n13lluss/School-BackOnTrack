using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Core.Services
{
    public class SleepService : ISleepService
    {   
        private readonly ISleepRepository _sleepRepository;

        public SleepService(ISleepRepository sleepRepository)
        {
            _sleepRepository = sleepRepository;
        }

        public SleepResult CreateResult(SleepResult result)
        {
            if(result.HoursSlept > 24)
            {
                return null;
            }
            bool IsCreated = _sleepRepository.CreateResult(result);

            if(IsCreated)
            {
                return result;
            }

            return null;
        }

        public bool DeleteResult(SleepResult result)
        {
            bool isDeleted = _sleepRepository.DeleteResult(result);
            return isDeleted;
        }

        public SleepResult EditResult(SleepResult result)
        {
            if (result.HoursSlept > 24)
            {
                return null;
            }
            bool updated = _sleepRepository.UpdateResult(result);
            if(updated)
            {
                return result;
            }
            return null;
        }

        public SleepResult GetById(int id)
        {
            SleepResult result = _sleepRepository.GetById(id);
            return result;
        }

        public List<SleepResult> GetLastSevenDays(string userId)
        {
            throw new NotImplementedException();
        }

        public List<SleepResult> GetResultList()
        {
            List<SleepResult> results = _sleepRepository.GetAll();
            return results;
        }
    }
}
