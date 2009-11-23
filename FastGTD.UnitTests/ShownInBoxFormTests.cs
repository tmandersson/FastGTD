using System.Collections.Generic;
using System.Windows.Forms;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class ShownInBoxFormTests
    {
        private TestableInBoxForm _form;
        private ItemModel<InBoxItem> _model;

        [SetUp]
        public void SetupTests()
        {
            var repository = MockRepository.GenerateStub<IItemPersistence<InBoxItem>>();
            repository.Stub(x => x.GetAll()).Return(new List<InBoxItem>());
            _model = new ItemModel<InBoxItem>(repository);
            _form = new TestableInBoxForm();
            var controller = CreateInboxController();
            controller.Show();
        }

        private InBoxController CreateInboxController()
        {
            FastGTDApp.WireClasses();
            ObjectFactory.Inject(_form);
            ObjectFactory.Inject(_model);
            return ObjectFactory.GetInstance<InBoxController>();
        }

        [Test]
        public void AddingInBoxItemWithButtonClick()
        {
            _form.TextBoxText = "foo";
            _form.ClickAddButton();
            Assert.That(_model.Items, Has.Count.EqualTo(1));
            Assert.That(_model.Items[0].Name, Is.EqualTo("foo"));

            _form.TextBoxText = "bar";
            _form.ClickAddButton();
            Assert.That(_model.Items, Has.Count.EqualTo(2));
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
            _model.Add("foo");
            _form.SelectItem("foo");
            _form.ClickDeleteButton();
            Assert.That(_model.Items, Has.Count.EqualTo(0));
        }

        [Test]
        public void DeletingItemWithDeleteKey()
        {
            _model.Add("foo");
            Assert.That(_model.Items, Has.Count.EqualTo(1));
            _form.SelectItem("foo");
            _form.PerformKeyDown(Keys.Delete);
            Assert.That(_model.Items, Has.Count.EqualTo(0));
        }

        [Test]
        public void DownAndUpKeysChangeSelection()
        {
            _model.Add("foo1");
            _model.Add("foo2");
            _model.Add("foo3");

            _form.PerformKeyDown(Keys.Down);
            Assert.That(_form.SelectedItems, Has.Count.EqualTo(1));
            Assert.That(_form.SelectedItems, Has.All.Property("Name").EqualTo("foo1"));
            _form.PerformKeyDown(Keys.Down);
            Assert.That(_form.SelectedItems, Has.Count.EqualTo(1));
            Assert.That(_form.SelectedItems, Has.All.Property("Name").EqualTo("foo2"));
            _form.PerformKeyDown(Keys.Down);
            Assert.That(_form.SelectedItems, Has.Count.EqualTo(1));
            Assert.That(_form.SelectedItems, Has.All.Property("Name").EqualTo("foo3"));
            _form.PerformKeyDown(Keys.Up);
            Assert.That(_form.SelectedItems, Has.Count.EqualTo(1));
            Assert.That(_form.SelectedItems, Has.All.Property("Name").EqualTo("foo2"));
        }

        [Test]
        public void DownAndUpKeysShouldNotCrashOutsideBoundaries()
        {
            _model.Add("foo1");
            _model.Add("foo2");
            _model.Add("foo3");

            _form.PerformKeyDown(Keys.Up);
            _form.PerformKeyDown(Keys.Up);
            Assert.That(_form.SelectedItems, Has.Count.EqualTo(1));
            Assert.That(_form.SelectedItems, Has.All.Property("Name").EqualTo("foo1"));

            _form.PerformKeyDown(Keys.Down);
            _form.PerformKeyDown(Keys.Down);
            _form.PerformKeyDown(Keys.Down);
            _form.PerformKeyDown(Keys.Down);
            Assert.That(_form.SelectedItems, Has.Count.EqualTo(1));
            Assert.That(_form.SelectedItems, Has.All.Property("Name").EqualTo("foo3"));
        }
    }
}