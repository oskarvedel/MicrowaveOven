using System.Threading;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT5_CookControllerTimer
    {
        private IUserInterface _userInterface;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private IOutput _output;
        private ICookController _sut;

        [SetUp]
        public void Setup()
        {
            _userInterface = Substitute.For<IUserInterface>();
            _output = Substitute.For<IOutput>();
            _powerTube = Substitute.For<IPowerTube>();
  

            _display = new Display(_output);
            _timer = new Timer();
            _sut = new CookController(_timer,_display,_powerTube,_userInterface);
        }

        [Test]
        public void StartCookingStartTimer()
        {
            _sut.StartCooking(70, 40);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(40));
        }

        [Test]
        public void StartCookingStartTimer_TimeRemaining_Decrements()
        {
            _sut.StartCooking(70, 20);
            Thread.Sleep(1500);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(19));
            Thread.Sleep(1000);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(18));
            Thread.Sleep(1000);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(17));
        }

        [TestCase(20)]
        [TestCase(19)]
        [TestCase(18)]
        public void StartCookingStartTimer_Display_Decrements_LessThanOneMinute(int timeRemaining)
        {
            _sut.StartCooking(70, timeRemaining+1);
            Thread.Sleep(1500);
            _output.Received(1).OutputLine($"Display shows: 00:{timeRemaining}");
        }

        [Test]
        public void StartCookingStartTimer_Display_Decrements_OneMinuteMark()
        {
            _sut.StartCooking(70,  62);
            Thread.Sleep(1500);
            _output.Received(1).OutputLine($"Display shows: 01:01");
            Thread.Sleep(1000);
            _output.Received(1).OutputLine($"Display shows: 01:00");
            Thread.Sleep(1000);
            _output.Received(1).OutputLine($"Display shows: 00:59");
        }

        [Test]
        public void PowerTubeReceives_TurnOff_AfterTimerExpires()
        {
            _sut.StartCooking(70, 2);
            Thread.Sleep(2500);
            _powerTube.Received(1).TurnOff();

        }
        
        [Test]
        public void StopTimer()
        {
            _sut.StartCooking(70, 50);
            _sut.Stop();
            Thread.Sleep(1500);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(50));
        }



    }
}
