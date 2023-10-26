using BackOnTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOnTrack.Core.Interfaces
{
    public interface ISleepService
    {
        public SleepResult CreateResult(SleepResult result);
        public SleepResult EditResult(SleepResult result);
        public bool DeleteResult(SleepResult result);
        public List<SleepResult> GetResultList();
        public List<SleepResult> GetLastSevenDays(string userId);
        public SleepResult GetById(int id);
    }
}
