﻿using System;
using System.IO;
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
    class IT6_CookControllerPowerTube
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
            _output = new Output();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _sut = new CookController(_timer, _display, _powerTube, _userInterface);
        }

        [TearDown]
        public void TearDown()
        {
            var standardOut = new StreamWriter(Console.OpenStandardOutput());
            standardOut.AutoFlush = true;
            Console.SetOut(standardOut);
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
                Assert.That(_sut.isCooking, Is.EqualTo(false));
            }
        }

        [Test]
        public void OnTimerExpiredTurnOff()
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                //Arrange
                _sut.StartCooking(10, 2000);
                string expectedOutput = $"PowerTube works with {10}\r\nPowerTube turned off\r\n";

                //Act
                Thread.Sleep(2500);
                //_timer.Expired += Raise.EventWith(EventArgs.Empty);

                //Assert
                Assert.That(_sut.isCooking,Is.EqualTo(false));
                Assert.That(_timer.TimeRemaining <= 0);
            }
        }
    }
}