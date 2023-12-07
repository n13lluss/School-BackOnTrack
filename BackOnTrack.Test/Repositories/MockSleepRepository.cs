using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using BackOnTrack.Test.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Test.Repositories
{
    public class MockSleepRepository : ISleepRepository
    {
        public List<SleepResult> _sleeps = new List<SleepResult>();
        public bool CreateResult(SleepResult result)
        {
            result.Id = _sleeps.Count + 1;
            _sleeps.Add(result);
            return true;
        }

        public bool DeleteResult(SleepResult result)
        {
            foreach(var sleep in _sleeps)
            {
                if (sleep.Id == result.Id)
                {
                    _sleeps.Remove(sleep);
                    return true;
                }
            }
            return false;

        }

        public List<SleepResult> GetAll()
        {
            return _sleeps;
        }

        public SleepResult GetById(int id)
        {
            foreach (SleepResult result in _sleeps)
            {
                if(result.Id == id) return result;
            }
            return new SleepResult();
        }

        public List<SleepResult> GetLastSeven()
        {
            throw new NotImplementedException();
        }

        public SleepResult GetResultByDateAndUserId(DateTime inputDate, string userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateResult(SleepResult result)
        {
            throw new NotImplementedException();
        }
    }
}
