using NUnit.Framework;
using White.Core.UIItems.ListViewItems;
using White.UnitTests.Core.Testing;

namespace White.Core.UIItems
{
    [TestFixture, WinFormCategory]
    public class WinFormTextBoxTest : ControlsActionTest
    {
        private WinFormTextBox textBox;

        protected override void TestFixtureSetUp()
        {
            textBox = (WinFormTextBox) window.Get<TextBox>("textBox");
        }

        [Test]
        public void SuggestionList()
        {
            textBox.Text = "h";
            SuggestionList suggestionList = textBox.SuggestionList;
            Assert.AreEqual(2, suggestionList.Items.Count);
            Assert.AreEqual("hello", suggestionList.Items[0]);
            Assert.AreEqual("hi", suggestionList.Items[1]);
        }

        [Test]
        public void SelectFromSuggestionList()
        {
            textBox.Text = "h";
            SuggestionList suggestionList = textBox.SuggestionList;
            suggestionList.Select("hello");
            Assert.AreEqual("hello", textBox.Text);
        }
    }
}