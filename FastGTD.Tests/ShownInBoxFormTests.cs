using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class ShownInBoxFormTests
    {
        private TestableInBoxForm form;
        private InBoxModel model;

        [SetUp]
        public void SetupTests()
        {
            model = new InBoxModel(new FakeInBoxItemRepository());
            form = new TestableInBoxForm();
            var controller = new InBoxController(form, model);
            controller.Show(); // TODO: Eliminate need to show dialog when testing.
        }

        [Test]
        public void TextBoxStartsWithFocus()
        {
            Assert.That(form.FocusedControl, Is.Not.Null);
            Assert.That(form.FocusedControl, Is.InstanceOfType(typeof(Control)));
            Assert.That(form.FocusedControl.Name, Is.EqualTo("_textBox"));
        }

        [Test]
        public void AddingInBoxItemWithButtonClick()
        {
            form.TextBoxText = "foo";
            form.ClickAddButton();
            Assert.That(model.Items, Has.Count(1));
            Assert.That(model.Items[0].Name, Is.EqualTo("foo"));

            form.TextBoxText = "bar";
            form.ClickAddButton();
            Assert.That(model.Items, Has.Count(2));
            Assert.That(model.Items[0].Name, Is.EqualTo("foo"));
            Assert.That(model.Items[1].Name, Is.EqualTo("bar"));
        }

        [Test]
        public void TextBoxIsClearedOnAdd()
        {
            form.TextBoxText = "foo";
            Assert.That(form.TextBoxText, Is.EqualTo("foo"));
            form.ClickAddButton();
            Assert.That(form.TextBoxText, Is.EqualTo(string.Empty));
        }

        [Test]
        public void DeletingItemWithButtonClick()
        {
            model.CreateItem("foo");
            form.SelectItem("foo");
            form.ClickDeleteButton();
            Assert.That(model.Items, Has.Count(0));
        }

        [Test]
        public void DeletingItemWithDeleteKey()
        {
            model.CreateItem("foo");
            Assert.That(model.Items, Has.Count(1));
            form.SelectItem("foo");
            form.PerformKeyDown(Keys.Delete);
            Assert.That(model.Items, Has.Count(0));
        }

        [Test]
        public void DownAndUpKeysChangeSelection()
        {
            model.CreateItem("foo1");
            model.CreateItem("foo2");
            model.CreateItem("foo3");

            form.PerformKeyDown(Keys.Down);
            Assert.That(form.SelectedItems[0], Is.EqualTo("foo1"));
            form.PerformKeyDown(Keys.Down);
            Assert.That(form.SelectedItems[0], Is.EqualTo("foo2"));
            form.PerformKeyDown(Keys.Down);
            Assert.That(form.SelectedItems[0], Is.EqualTo("foo3"));
            form.PerformKeyDown(Keys.Up);
            Assert.That(form.SelectedItems[0], Is.EqualTo("foo2"));
        }

        [Test]
        public void DownAndUpKeysShouldNotCrashOutsideBoundaries()
        {
            model.CreateItem("foo1");
            model.CreateItem("foo2");
            model.CreateItem("foo3");

            form.PerformKeyDown(Keys.Up);
            form.PerformKeyDown(Keys.Up);
            Assert.That(form.SelectedItems[0], Is.EqualTo("foo1"));

            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            form.PerformKeyDown(Keys.Down);
            Assert.That(form.SelectedItems[0], Is.EqualTo("foo3"));
        }
    }
}