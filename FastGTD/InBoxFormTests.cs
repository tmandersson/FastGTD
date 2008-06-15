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
        public void AddingInBoxItemWithButtonClick()
        {
            InBoxForm form = new InBoxForm();
            form.Show(); // TODO: Eliminate need to show dialog when testing.

            string new_inbox_item = "foo";
            form.textBoxNewItem.Text = new_inbox_item;
            form.buttonAdd.PerformClick();
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            Assert.That(form.listViewInBoxItems.Items[0].Text, Is.EqualTo(new_inbox_item));

            string new_inbox_item2 = "bar";
            form.textBoxNewItem.Text = new_inbox_item2;
            form.buttonAdd.PerformClick();
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(2));
            Assert.That(form.listViewInBoxItems.Items[0].Text, Is.EqualTo(new_inbox_item));
            Assert.That(form.listViewInBoxItems.Items[1].Text, Is.EqualTo(new_inbox_item2));
        }

        [Test]
        public void AddingInBoxItemWithEnterKey()
        {
            InBoxForm form = new InBoxForm();
            form.Show(); // TODO: Eliminate need to show dialog when testing.

            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(0));

            string new_inbox_item = "foo";
            form.textBoxNewItem.Text = new_inbox_item;

            form.KeyDownHandler((object)this, new KeyEventArgs(Keys.Space));
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(0));

            form.KeyDownHandler((object) this, new KeyEventArgs(Keys.Enter));
            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            Assert.That(form.listViewInBoxItems.Items[0].Text, Is.EqualTo(new_inbox_item));
        }
    }
}
