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
        public void StopTimer()
        {
            _sut.StartCooking(70,10000);
            _sut.Stop();
            Thread.Sleep(1500);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(10000));
        }
    }
}
