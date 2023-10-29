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
        public bool CreateResult(SleepResult result);
        public bool EditResult(SleepResult result);
        public bool DeleteResult(SleepResult result);
        public List<SleepResult> GetResultList();
        public double GetAverageTimeSleptLastSevenDays(string userId);
        public SleepResult GetById(int id);
        public SleepResult GetResultByDateAndUserId(DateTime inputDate, string userId);
    }
}
