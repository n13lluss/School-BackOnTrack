using BackOnTrack.Core.Interfaces;
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
        public void CreateSleep_MissingName_ShouldThrowException()
        {
            // Arrange



            // Act

            // Assert

        }
    }
}