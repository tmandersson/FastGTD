using NUnit.Framework;
using White.UnitTests.Core.Testing;

namespace White.Core.UIItems
{
    [TestFixture, WinFormCategory, WPFCategory]
    public class VerticalThumbTest : ControlsActionTest
    {
        protected override string CommandLineArguments
        {
            get { return VerticalGridSplitter; }
        }

        [Test]
        public void Find()
        {
            var thumb = window.Get<Thumb>("splitter");
            Assert.AreNotEqual(null, thumb);
        }

        [Test]
        public void Slide()
        {
            var thumb = window.Get<Thumb>("splitter");
            double originalY = thumb.Location.Y;
            thumb.SlideVertically(50);
            Assert.AreEqual(originalY + 50, thumb.Location.Y);
        }
    }
}