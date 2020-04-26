using MicrowaveOvenClasses.Boundary;
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
    [TestFixture]//test
    class IT4_PowerTubeLight
    {
        private IOutput output;
        private IPowerTube uut;


        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();

            uut = new PowerTube(output);
        }
    }
   
}
