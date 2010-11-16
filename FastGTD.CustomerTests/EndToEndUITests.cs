using System.Threading;
using NUnit.Framework;
using White.Core;
using White.Core.UIItems.WindowItems;
using Debug = System.Diagnostics.Debug;

namespace FastGTD.CustomerTests
{
    [TestFixture]
    public class EndToEndUITests
    {
        [Test]
        public void TryWhite()
        {
            var app = Application.Launch("FastGTD.exe");
            Thread.Sleep(5000);
            //app.WaitWhileBusy();
            //var windows = app.GetWindows();
            //foreach (var w in windows)
            //{
            //    Debug.WriteLine(w.Title);
            //}
            //app.WaitWhileBusy();
            //var inbox_window = app.GetWindow("InBox");
            //inbox_window.Focus(DisplayState.Maximized);
           
        }
    }
}
