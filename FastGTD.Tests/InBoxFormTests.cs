using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxFormTests
    {
        private string LastHandledEventArgument;

        [Test]
        public void CanSetFullRowSelect()
        {
            InBoxForm form = new InBoxForm();
            Assert.That(form.listViewInBoxItems.FullRowSelect, Is.False);
            form.InBoxListFullRowSelect = true;
            Assert.That(form.listViewInBoxItems.FullRowSelect, Is.True);
        }

        [Test]
        public void CanSetTextBoxFocus()
        {
            InBoxForm form = new InBoxForm();
            form.Show();
            form.listViewInBoxItems.Focus();
            Assert.IsFalse(form.textBox.Focused);

            form.SetTextBoxFocus();

            Assert.IsFalse(form.Focused);
            Assert.IsFalse(form.listViewInBoxItems.Focused);
            Assert.IsFalse(form.buttonAdd.Focused);
            Assert.IsTrue(form.textBox.Focused);
        }

        [Test, Ignore]
        public void ClickingAddItemButtonFiresAddItemEvent()
        {
            InBoxForm form = new InBoxForm();
            form.AddItemAction += HandleAddItemEvent;

            form.textBox.Text = "foo";
            form.buttonAdd.PerformClick();

            Assert.That(LastHandledEventArgument, Is.EqualTo("foo"));
        }

        private void HandleAddItemEvent(string new_in_item)
        {
            LastHandledEventArgument = new_in_item;
        }
    }
}
