using System;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxFormIntegrationTests
    {
        private InBoxForm form;

        [SetUp]
        public void SetupTests()
        {
            form = Program.CreateInBoxForm();
        }

        [Test]
        public void InboxCreation()
        {
            form.Show();

            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(0));
            Assert.That(form.listViewInBoxItems.FullRowSelect);
        }

        [Test]
        public void TextBoxStartsWithFocus()
        {
            form.Show();

            Assert.IsFalse(form.Focused);
            Assert.IsFalse(form.listViewInBoxItems.Focused);
            Assert.IsFalse(form.buttonAdd.Focused);
            Assert.IsTrue(form.textBox.Focused);
        }

        [Test]
        public void AddingInBoxItemWithButtonClick()
        {
            form.Show();

            form.textBox.Text = "foo";
            form.buttonAdd.PerformClick();
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            Assert.That(form.listViewInBoxItems.Items[0].Text, Is.EqualTo("foo"));

            form.textBox.Text = "bar";
            form.buttonAdd.PerformClick();
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(2));
            Assert.That(form.listViewInBoxItems.Items[0].Text, Is.EqualTo("foo"));
            Assert.That(form.listViewInBoxItems.Items[1].Text, Is.EqualTo("bar"));
        }

        [Test]
        public void AddingInBoxItemWithEnterKey()
        {
            form.Show();

            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(0));

            form.textBox.Text = "foo";

            form.PerformKeyDown(Keys.Enter);
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            Assert.That(form.listViewInBoxItems.Items[0].Text, Is.EqualTo("foo"));
        }

        [Test]
        public void TextBoxIsClearedOnAdd()
        {
            form.Show();

            form.textBox.Text = "foo";
            Assert.That(form.textBox.Text, Is.EqualTo("foo"));
            form.AddInboxItemInTextBox();
            Assert.That(form.textBox.Text, Is.EqualTo(string.Empty));
        }

        [Test]
        public void DeletingItem()
        {
            form.Show();

            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(0));

            form.AddInboxItem("foo");
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            form.AddInboxItem("bar");
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(2));

            form.listViewInBoxItems.Items[1].Selected = true;
            form.DeleteSelectedItem();
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            Assert.That(form.listViewInBoxItems.Items[0].Text, Is.EqualTo("foo"));
        }

        [Test]
        public void DeletingItemWithButtonClick()
        {
            form.Show();

            form.AddInboxItem("foo");
            form.listViewInBoxItems.Items[0].Selected = true;
            form.buttonDelete.PerformClick();
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(0));
        }

        [Test]
        public void DeletingItemWithDeleteKey()
        {
            form.Show();

            form.AddInboxItem("foo");
            form.listViewInBoxItems.Items[0].Selected = true;
            form.PerformKeyDown(Keys.Delete);
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(0));
        }

        [Test]
        public void DownAndUpKeysChangeSelection()
        {
            form.Show();
            form.AddInboxItem("foo1");
            form.AddInboxItem("foo2");
            form.AddInboxItem("foo3");

            form.PerformKeyDown(Keys.Down);
            Assert.That(form.listViewInBoxItems.SelectedItems[0].Text, Is.EqualTo("foo1"));
            form.PerformKeyDown(Keys.Down);
            Assert.That(form.listViewInBoxItems.SelectedItems[0].Text, Is.EqualTo("foo2"));
            form.PerformKeyDown(Keys.Down);
            Assert.That(form.listViewInBoxItems.SelectedItems[0].Text, Is.EqualTo("foo3"));
            form.PerformKeyDown(Keys.Up);
            Assert.That(form.listViewInBoxItems.SelectedItems[0].Text, Is.EqualTo("foo2"));
        }

        [Test]
        public void DownAndUpKeysShouldNotCrashOutsideBoundaries()
        {
            form.Show();
            form.AddInboxItem("foo1");
            form.AddInboxItem("foo2");
            form.AddInboxItem("foo3");

            form.PerformKeyDown(Keys.Up);
            form.PerformKeyDown(Keys.Up);
            Assert.That(form.listViewInBoxItems.SelectedItems[0].Text, Is.EqualTo("foo1"));

            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            Assert.That(form.listViewInBoxItems.SelectedItems[0].Text, Is.EqualTo("foo3"));
        }

        [Test]
        public void DownAndUpKeysShouldNotCrashWhenNoItems()
        {
            form.Show();

            form.PerformKeyDown(Keys.Up);
            form.PerformKeyDown(Keys.Up);

            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
        }
 
    }
}