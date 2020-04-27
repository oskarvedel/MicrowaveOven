using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.IntegrationV2
{
    [TestFixture]
    class IT1_CookControllerDisplay
    {
        private IOutput _output;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IUserInterface _userInterface;
        private ICookController _sut;

        [SetUp]
        public void SetUp()
        {
            _powerTube = Substitute.For<IPowerTube>();
            _timer = Substitute.For<ITimer>();
            _userInterface = Substitute.For<IUserInterface>();

            _output = new Output();
            _display = new Display(_output);

            _sut = new CookController(_timer,_display,_powerTube,_userInterface);
        }

        [TestCase(1)]
        [TestCase(59)]
        [TestCase(60)]
        [TestCase(61)]
        [TestCase(119)]
        [TestCase(120)]
        [TestCase(121)]
        public void RemainingTimeIsDisplayedCorrectly(int secondsRemaining)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                //Arrange
                _timer.TimeRemaining.Returns(secondsRemaining);
                string expectedOutput = ($"Display shows: {(secondsRemaining/60):D2}:{(secondsRemaining % 60):D2}\r\n");

                
                //Act
                _timer.TimerTick += Raise.EventWith(EventArgs.Empty);

                //Assert
                Assert.AreEqual(expectedOutput,stringWriter.ToString());
            }
        }
    }
}
