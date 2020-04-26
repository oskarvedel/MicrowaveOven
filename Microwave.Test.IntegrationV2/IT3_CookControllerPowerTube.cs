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
    }
}
