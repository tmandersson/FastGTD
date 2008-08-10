using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxPresenterTests
    {
        [Test]
        public void AddItemEventAddsItem()
        {
            InboxViewFake view_fake = new InboxViewFake();
            IInBoxView view = view_fake;
            InboxModelFake model_fake = new InboxModelFake();
            IInBoxModel model = model_fake;

            InBoxPresenter inbox = new InBoxPresenter(view, model);
            view_fake.FireAddItemEvent("foo");

            Assert.That(model_fake.LastAddInBoxItemCall, Is.EqualTo("foo"));
        }

        [Test]
        public void PresenterCreation()
        {
            InboxViewFake view_fake = new InboxViewFake();
            IInBoxView view = view_fake;
            IInBoxModel model = new InBoxModel();

            Assert.That(view.InBoxListFullRowSelect, Is.False);
            InBoxPresenter inbox = new InBoxPresenter(view, model);

            Assert.That(view.InBoxListFullRowSelect, Is.True, 
                "Presenter should set InBoxListFullRowSelect.");
            Assert.That(inbox.View, Is.EqualTo(view), 
                "Presenter should have view instance.");
            Assert.That(view_fake.SetTextBoxFocusWasCalled, Is.True,
                "Presenter should call SetTextBoxFocus on view.");
        }
    }

    internal class InboxModelFake : IInBoxModel
    {
        public event ContentUpdatedEvent ContentUpdated;

        public string LastAddInBoxItemCall;

        public IList<string> InboxItems
        {
            get { throw new System.NotImplementedException(); }
        }

        public void AddInboxItem(string new_in_item)
        {
            LastAddInBoxItemCall = new_in_item;
        }
    }

    internal class InboxViewFake : IInBoxView
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