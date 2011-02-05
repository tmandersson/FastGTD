using System.Collections.Generic;
using System.Windows.Forms;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;
using Rhino.Mocks;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class InBoxFormWithModelTests
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
            CreateInBoxController();
        }

        private void CreateInBoxController()
        {
            new InBoxController(_form, _form, _model, null, _form);
        }

        [Test]
        public void AddingInBoxItemWithEnterKey()
        {
            _form.TextBoxText = "foo";
            _form.PerformKeyDown(Keys.Enter);

            Assert.That(_model.Items, Has.Count.EqualTo(1));
            Assert.That(_model.Items[0].Name, Is.EqualTo("foo"));
        }
        
        [Test]
        public void SelectingAndDeletingMultipleItems()
        {
            _model.Add("foo");
            _model.Add("foobar");
            _model.Add("bar");
            _model.Add("fubar");
            
            IList<string> items = new List<string> { "foobar", "bar", "fubar" };
            _form.SelectItems(items);
            _form.ClickDeleteButton();

            Assert.That(_model.Items[0].Name, Is.EqualTo("foo"));
        }

        [Test]
        public void DownAndUpKeysShouldNotCrashWhenNoItems()
        {
            _form.PerformKeyDown(Keys.Up);
            _form.PerformKeyDown(Keys.Up);

            _form.PerformKeyDown(Keys.Down);
            _form.PerformKeyDown(Keys.Down);
            _form.PerformKeyDown(Keys.Down);
            _form.PerformKeyDown(Keys.Down);
        }

        [Test]
        public void UpdatingModelShouldntClearListHeader()
        {
            Assert.That(_form.ListHeaderText, Is.EqualTo("New items"));
            _model.Add("foo");
            Assert.That(_form.ListHeaderText, Is.EqualTo("New items"));
        }
    }
}