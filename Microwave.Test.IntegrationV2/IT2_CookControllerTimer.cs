using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.IntegrationV2
{
    [TestFixture]
    public class IT2_CookControllerTimer
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

        [TestCase(1000)]
        [TestCase(2100)]
        [TestCase(3250)]
        [TestCase(4001)]
        [TestCase(11243)]
        public void StartCookingStartTimer(int time)
        {
            _sut.StartCooking(70, time);
        }

        [Test]
        public void CookControllerCheckTimer()
        {
            _sut.StartCooking(80,20);
            _sut.Stop();
            Assert.That(_timer.TimeRemaining,Is.EqualTo(0));
        }


    }
}
