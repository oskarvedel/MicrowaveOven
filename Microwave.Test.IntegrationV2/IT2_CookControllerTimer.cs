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

        [Test]
        public void StartCookingStartTimer()
        {
            _sut.StartCooking(70, 40);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(40));
        }
    }
}
