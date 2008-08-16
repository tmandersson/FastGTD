using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxFormTests
    {
        private IInboxForm form;
        private InBoxModel model;

        [SetUp]
        public void SetupTests()
        {
            model = new InBoxModel();
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
            form.ClickControl(InboxFormButton.Add);
            Assert.That(form.InBoxItems.Count, Is.EqualTo(1));
            Assert.That(form.InBoxItems[0], Is.EqualTo("foo"));

            form.TextBoxValue = "bar";
            form.ClickControl(InboxFormButton.Add);
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
            form.ClickControl(InboxFormButton.Add);
            Assert.That(form.TextBoxValue, Is.EqualTo(string.Empty));
        }

        [Test]
        public void DeletingItem()
        {
            // TODO: Move part of test (selection should stay?) and code to a model class
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));

            model.AddItem("foo");
            Assert.That(form.InBoxItems.Count, Is.EqualTo(1));
            model.AddItem("bar");
            Assert.That(form.InBoxItems.Count, Is.EqualTo(2));

            form.SelectedItem = form.InBoxItems[1];
            form.DeleteSelectedItems();
            Assert.That(form.InBoxItems.Count, Is.EqualTo(1));
            Assert.That(form.InBoxItems[0], Is.EqualTo("foo"));
        }
        
        [Test]
        public void DeletingItemWithButtonClick()
        {
            model.AddItem("foo");
            form.SelectedItem = form.InBoxItems[0];
            form.ClickControl(InboxFormButton.Delete);
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));
        }

        [Test]
        public void DeletingItemWithDeleteKey()
        {
            model.AddItem("foo");
            form.SelectedItem = form.InBoxItems[0];
            form.PerformKeyDown(Keys.Delete);
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));
        }

        [Test]
        public void DownAndUpKeysChangeSelection()
        {
            model.AddItem("foo1");
            model.AddItem("foo2");
            model.AddItem("foo3");

            form.PerformKeyDown(Keys.Down);
            Assert.That(form.SelectedItem, Is.EqualTo("foo1"));
            form.PerformKeyDown(Keys.Down);
            Assert.That(form.SelectedItem, Is.EqualTo("foo2"));
            form.PerformKeyDown(Keys.Down);
            Assert.That(form.SelectedItem, Is.EqualTo("foo3"));
            form.PerformKeyDown(Keys.Up);
            Assert.That(form.SelectedItem, Is.EqualTo("foo2"));
        }

        [Test]
        public void DownAndUpKeysShouldNotCrashOutsideBoundaries()
        {
            model.AddItem("foo1");
            model.AddItem("foo2");
            model.AddItem("foo3");

            form.PerformKeyDown(Keys.Up);
            form.PerformKeyDown(Keys.Up);
            Assert.That(form.SelectedItem, Is.EqualTo("foo1"));

            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            Assert.That(form.SelectedItem, Is.EqualTo("foo3"));
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
}