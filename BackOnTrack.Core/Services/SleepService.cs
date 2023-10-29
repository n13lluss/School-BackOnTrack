using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackOnTrack.Core.Services
{
    public class SleepService : ISleepService
    {
        private readonly ISleepRepository _sleepRepository;

        public SleepService(ISleepRepository sleepRepository)
        {
            _sleepRepository = sleepRepository;
        }

        public bool CreateResult(SleepResult result)
        {
            if (!IsSleepResultValid(result))
            {
                return false;
            }

            return _sleepRepository.CreateResult(result);
        }

        public bool DeleteResult(SleepResult result)
        {
            return _sleepRepository.DeleteResult(result);
        }

        public bool EditResult(SleepResult result)
        {
            if (!IsSleepResultValid(result))
            {
                return false;
            }

            return _sleepRepository.UpdateResult(result);
        }

        public SleepResult GetById(int id)
        {
            return _sleepRepository.GetById(id);
        }

        public double GetAverageTimeSleptLastSevenDays(string userId)
        {
            List<SleepResult> results = _sleepRepository.GetLastSeven();

            List<SleepResult> validResults = results
                .Where(result => result.UserID == userId && result.Date >= DateTime.Today.AddDays(-7))
                .ToList();

            if (validResults.Count == 0)
            {
                return 0.0;
            }

            double totalHoursSlept = validResults.Sum(result => result.HoursSlept);
            double averageTimeSlept = totalHoursSlept / validResults.Count;

            return Math.Round(averageTimeSlept, 2);
        }

        public SleepResult GetResultByDateAndUserId(DateTime inputDate, string userId)
        {
            return _sleepRepository.GetResultByDateAndUserId(inputDate, userId);
        }

        public List<SleepResult> GetResultList()
        {
            return _sleepRepository.GetAll();
        }

        private bool IsSleepResultValid(SleepResult result)
        {
            return result.HoursSlept >= 0 && result.HoursSlept <= 24 && result.Date <= DateTime.Today;
        }
    }
}
