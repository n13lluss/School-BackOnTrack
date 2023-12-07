using BackOnTrack.Core.Interfaces;
using BackOnTrack.Core.Models;
using BackOnTrack.Core.Services;
using BackOnTrack.Test.Repositories;

namespace BackOnTrack.Test.Tests
{
    public class Sleep
    {
        private readonly ISleepService _sleepService;
        private readonly MockSleepRepository _sleepRepository; 
        

        public Sleep()
        {
            _sleepRepository = new MockSleepRepository();
            _sleepService = new SleepService(_sleepRepository);
        }

        [Fact]
        public void CreateSleep_MissingDate_ShouldThrowException()
        {
            SleepResult result = new()
            {
                HoursSlept = 6,
            };

            _sleepService.CreateResult(result);
            var list = _sleepRepository._sleeps;

            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void CreateSleep_ResultOutRange_ShouldThrowException()
        {
            SleepResult result = new()
            {
                HoursSlept = -3,
            };

            _sleepService.CreateResult(result);
            var list = _sleepRepository._sleeps;

            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void CreateSleep_0Input_ShouldCreate()
        {
            SleepResult result = new() { Date = DateTime.Today, HoursSlept = 6, };

            _sleepService.CreateResult(result);
            var list = _sleepRepository._sleeps;

            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void CreateSleep_OldDate_ShouldThrowException()
        {
            SleepResult result = new() { Date = new DateTime(), HoursSlept = 6, };

            _sleepService.CreateResult(result);
            var list = _sleepRepository._sleeps;

            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void DeleteSleep_WithCorrectInput_ShouldDelete()
        {
            List<SleepResult> _result = new List<SleepResult>();
            SleepResult result1 = new SleepResult()
            {
                Id = 1,
                HoursSlept = 6,
                Date = DateTime.Today,
                UserID = "4002",
            };
            _sleepRepository._sleeps.Add(result1);

            _sleepService.DeleteResult(result1);

            Assert.Equal(0, _sleepRepository._sleeps.Count);
        }

        [Fact]
        public void GetAll_CorrectUSerID_ShouldReturnList()
        {

        }
    }
}