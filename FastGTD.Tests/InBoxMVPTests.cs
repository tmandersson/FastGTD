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

    internal class InboxViewFake : IInboxView
    {
        private bool _fullRowSelect;
        public bool SetTextBoxFocusWasCalled = false;

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
    }
}