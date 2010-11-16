using NUnit.Framework;
using White.Core;
using White.Core.Factory;
using White.Core.UIItems;
using White.Core.UIItems.Finders;
using White.Core.UIItems.WindowItems;
using White.UnitTests.Core.Testing;

namespace White.UnitTests.Core.UIItems
{
    [TestFixture, WinFormCategory, WPFCategory]
    public class SplashScreenTest : CoreTestTemplate
    {
        protected override string CommandLineArguments
        {
            get { return "splash"; }
        }

        [Test]
        public void FindWindowOnSplashScreen()
        {
            Window splash = application.FindSplash();
            Assert.IsNotNull(splash);
            Window modalWindow = splash.ModalWindow("Foo", InitializeOption.NoCache);
            Assert.AreNotEqual(null, modalWindow);
            var okButton = modalWindow.Get<Button>(SearchCriteria.ByText("OK"));
            okButton.Click();
        }
    }
}