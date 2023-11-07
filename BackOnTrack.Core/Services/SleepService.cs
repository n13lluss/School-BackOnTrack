using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;

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
            try
            {
                if (!IsSleepResultValid(result))
                {
                    return false;
                }

                return _sleepRepository.CreateResult(result);
            }
            catch (Exception ex)
            {
                throw new Exception("There is a problem when adding the result, check the input or the database operation", ex);
            }
        }


        public bool DeleteResult(SleepResult result)
        {
            try
            {
                return _sleepRepository.DeleteResult(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the result, please check the input or the database operation.", ex);
            }
        }

        public bool EditResult(SleepResult result)
        {
            try
            {
                if (!IsSleepResultValid(result))
                {
                    return false;
                }

                return _sleepRepository.UpdateResult(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while editing the result, please check the input or the database operation.", ex);
            }
        }

        public SleepResult GetById(int id)
        {
            try
            {
                return _sleepRepository.GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "An error occurred when trying to get the result");
                return new SleepResult();
            }
        }


        public double GetAverageTimeSleptLastSevenDays(string userId)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Unable to get the result of the last 7 days");
                return 0.0;
            }
        }


        public SleepResult GetResultByDateAndUserId(DateTime inputDate, string userId)
        {
            try
            {
                return _sleepRepository.GetResultByDateAndUserId(inputDate, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Unable to get the result by date and userid");
                return new SleepResult();
            }
        }


        public List<SleepResult> GetResultList()
        {
            try
            {
                return _sleepRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to retrieve the list of sleep results.", ex);
            }
        }

        private static bool IsSleepResultValid(SleepResult result)
        {
            return result.HoursSlept >= 0 && result.HoursSlept <= 24 && result.Date >= new DateTime(2000, 1, 1) && result.Date <= DateTime.Today;
        }
    }
}
