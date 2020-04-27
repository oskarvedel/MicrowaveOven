using System;
using System.IO;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
	[TestFixture]
	class IT3_LightOutput
	{
		private IOutput _output;
		private ILight _sut;

		[SetUp]
		public void Setup()
		{
			_output = new Output();
			_sut = new Light(_output);
		}

		[Test]
		public void TurnOnOutputIsCorrect()
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				Console.SetOut(stringWriter);

				//Arrange
				string expectedOutput = $"Light is turned on\r\n";

				//Act
				_sut.TurnOn();

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
				string expectedOutput = $"Light is turned off\r\n";
				_sut.TurnOn();
				Console.SetOut(stringWriter);

				//Act
				_sut.TurnOff();

				//Assert
				Assert.AreEqual(expectedOutput, stringWriter.ToString());
			}
		}
	}
}