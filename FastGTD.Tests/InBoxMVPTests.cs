using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxMVPTests
    {
        [Test]
        public void ModelStartsWithEmptyInbox()
        {
            IInboxModel model = new InboxModel();
            Assert.That(model.InboxItems.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddingInBoxItemWithButtonClick()
        {
            InBoxPresenter form = Program.CreateInBoxForm();
            form.Show();

            form.View.Form.textBox.Text = "foo";
            form.View.Form.buttonAdd.PerformClick();
            Assert.That(form.View.Form.listViewInBoxItems.Items.Count, Is.EqualTo(1));
            Assert.That(form.View.Form.listViewInBoxItems.Items[0].Text, Is.EqualTo("foo"));

            form.View.Form.textBox.Text = "bar";
            form.View.Form.buttonAdd.PerformClick();
            Assert.That(form.View.Form.listViewInBoxItems.Items.Count, Is.EqualTo(2));
            Assert.That(form.View.Form.listViewInBoxItems.Items[0].Text, Is.EqualTo("foo"));
            Assert.That(form.View.Form.listViewInBoxItems.Items[1].Text, Is.EqualTo("bar"));
        }

        [Test]
        public void AddItemEventAddsItem()
        {
            InboxViewFake view_fake = new InboxViewFake();
            IInboxView view = view_fake;
            InboxModelFake model_fake = new InboxModelFake();
            IInboxModel model = model_fake;

            InBoxPresenter inbox = new InBoxPresenter(view, model);
            view_fake.FireAddItemEvent("foo");

            Assert.That(model_fake.LastAddInBoxItemCall, Is.EqualTo("foo"));
        }

        [Test]
        public void PresenterCreation()
        {
            InboxViewFake view_fake = new InboxViewFake();
            IInboxView view = view_fake;
            IInboxModel model = new InboxModel();

            Assert.That(view.InBoxListFullRowSelect, Is.False);
            InBoxPresenter inbox = new InBoxPresenter(view, model);

            Assert.That(view.InBoxListFullRowSelect, Is.True, 
                "Presenter should set InBoxListFullRowSelect.");
            Assert.That(inbox.View, Is.EqualTo(view), 
                "Presenter should have view instance.");
            Assert.That(view_fake.SetTextBoxFocusWasCalled, Is.True,
                "Presenter should call SetTextBoxFocus on view.");
        }

        [Test]
        public void ViewFormReturnsItself()
        {
            InBoxForm view_impl = new InBoxForm();
            IInboxView view = view_impl;
            Assert.That(view.Form, Is.EqualTo(view_impl));
        }

        [Test]
        public void InboxCreationWithFactory()
        {
            
        }
    }

    internal class InboxModelFake : IInboxModel
    {
        public string LastAddInBoxItemCall;

        public ICollection<string> InboxItems
        {
            get { throw new System.NotImplementedException(); }
        }

        public void AddInboxItem(string new_in_item)
        {
            LastAddInBoxItemCall = new_in_item;
        }
    }

    internal class InboxViewFake : IInboxView
    {
        private bool _fullRowSelect;
        public bool SetTextBoxFocusWasCalled = false;

        public event AddItemEvent AddItemAction;

        public InBoxForm Form
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool InBoxListFullRowSelect
        {
            get { return _fullRowSelect; }
            set { _fullRowSelect = value; }
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }

        public void SetTextBoxFocus()
        {
            SetTextBoxFocusWasCalled = true;
        }

        public void FireAddItemEvent(string item)
        {
            AddItemAction(item);
        }
    }
}