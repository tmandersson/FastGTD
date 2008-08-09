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
        public void ViewImplCanSetFullRowSelect()
        {
            InBoxForm view_impl = new InBoxForm();
            IInboxView view = view_impl;
            Assert.That(view_impl.listViewInBoxItems.FullRowSelect, Is.False);
            view.FullRowSelect = true;
            Assert.That(view_impl.listViewInBoxItems.FullRowSelect, Is.True);
        }

        [Test]
        public void PresenterCreation()
        {
            IInboxView view = new InboxViewFake();
            IInboxModel model = new InboxModel();

            Assert.That(view.FullRowSelect, Is.False);
            InBoxPresenter inbox = new InBoxPresenter(view, model);

            Assert.That(view.FullRowSelect, Is.True);
            Assert.That(inbox.View, Is.EqualTo(view));
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

        public InBoxForm Form
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool FullRowSelect
        {
            get { return _fullRowSelect; }
            set { _fullRowSelect = value; }
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }
    }
}