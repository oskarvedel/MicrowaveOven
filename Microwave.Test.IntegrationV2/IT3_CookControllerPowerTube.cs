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

        private IUserInterface userInterface;
        private ITimer timer;
        private IPowerTube powerTube;
        private IDisplay display;
        private IOutput output;
        private ICookController uut;

        [SetUp]
        public void Setup()
        {
            userInterface = Substitute.For<IUserInterface>();
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();

            output = new Output();
            powerTube = new PowerTube(output);
            uut = new CookController(timer, display, powerTube, userInterface);
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
                uut.StartCooking(power, 10);

                //Assert
                Assert.AreEqual(expectedOutput,stringWriter.ToString());
            }
        }

        [TestCase(-5)]
        [TestCase(0)]
        [TestCase(120)]
        public void StartCookingTurnOnWithIncorretValuesThrowExecption(int power)
        {
            uut.StartCooking(power, 10);

            Assert.That(() => uut.StartCooking(power, 10), Throws.Exception);
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void StartCookingTurnOnAlreadyTurnedOnThrowExecption(int power)
        {
            uut.StartCooking(power, 10);

            Assert.That(() => uut.StartCooking(power, 10), Throws.Exception);
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
                uut.StartCooking(power, 10);
                uut.Stop();

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
                uut.StartCooking(power, 10);
                string expectedOutput = $"PowerTube works with {power}\r\nPowerTube turned off\r\n";

                //Act
                timer.Expired += Raise.EventWith(this, EventArgs.Empty);

                //Assert
                Assert.AreEqual(expectedOutput, stringWriter.ToString());
            }
        }
    }
}
