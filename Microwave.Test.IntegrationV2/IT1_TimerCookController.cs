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
using MicrowaveOvenClasses.Boundary;

namespace Microwave.Test.IntegrationV2
{
    [TestFixture]
    public class IT1_TimerCookController
    {
        private Timer timer;
        private IOutput output;
        private IDisplay display;
        private PowerTube powerTube;
        private CookController cookController;
        private IUserInterface userInterface;

        [SetUp]
        public void SetUp()
        {

        }
    }
}
