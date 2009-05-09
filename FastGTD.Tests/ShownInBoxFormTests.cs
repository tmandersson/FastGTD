using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class ShownInBoxFormTests
    {
        private TestableInBoxForm _form;
        private InBoxModel _model;

        [SetUp]
        public void SetupTests()
        {
            _model = new InBoxModel(new FakeInBoxItemRepository());
            _form = new TestableInBoxForm();
            var controller = new InBoxController(_form, _model);
            controller.Show();
        }

        [Test]
        public void TextBoxStartsWithFocus()
        {
            Assert.That(_form.FocusedControl, Is.Not.Null);
            Assert.That(_form.FocusedControl, Is.InstanceOfType(typeof(Control)));
            Assert.That(_form.FocusedControl.Name, Is.EqualTo("_textBox"));
        }

        [Test]
        public void AddingInBoxItemWithButtonClick()
        {
            _form.TextBoxText = "foo";
            _form.ClickAddButton();
            Assert.That(_model.Items, Has.Count(1));
            Assert.That(_model.Items[0].Name, Is.EqualTo("foo"));

            _form.TextBoxText = "bar";
            _form.ClickAddButton();
            Assert.That(_model.Items, Has.Count(2));
            Assert.That(_model.Items[0].Name, Is.EqualTo("foo"));
            Assert.That(_model.Items[1].Name, Is.EqualTo("bar"));
        }

        [Test]
        public void TextBoxIsClearedOnAdd()
        {
            _form.TextBoxText = "foo";
            Assert.That(_form.TextBoxText, Is.EqualTo("foo"));
            _form.ClickAddButton();
            Assert.That(_form.TextBoxText, Is.EqualTo(string.Empty));
        }

        [Test]
        public void DeletingItemWithButtonClick()
        {
            _model.CreateItem("foo");
            _form.SelectItem("foo");
            _form.ClickDeleteButton();
            Assert.That(_model.Items, Has.Count(0));
        }

        [Test]
        public void DeletingItemWithDeleteKey()
        {
            _model.CreateItem("foo");
            Assert.That(_model.Items, Has.Count(1));
            _form.SelectItem("foo");
            _form.PerformKeyDown(Keys.Delete);
            Assert.That(_model.Items, Has.Count(0));
        }

        [Test]
        public void DownAndUpKeysChangeSelection()
        {
            _model.CreateItem("foo1");
            _model.CreateItem("foo2");
            _model.CreateItem("foo3");

            _form.PerformKeyDown(Keys.Down);
            Assert.That(_form.SelectedItems[0], Is.EqualTo("foo1"));
            _form.PerformKeyDown(Keys.Down);
            Assert.That(_form.SelectedItems[0], Is.EqualTo("foo2"));
            _form.PerformKeyDown(Keys.Down);
            Assert.That(_form.SelectedItems[0], Is.EqualTo("foo3"));
            _form.PerformKeyDown(Keys.Up);
            Assert.That(_form.SelectedItems[0], Is.EqualTo("foo2"));
        }

        [Test]
        public void DownAndUpKeysShouldNotCrashOutsideBoundaries()
        {
            _model.CreateItem("foo1");
            _model.CreateItem("foo2");
            _model.CreateItem("foo3");

            _form.PerformKeyDown(Keys.Up);
            _form.PerformKeyDown(Keys.Up);
            Assert.That(_form.SelectedItems[0], Is.EqualTo("foo1"));

            _form.PerformKeyDown(Keys.Down);
            _form.PerformKeyDown(Keys.Down);
            _form.PerformKeyDown(Keys.Down);
            _form.PerformKeyDown(Keys.Down);
            Assert.That(_form.SelectedItems[0], Is.EqualTo("foo3"));
        }
    }
}