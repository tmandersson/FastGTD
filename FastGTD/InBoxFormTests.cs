using System;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD
{
    [TestFixture]
    public class InBoxFormTests
    {
        [Test]
        public void EmptyInBox()
        {
            InBoxForm form = new InBoxForm();
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(0));
        }

        [Test]
        public void TextBoxStartsWithFocus()
        {
            InBoxForm form = new InBoxForm();
            form.Show(); // TODO: Eliminate need to show dialog when testing.
            
            Assert.IsFalse(form.Focused);
            Assert.IsFalse(form.listViewInBoxItems.Focused);
            Assert.IsFalse(form.buttonAdd.Focused);
            Assert.IsTrue(form.textBox.Focused);
        }

        [Test]
        public void AddingInBoxItemWithButtonClick()
        {
            InBoxForm form = new InBoxForm();
            form.Show(); // TODO: Eliminate need to show dialog when testing.

            string new_inbox_item = "foo";
            form.textBox.Text = new_inbox_item;
            form.buttonAdd.PerformClick();
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            Assert.That(form.listViewInBoxItems.Items[0].Text, Is.EqualTo(new_inbox_item));

            string new_inbox_item2 = "bar";
            form.textBox.Text = new_inbox_item2;
            form.buttonAdd.PerformClick();
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(2));
            Assert.That(form.listViewInBoxItems.Items[0].Text, Is.EqualTo(new_inbox_item));
            Assert.That(form.listViewInBoxItems.Items[1].Text, Is.EqualTo(new_inbox_item2));
        }

        [Test]
        public void AddingInBoxItemWithEnterKey()
        {
            // TODO: Duplication
            InBoxForm form = new InBoxForm();
            form.Show(); // TODO: Eliminate need to show dialog when testing.

            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(0));

            string new_inbox_item = "foo";
            form.textBox.Text = new_inbox_item;

            // TODO: This call seems a bit ugly. Fix or ignore?
            form.KeyDownHandler((object)this, new KeyEventArgs(Keys.Space));
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(0));

            form.KeyDownHandler((object) this, new KeyEventArgs(Keys.Enter));
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            Assert.That(form.listViewInBoxItems.Items[0].Text, Is.EqualTo(new_inbox_item));
        }

        [Test, Ignore]
        public void TextBoxIsClearedOnAdd()
        {
            
        }
    }
}
