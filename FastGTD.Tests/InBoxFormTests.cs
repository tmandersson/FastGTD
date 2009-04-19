using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxFormTests
    {
        private TestableInBoxForm form;
        private InBoxModel model;

        [SetUp]
        public void SetupTests()
        {
            model = new InBoxModel(new FakeInBoxItemRepository());
            form = new TestableInBoxForm();
            new InBoxController(form, model);
        }

        [Test]
        public void AddingInBoxItemWithEnterKey()
        {
            form.TextBoxText = "foo";
            form.PerformKeyDown(Keys.Enter);

            Assert.That(model.Items, Has.Count(1));
            Assert.That(model.Items[0].Name, Is.EqualTo("foo"));
        }
        
        [Test]
        public void SelectingAndDeletingMultipleItems()
        {
            model.CreateItem("foo");
            model.CreateItem("foobar");
            model.CreateItem("bar");
            model.CreateItem("fubar");
            
            IList<string> items = new List<string> { "foobar", "bar", "fubar" };
            form.SelectItems(items);
            form.ClickControl(InBoxFormButton.Delete);

            Assert.That(model.Items[0].Name, Is.EqualTo("foo"));
        }

        [Test]
        public void DownAndUpKeysShouldNotCrashWhenNoItems()
        {
            form.PerformKeyDown(Keys.Up);
            form.PerformKeyDown(Keys.Up);

            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
        }

        [Test]
        public void UpdatingModelShouldntClearListHeader()
        {
            Assert.That(form.ListHeaderText, Is.EqualTo("New items"));
            model.CreateItem("foo");
            Assert.That(form.ListHeaderText, Is.EqualTo("New items"));
        }
    }
}