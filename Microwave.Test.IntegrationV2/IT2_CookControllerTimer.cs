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
            powerTube = Substitute.For<IPowerTube>();
            display = Substitute.For<IDisplay>();


            timer = new Timer();
            uut = new CookController(timer,display,powerTube,userInterface);
        }

        [TestCase(1000)]
        [TestCase(2100)]
        [TestCase(3250)]
        [TestCase(4001)]
        [TestCase(11243)]
        public void StartCookingStartTimer(int time)
        {
            uut.StartCooking(70, time);
        }

        public void CookControllerCheckTimer()
        {
            uut.StartCooking(80,20);
            uut.Stop();
            Assert.That(timer.TimeRemaining,Is.EqualTo(0));
        }


    }
}
