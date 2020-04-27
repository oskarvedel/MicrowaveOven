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
	class IT2_PowerTubeOutput
	{
		private IOutput _output;
		private IPowerTube _sut;

		[SetUp]
		public void Setup()
		{
			_output = new Output();
			_sut = new PowerTube(_output);
		}

		[Test]
		[TestCase(1)]
		[TestCase(50)]
		[TestCase(100)]
		public void TurnOnOutputIsCorrect(int power)
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				Console.SetOut(stringWriter);

				//Arrange
				string expectedOutput = $"PowerTube works with {power}\r\n";

				//Act
				_sut.TurnOn(power);

				//Assert
				Assert.AreEqual(expectedOutput, stringWriter.ToString());
			}
		}


		[Test]
		public void TurnOffOutputIsCorrect()
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				//Arrange
				string expectedOutput = $"PowerTube turned off\r\n";
				_sut.TurnOn(50);
				Console.SetOut(stringWriter);

				//Act
				_sut.TurnOff();

				//Assert
				Assert.AreEqual(expectedOutput, stringWriter.ToString());
			}
		}
	}
}