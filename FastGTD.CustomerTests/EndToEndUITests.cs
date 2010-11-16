using NUnit.Framework;
using White.Core;
using White.Core.UIItems.WindowItems;

namespace FastGTD.CustomerTests
{
    [TestFixture]
    public class EndToEndUITests
    {
        [Test]
        public void TryWhite()
        {
            var app = Application.Launch("FastGTD.exe");
            var inbox_window = app.GetWindow("InBox");
            inbox_window.Focus(DisplayState.Maximized);
            
            app.Kill();
        }
    }
}
