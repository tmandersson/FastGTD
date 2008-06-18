using System;
using NUnit.Framework;

namespace FastGTD
{
    [TestFixture]
    public class InBoxFormFocusProblemTests
    {
        private bool loadEventWasFired;

        [Test]
        public void ShowingInBoxFiresLoadEvent()
        {
            InBoxForm form = new InBoxForm();
            loadEventWasFired = false;
            form.Load += SetLoadEventWasFired;

            form.Show(); // TODO: Eliminate need to show dialog when testing.

            Assert.That(loadEventWasFired);
        }

        private void SetLoadEventWasFired(object sender, EventArgs e)
        {
            loadEventWasFired = true;
        }

        [Test]
        public void SettingFocusOnTextBoxMakesItFocused()
        {
            InBoxForm form = new InBoxForm();
            form.Show();
            form.listViewInBoxItems.Focus();
            Assert.IsFalse(form.textBox.Focused);

            form.textBox.Focus();

            Assert.IsTrue(form.textBox.Focused);
        }
    }
}