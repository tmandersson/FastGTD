using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxFormTests
    {
        private string LastHandledEventArgument;
        private InBoxForm form;

        [SetUp]
        public void SetUpTests()
        {
            form = new InBoxForm();
        }

        [Test]
        public void ViewFormReturnsItself()
        {
            IInBoxView view = form;
            Assert.That(view.Form, Is.EqualTo(form));
        }

        [Test]
        public void CanSetFullRowSelect()
        {
            Assert.That(form.listViewInBoxItems.FullRowSelect, Is.False);
            form.InBoxListFullRowSelect = true;
            Assert.That(form.listViewInBoxItems.FullRowSelect, Is.True);
        }

        [Test]
        public void CanSetTextBoxFocus()
        {
            form.Show();
            form.listViewInBoxItems.Focus();
            Assert.IsFalse(form.textBox.Focused);

            form.SetTextBoxFocus();

            Assert.IsFalse(form.Focused);
            Assert.IsFalse(form.listViewInBoxItems.Focused);
            Assert.IsFalse(form.buttonAdd.Focused);
            Assert.IsTrue(form.textBox.Focused);
        }

        [Test]
        public void ClickingAddItemButtonFiresAddItemEvent()
        {
            form.Show();
            form.AddItemAction += HandleAddItemEvent;

            form.textBox.Text = "foo";
            form.buttonAdd.PerformClick();

            Assert.That(LastHandledEventArgument, Is.EqualTo("foo"));
        }

        [Test]
        public void UpdatingInBoxListChangesListControlContents()
        {
            Assert.Fail();
        }

        private void HandleAddItemEvent(string new_in_item)
        {
            LastHandledEventArgument = new_in_item;
        }
    }
}
