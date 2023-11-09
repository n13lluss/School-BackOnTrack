using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Test.Repositories
{
    public class MockStressRepository : IStressRepository
    {
        public bool CreateResult(StressResult result)
        {
            throw new NotImplementedException();
        }

        public bool DeleteResult(StressResult result)
        {
            throw new NotImplementedException();
        }

        public bool EditResult(StressResult result)
        {
            throw new NotImplementedException();
        }

        public List<StressResult> GetAllStresss(string userId)
        {
            throw new NotImplementedException();
        }

        public StressResult GetResultById(int id)
        {
            throw new NotImplementedException();
        }

        public StressResult GetStressByDate(string userId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public StressResult GetStressByDateAndId(DateTime date, string Id)
        {
            throw new NotImplementedException();
        }
    }
}
