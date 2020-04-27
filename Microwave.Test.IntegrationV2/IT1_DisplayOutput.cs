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
    class IT1_DisplayOutput
    {
	    private IOutput _output;
        private IDisplay _uut;

        [SetUp]
        public void Setup()
        {
	        _output = new Output();
	        _uut = new Display(_output);
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void test1()
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                //Console.SetOut(stringWriter);
	            
                //Arrange
                //string expectedOutput = $"PowerTube works with {power}\r\n";

                //Act
                //uut.StartCooking(power, 10);

                //Assert
                //Assert.AreEqual(expectedOutput, stringWriter.ToString());
            }
        }
    }
}
