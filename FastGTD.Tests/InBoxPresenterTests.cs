using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxPresenterTests
    {
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

        [Test]
        public void AddItemEventAddsItemInModel()
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
        public void UpdateViewWhenModelIsUpdated()
        {
            IInBoxView view = new InboxViewFake();
            InboxModelFake model_fake = new InboxModelFake();
            IInBoxModel model = model_fake;
            new InBoxPresenter(view, model);

            model_fake.FireContentUpdatedEvent();

            Assert.That(view.InBoxList, Is.EqualTo(model.InboxItems));
        }

        [Test, Ignore]
        public void ViewKeepsSelectedItemWhenContentIsUpdated()
        {
            
        }
    }

    internal class InboxModelFake : IInBoxModel
    {
        public event ContentUpdatedEvent ContentUpdated;

        public string LastAddInBoxItemCall;

        public IList<string> InboxItems
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("foo");
                list.Add("boo");
                return list;
            }
        }

        public void AddInboxItem(string new_in_item)
        {
            LastAddInBoxItemCall = new_in_item;
        }

        public void FireContentUpdatedEvent()
        {
            ContentUpdated();
        }
    }

    internal class InboxViewFake : IInBoxView
    {
        private bool _fullRowSelect;
        public bool SetTextBoxFocusWasCalled = false;
        private IList<string> inBoxList;

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

        public IList<string> InBoxList
        {
            get { return inBoxList; }
            set { inBoxList = value; }
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