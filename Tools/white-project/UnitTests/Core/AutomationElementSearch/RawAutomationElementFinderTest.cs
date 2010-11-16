using System.Windows.Automation;
using NUnit.Framework;
using White.Core;
using White.Core.AutomationElementSearch;
using White.Core.Configuration;
using White.Core.UIItems;
using White.UnitTests.Core.Testing;

namespace White.UnitTests.Core.AutomationElementSearch
{
    [TestFixture, WinFormCategory]
    public class RawAutomationElementFinderTest : ControlsActionTest
    {
        protected override void BaseTestFixtureSetup()
        {
            CoreAppXmlConfiguration.Instance.RawElementBasedSearch = true;
            CoreAppXmlConfiguration.Instance.MaxElementSearchDepth = 2;
            base.BaseTestFixtureSetup();
        }

        protected override void BaseTestFixtureTearDown()
        {
            CoreAppXmlConfiguration.Instance.RawElementBasedSearch = false;
            base.BaseTestFixtureTearDown();
        }

        [Test]
        public void Descendant()
        {
            var listView = window.Get<ListView>("listView");
            var finder = new RawAutomationElementFinder(listView.AutomationElement);
            Assert.AreNotEqual(null, finder.Descendant(AutomationSearchCondition.ByControlType(ControlType.HeaderItem).OfName("Key")));
            Assert.AreEqual(null, finder.Descendant(AutomationSearchCondition.ByControlType(ControlType.Header).OfName("Key")));
        }
    }
}