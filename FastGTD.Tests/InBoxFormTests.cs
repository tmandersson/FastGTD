using System.Collections.Generic;
using System.Windows.Forms;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxFormTests
    {
        private IInBoxForm form;
        private InBoxModel model;

        [SetUp]
        public void SetupTests()
        {
            model = new InBoxModel(new FakeInBoxItemRepository());
            form = new InBoxForm(model);
            form.Show(); // TODO: Eliminate need to show dialog when testing.
        }

        [Test]
        public void InBoxStartsEmpty()
        {
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));
        }

        [Test]
        public void TextBoxStartsWithFocus()
        {
            Assert.That(form.FocusedControl, Is.Not.Null);
            Assert.That(form.FocusedControl, Is.InstanceOfType(typeof(Control)));
            Assert.That(form.FocusedControl.Name, Is.EqualTo("_textBox"));
        }

        [Test]
        public void AddingInBoxItemWithButtonClick()
        {
            form.TextBoxValue = "foo";
            form.ClickControl(InBoxFormButton.Add);
            Assert.That(form.InBoxItems.Count, Is.EqualTo(1));
            Assert.That(form.InBoxItems[0], Is.EqualTo("foo"));

            form.TextBoxValue = "bar";
            form.ClickControl(InBoxFormButton.Add);
            Assert.That(form.InBoxItems.Count, Is.EqualTo(2));
            Assert.That(form.InBoxItems[0], Is.EqualTo("foo"));
            Assert.That(form.InBoxItems[1], Is.EqualTo("bar"));
        }

        [Test]
        public void AddingInBoxItemWithEnterKey()
        {
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));

            form.TextBoxValue = "foo";
            form.PerformKeyDown(Keys.Enter);

            Assert.That(form.InBoxItems.Count, Is.EqualTo(1));
            Assert.That(form.InBoxItems[0], Is.EqualTo("foo"));
        }

        [Test]
        public void TextBoxIsClearedOnAdd()
        {
            form.TextBoxValue = "foo";
            Assert.That(form.TextBoxValue, Is.EqualTo("foo"));
            form.ClickControl(InBoxFormButton.Add);
            Assert.That(form.TextBoxValue, Is.EqualTo(string.Empty));
        }

        [Test]
        public void DeletingItem()
        {
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));

            model.AddItem("foo");
            Assert.That(form.InBoxItems.Count, Is.EqualTo(1));
            model.AddItem("bar");
            Assert.That(form.InBoxItems.Count, Is.EqualTo(2));

            model.RemoveItem("bar");
            Assert.That(form.InBoxItems.Count, Is.EqualTo(1));
            Assert.That(form.InBoxItems[0], Is.EqualTo("foo"));
        }

        [Test]
        public void ClearItems()
        {
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));

            model.AddItem("foo");
            Assert.That(form.InBoxItems.Count, Is.EqualTo(1));
            model.AddItem("bar");
            Assert.That(form.InBoxItems.Count, Is.EqualTo(2));

            model.ClearItems();
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void DeletingItemWithButtonClick()
        {
            model.AddItem("foo");
            form.SelectItem("foo");
            form.ClickControl(InBoxFormButton.Delete);
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));
        }

        [Test]
        public void DeletingItemWithDeleteKey()
        {
            model.AddItem("foo");
            form.SelectItem("foo");
            form.PerformKeyDown(Keys.Delete);
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));
        }

        [Test]
        public void SelectingAndDeletingMultipleItems()
        {
            model.AddItem("foo");
            model.AddItem("foobar");
            model.AddItem("bar");
            model.AddItem("fubar");
            
            IList<string> items = new List<string> { "foobar", "bar", "fubar" };
            form.SelectItems(items);
            form.ClickControl(InBoxFormButton.Delete);

            IList<string> expectedItems = new List<string> {"foo"};
            Assert.That(model.Items, Is.EqualTo(expectedItems));
        }

        [Test]
        public void DownAndUpKeysChangeSelection()
        {
            model.AddItem("foo1");
            model.AddItem("foo2");
            model.AddItem("foo3");

            form.PerformKeyDown(Keys.Down);
            Assert.That(form.SelectedItems[0], Is.EqualTo("foo1"));
            form.PerformKeyDown(Keys.Down);
            Assert.That(form.SelectedItems[0], Is.EqualTo("foo2"));
            form.PerformKeyDown(Keys.Down);
            Assert.That(form.SelectedItems[0], Is.EqualTo("foo3"));
            form.PerformKeyDown(Keys.Up);
            Assert.That(form.SelectedItems[0], Is.EqualTo("foo2"));
        }

        [Test]
        public void DownAndUpKeysShouldNotCrashOutsideBoundaries()
        {
            model.AddItem("foo1");
            model.AddItem("foo2");
            model.AddItem("foo3");

            form.PerformKeyDown(Keys.Up);
            form.PerformKeyDown(Keys.Up);
            Assert.That(form.SelectedItems[0], Is.EqualTo("foo1"));

            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            Assert.That(form.SelectedItems[0], Is.EqualTo("foo3"));
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
            model.AddItem("foo");
            Assert.That(form.ListHeaderText, Is.EqualTo("New items"));
        }
    }

    public class FakeInBoxItemRepository : IInBoxItemRepository
    {
        public IList<InBoxItem> GetAll()
        {
            return new List<InBoxItem>();
        }

        public void DeleteAll()
        {
        }
    }
}