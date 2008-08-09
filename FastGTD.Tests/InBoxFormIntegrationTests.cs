using System;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxFormIntegrationTests
    {
        private InBoxPresenter form;

        [SetUp]
        public void SetupTests()
        {
            form = Program.CreateInBoxForm();
        }

        [Test]
        public void TextBoxStartsWithFocus()
        {
            form.Show();

            Assert.IsFalse(form.View.Form.Focused);
            Assert.IsFalse(form.View.Form.listViewInBoxItems.Focused);
            Assert.IsFalse(form.View.Form.buttonAdd.Focused);
            Assert.IsTrue(form.View.Form.textBox.Focused);
        }

        [Test]
        public void AddingInBoxItemWithButtonClick()
        {
            form.Show();

            form.View.Form.textBox.Text = "foo";
            form.View.Form.buttonAdd.PerformClick();
            Assert.That(form.View.Form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            Assert.That(form.View.Form.listViewInBoxItems.Items[0].Text, Is.EqualTo("foo"));

            form.View.Form.textBox.Text = "bar";
            form.View.Form.buttonAdd.PerformClick();
            Assert.That(form.View.Form.listViewInBoxItems.Items.Count, Is.EqualTo(2));
            Assert.That(form.View.Form.listViewInBoxItems.Items[0].Text, Is.EqualTo("foo"));
            Assert.That(form.View.Form.listViewInBoxItems.Items[1].Text, Is.EqualTo("bar"));
        }

        [Test]
        public void AddingInBoxItemWithEnterKey()
        {
            form.Show();

            Assert.That(form.View.Form.listViewInBoxItems.Items.Count, Is.EqualTo(0));

            form.View.Form.textBox.Text = "foo";

            form.View.Form.PerformKeyDown(Keys.Enter);
            Assert.That(form.View.Form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            Assert.That(form.View.Form.listViewInBoxItems.Items[0].Text, Is.EqualTo("foo"));
        }

        [Test]
        public void TextBoxIsClearedOnAdd()
        {
            form.Show();

            form.View.Form.textBox.Text = "foo";
            Assert.That(form.View.Form.textBox.Text, Is.EqualTo("foo"));
            form.View.Form.AddInboxItemInTextBox();
            Assert.That(form.View.Form.textBox.Text, Is.EqualTo(string.Empty));
        }

        [Test]
        public void DeletingItem()
        {
            form.Show();

            Assert.That(form.View.Form.listViewInBoxItems.Items.Count, Is.EqualTo(0));

            form.View.Form.AddInboxItem("foo");
            Assert.That(form.View.Form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            form.View.Form.AddInboxItem("bar");
            Assert.That(form.View.Form.listViewInBoxItems.Items.Count, Is.EqualTo(2));

            form.View.Form.listViewInBoxItems.Items[1].Selected = true;
            form.View.Form.DeleteSelectedItem();
            Assert.That(form.View.Form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            Assert.That(form.View.Form.listViewInBoxItems.Items[0].Text, Is.EqualTo("foo"));
        }

        [Test]
        public void DeletingItemWithButtonClick()
        {
            form.Show();

            form.View.Form.AddInboxItem("foo");
            form.View.Form.listViewInBoxItems.Items[0].Selected = true;
            form.View.Form.buttonDelete.PerformClick();
            Assert.That(form.View.Form.listViewInBoxItems.Items.Count, Is.EqualTo(0));
        }

        [Test]
        public void DeletingItemWithDeleteKey()
        {
            form.Show();

            form.View.Form.AddInboxItem("foo");
            form.View.Form.listViewInBoxItems.Items[0].Selected = true;
            form.View.Form.PerformKeyDown(Keys.Delete);
            Assert.That(form.View.Form.listViewInBoxItems.Items.Count, Is.EqualTo(0));
        }

        [Test]
        public void DownAndUpKeysChangeSelection()
        {
            form.Show();
            form.View.Form.AddInboxItem("foo1");
            form.View.Form.AddInboxItem("foo2");
            form.View.Form.AddInboxItem("foo3");

            form.View.Form.PerformKeyDown(Keys.Down);
            Assert.That(form.View.Form.listViewInBoxItems.SelectedItems[0].Text, Is.EqualTo("foo1"));
            form.View.Form.PerformKeyDown(Keys.Down);
            Assert.That(form.View.Form.listViewInBoxItems.SelectedItems[0].Text, Is.EqualTo("foo2"));
            form.View.Form.PerformKeyDown(Keys.Down);
            Assert.That(form.View.Form.listViewInBoxItems.SelectedItems[0].Text, Is.EqualTo("foo3"));
            form.View.Form.PerformKeyDown(Keys.Up);
            Assert.That(form.View.Form.listViewInBoxItems.SelectedItems[0].Text, Is.EqualTo("foo2"));
        }

        [Test]
        public void DownAndUpKeysShouldNotCrashOutsideBoundaries()
        {
            form.Show();
            form.View.Form.AddInboxItem("foo1");
            form.View.Form.AddInboxItem("foo2");
            form.View.Form.AddInboxItem("foo3");

            form.View.Form.PerformKeyDown(Keys.Up);
            form.View.Form.PerformKeyDown(Keys.Up);
            Assert.That(form.View.Form.listViewInBoxItems.SelectedItems[0].Text, Is.EqualTo("foo1"));

            form.View.Form.PerformKeyDown(Keys.Down);
            form.View.Form.PerformKeyDown(Keys.Down);
            form.View.Form.PerformKeyDown(Keys.Down);
            form.View.Form.PerformKeyDown(Keys.Down);
            Assert.That(form.View.Form.listViewInBoxItems.SelectedItems[0].Text, Is.EqualTo("foo3"));
        }

        [Test]
        public void DownAndUpKeysShouldNotCrashWhenNoItems()
        {
            form.Show();

            form.View.Form.PerformKeyDown(Keys.Up);
            form.View.Form.PerformKeyDown(Keys.Up);

            form.View.Form.PerformKeyDown(Keys.Down);
            form.View.Form.PerformKeyDown(Keys.Down);
            form.View.Form.PerformKeyDown(Keys.Down);
            form.View.Form.PerformKeyDown(Keys.Down);
        }
 
    }
}