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
        List<Sleep> _sleeps = new List<Sleep>();
        public bool CreateResult(SleepResult result)
        {
            throw new NotImplementedException();
        }

        public bool DeleteResult(SleepResult result)
        {
            throw new NotImplementedException();
        }

        public List<SleepResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public SleepResult GetById(int id)
        {
            throw new NotImplementedException();
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
