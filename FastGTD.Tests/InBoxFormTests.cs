﻿using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxFormTests
    {
        private IInboxForm form; 

        [SetUp]
        public void SetupTests()
        {
            form = new InBoxForm();
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
            Assert.That(form.FocusedControl.Name, Is.EqualTo("textBox"));
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

            form.AddInboxItem("foo");
            Assert.That(form.InBoxItems.Count, Is.EqualTo(1));
            form.AddInboxItem("bar");
            Assert.That(form.InBoxItems.Count, Is.EqualTo(2));

            form.SelectedItem = form.InBoxItems[1];
            form.DeleteSelectedItem();
            Assert.That(form.InBoxItems.Count, Is.EqualTo(1));
            Assert.That(form.InBoxItems[0], Is.EqualTo("foo"));
        }
        
        [Test]
        public void DeletingItemWithButtonClick()
        {
            form.AddInboxItem("foo");
            form.SelectedItem = form.InBoxItems[0];
            form.ClickControl(InboxFormButton.Delete);
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));
        }

        [Test]
        public void DeletingItemWithDeleteKey()
        {
            form.AddInboxItem("foo");
            form.SelectedItem = form.InBoxItems[0];
            form.PerformKeyDown(Keys.Delete);
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));
        }

        [Test]
        public void DownAndUpKeysChangeSelection()
        {
            form.AddInboxItem("foo1");
            form.AddInboxItem("foo2");
            form.AddInboxItem("foo3");

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
            form.AddInboxItem("foo1");
            form.AddInboxItem("foo2");
            form.AddInboxItem("foo3");

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
    }
}