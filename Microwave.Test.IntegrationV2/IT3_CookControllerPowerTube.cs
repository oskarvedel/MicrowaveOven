using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            output = Substitute.For<IOutput>();
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();

            powerTube = new PowerTube(output);
            uut = new CookController(timer, display, powerTube, userInterface);
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void StartCookingTurnOnWithCorretValues(int power)
        {
            uut.StartCooking(power, 10);

            output.Received(1).OutputLine($"PowerTube works with {power}");
            Assert.That(() => uut.StartCooking(power, 10), Throws.Nothing);
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
            uut.StartCooking(power, 10);
            uut.Stop();

            output.Received(1).OutputLine($"PowerTube turned off");
        }

        [Test]
        public void OnTimerExpiredTurnOff(int power)
        {
    
        }



    }
}
