﻿using MicrowaveOvenClasses.Boundary;
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
		private IDisplay _sut;

		[SetUp]
		public void Setup()
		{
			_output = new Output();
			_sut = new Display(_output);
		}

		[Test]
		[TestCase(0, 0)]
		[TestCase(1, 1)]
		[TestCase(60, 60)]
		public void ShowTimeOutputIsCorrectTime(int min, int sec)
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				Console.SetOut(stringWriter);

				//Arrange
				string expectedOutput = $"Display shows: {min:D2}:{sec:D2}\r\n";

				//Act
				_sut.ShowTime(min, sec);

				//Assert
				Assert.AreEqual(expectedOutput, stringWriter.ToString());
			}
		}

		[Test]
		[TestCase(0)]
		[TestCase(50)]
		[TestCase(100)]
		public void ShowPowerOutputIsCorrect(int power)
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				Console.SetOut(stringWriter);

				//Arrange
				string expectedOutput = $"Display shows: {power} W\r\n";

				//Act
				_sut.ShowPower(power);

				//Assert
				Assert.AreEqual(expectedOutput, stringWriter.ToString());
			}
		}

		[Test]
		public void ClearOutputIsCorrect()
		{
			using (StringWriter stringWriter = new StringWriter())
			{
				Console.SetOut(stringWriter);

				//Arrange
				string expectedOutput = $"Display cleared\r\n";

				//Act
				_sut.Clear();

				//Assert
				Assert.AreEqual(expectedOutput, stringWriter.ToString());
			}
		}

	}
}
