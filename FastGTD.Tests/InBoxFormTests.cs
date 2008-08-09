using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxFormTests
    {
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
    }
}
