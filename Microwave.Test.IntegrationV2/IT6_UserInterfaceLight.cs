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
<<<<<<< HEAD
    /*
    [TestFixture]
    public class IT6_UserInterfaceLight
    {
        private IUserInterface uut;
        private ILight light;

        [SetUp]
        public void Setup()
        {
            light = Substitute.For<ILight>();
            uut = new UserInterface(light);
        }
    }
    */
=======
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

		[SetUp]
		public void Setup()
		{
			powerButton = Substitute.For<IButton>();
			timeButton = Substitute.For<IButton>();
			startCancelButton = Substitute.For<IButton>();
			door = Substitute.For<IDoor>();
			cookController = Substitute.For<ICookController>();
			light = Substitute.For<ILight>();
			display = Substitute.For<IDisplay>();
			uut = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cookController);
		}

		[Test]
		public void Test1 ()
		{}

}
>>>>>>> 39549180525a5f493b7f6a25f2be063c656630ac
}