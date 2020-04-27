using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Test.IntegrationV2
{
    [TestFixture]
    class IT3_CookControllerPowerTube
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

            _timer = new Timer();
            _display = new Display(_output);
            _output = new Output();
            _powerTube = new PowerTube(_output);
            _sut = new CookController(_timer, _display, _powerTube, _userInterface);
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void StartCookingTurnOnWithCorretValues(int power)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                //Arrange
                string expectedOutput = $"PowerTube works with {power}\r\n";

                //Act
                _sut.StartCooking(power, 10);

                //Assert
                Assert.AreEqual(expectedOutput,stringWriter.ToString());
            }
        }

        [TestCase(-5)]
        [TestCase(0)]
        [TestCase(120)]
        public void StartCookingTurnOnWithIncorretValuesThrowExecption(int power)
        {
            
                Assert.Throws<ArgumentOutOfRangeException>(() => _sut.StartCooking(power, 10));
           
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void StartCookingTurnOnAlreadyTurnedOnThrowExecption(int power)
        {
            var standardOut = new StreamWriter(Console.OpenStandardOutput());
            standardOut.AutoFlush = true;
            Console.SetOut(standardOut);

            _sut.StartCooking(power, 10);

                Assert.Throws<ApplicationException>(() => _sut.StartCooking(power, 10));
        }


        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void StopTurnOffCorrect(int power)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                //Arrange
                string expectedOutput = $"PowerTube works with {power}\r\nPowerTube turned off\r\n";

                //Act
                _sut.StartCooking(power, 10);
                _sut.Stop();

                //Assert
                Assert.AreEqual(expectedOutput, stringWriter.ToString());
            }
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void OnTimerExpiredTurnOff(int power)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                //Arrange
                _sut.StartCooking(power, 10);
                string expectedOutput = $"PowerTube works with {power}\r\nPowerTube turned off\r\n";

                //Act
                _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

                //Assert
                Assert.AreEqual(expectedOutput, stringWriter.ToString());
            }
        }
    }
}
