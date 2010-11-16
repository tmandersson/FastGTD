using NUnit.Framework;
using White.Core.UIItems.ListBoxItems;
using White.Core.UIItems.Scrolling;
using White.UnitTests.Core.Testing;

namespace White.Core.UIItems
{
    [TestFixture]
    public class VScrollBarTest : ControlsActionTest
    {
        private ListBox listBox;
        private IVScrollBar vScrollBar;
        private double smallChange;
        private double largeChange;

        protected override void TestFixtureSetUp()
        {
            listBox = window.Get<ListBox>("listBoxWithVScrollBar");
            vScrollBar = listBox.ScrollBars.Vertical;
            vScrollBar.ScrollDown();
            smallChange = vScrollBar.Value;
            vScrollBar.ScrollUp();
            vScrollBar.ScrollDownLarge();
            largeChange = vScrollBar.Value;
            vScrollBar.ScrollUpLarge();
        }

        [SetUp]
        public void SetUp()
        {
            vScrollBar.SetToMinimum();
        }

        [Test]
        public void ShouldGetVerticalScrollBar()
        {
            Assert.IsNotNull(vScrollBar);
        }

        [Test]
        public void ShouldScrollDown()
        {
            double currentValue = vScrollBar.Value;
            vScrollBar.ScrollDown();
            vScrollBar.ScrollDown();
            vScrollBar.ScrollDown();
            vScrollBar.ScrollDown();
            vScrollBar.ScrollDown();
            Assert.AreEqual(currentValue + (smallChange*5), vScrollBar.Value, 0.001d);
        }

        [Test]
        public void ShouldScrollDownLarge()
        {
            double currentValue = vScrollBar.Value;
            vScrollBar.ScrollDownLarge();
            vScrollBar.ScrollDownLarge();
            vScrollBar.ScrollDownLarge();
            Assert.AreEqual(currentValue + (largeChange*3), vScrollBar.Value, 0.001d);
        }

        [Test]
        public void ShouldScrollUp()
        {
            vScrollBar.SetToMaximum();
            double maxValue = vScrollBar.Value;
            vScrollBar.ScrollUp();
            vScrollBar.ScrollUp();
            vScrollBar.ScrollUp();
            vScrollBar.ScrollUp();
            vScrollBar.ScrollUp();
            Assert.AreEqual(maxValue - (smallChange*5), vScrollBar.Value);
        }

        [Ignore("Causing Visual Studio to crash")]
        public void ShouldScrollUpLarge()
        {
            vScrollBar.ScrollUpLarge();
            vScrollBar.ScrollUpLarge();
            vScrollBar.ScrollUpLarge();
            Assert.AreEqual(50 - (largeChange*3), vScrollBar.Value - 3);
        }
    }
}
