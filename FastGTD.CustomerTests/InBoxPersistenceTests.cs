using System;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;
using StructureMap;

namespace FastGTD.CustomerTests
{
    [TestFixture]
    public class InBoxPersistenceTests
    {
        [Test]
        public void NewInBoxItemIsSaved()
        {
            string item_name = Guid.NewGuid().ToString();

            var inbox = CreateAndShowInBox();
            var model = ObjectFactory.GetInstance<IItemModel<InBoxItem>>();
            model.ClearItems();
            var expected_item = model.Add(item_name);
            inbox.Close();

            var app2 = CreateAndShowInBox();
            var actual_model = ObjectFactory.GetInstance<IItemModel<InBoxItem>>();
            Assert.That(actual_model.Items, Has.Count.EqualTo(1));
            Assert.That(actual_model.Items, Has.Member(expected_item));
            app2.Close();
        }

        [Test]
        public void AddingAndRemovingInBoxResultIsSaved()
        {
            string item_name = Guid.NewGuid().ToString();
            string item_name2 = Guid.NewGuid().ToString();

            var app = CreateAndShowInBox();
            var model = ObjectFactory.GetInstance<IItemModel<InBoxItem>>();
            model.ClearItems();
            var item = model.Add(item_name);
            var item2 = model.Add(item_name2);
            model.Remove(item);
            app.Close();

            var app2 = CreateAndShowInBox();
            var actual_model = ObjectFactory.GetInstance<IItemModel<InBoxItem>>();;
            Assert.That(actual_model.Items, Has.Count.EqualTo(1));
            Assert.That(actual_model.Items, Has.Member(item2));
            Assert.That(actual_model.Items, Has.No.Member(item));
            app2.Close();
        }

        private static InBoxController CreateAndShowInBox()
        {
            FastGTDApp.WireClasses();
            var start_form = FastGTDApp.GetStartForm();
            start_form.Show();
            return start_form;
        }
    }
}