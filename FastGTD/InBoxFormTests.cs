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
            string new_inbox_item = "foobar";
            InBoxForm form = new InBoxForm();
            form.Show();

            form.textBoxNewItem.Text = new_inbox_item;
            form.buttonAdd.PerformClick();

            Assert.That(form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            Assert.That(form.listViewInBoxItems.Items[0].Text, Is.EqualTo(new_inbox_item));
        }
    }
}
