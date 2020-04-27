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
	public class IT6_UserInterfaceLight
	{
		private IUserInterface uut;
		private ILight light;
		private IDoor door;
		private ICookController cookController;
		private IDisplay display;
		private IButton powerButton;
		private IButton timeButton;
		private IButton startCancelButton;
		private IOutput output;

		[SetUp]
		public void Setup()
		{
			powerButton = Substitute.For<IButton>();
			timeButton = Substitute.For<IButton>();
			startCancelButton = Substitute.For<IButton>();
			door = Substitute.For<IDoor>();
			cookController = Substitute.For<ICookController>();
			display = Substitute.For<IDisplay>();
			output = Substitute.For<IOutput>();
			light = new Light(output);
			uut = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cookController);
		}

		[Test]
		public void Test1()
		{
			//arrange

			//act

			_timer.TimerTick += Raise.EventWith(EventArgs.Empty);

			//assert
			Assert.That(uut.OnStartCancelPressed());
		}
    
    }
}