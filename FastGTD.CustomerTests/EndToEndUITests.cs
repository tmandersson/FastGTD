using System.Threading;
using NUnit.Framework;
using White.Core;

namespace FastGTD.CustomerTests
{
    [TestFixture]
    public class EndToEndUITests
    {
        [Test]
        public void TryWhite()
        {
            var app = Application.Launch("FastGTD.exe");
            //var inbox_window = app.GetWindow("");
            
            app.Kill();
        }
    }
}
