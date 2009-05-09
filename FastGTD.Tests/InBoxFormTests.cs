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
            model.Add("foo");
            model.Add("foobar");
            model.Add("bar");
            model.Add("fubar");
            
            IList<string> items = new List<string> { "foobar", "bar", "fubar" };
            form.SelectItems(items);
            form.ClickDeleteButton();

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
            model.Add("foo");
            Assert.That(form.ListHeaderText, Is.EqualTo("New items"));
        }
    }
}