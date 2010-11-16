using NUnit.Framework;
using White.UnitTests.Core.Testing;

namespace White.Core.UIItems
{
    [TestFixture, WinFormCategory]
    public class ListViewScenarioTest : ControlsActionTest
    {
        [Test]
        public void Select()
        {
            ListView listView = window.Get<ListView>("listViewForScenarioTest");
            listView.Select("", "foo");
            ListViewRows rows = listView.SelectedRows;
            Assert.AreEqual(1, rows.Count);
            Assert.AreEqual("foo", rows[0].Cells[0].Text);
        }
    }
}