using System;
using System.Collections.Generic;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.IntegrationTests
{
    [TestFixture]
    public class InBoxModelWithInBoxItemRepositoryTests
    {
        [Test]
        public void SaveNewItems()
        {
            const string ITEM_NAME = "hej";
            IItemModel<InBoxItem> model = CreateModel();
            model.Load();
            model.ClearItems();

            var expected_item = model.Add(ITEM_NAME);

            var persisted_model = CreateModel();
            persisted_model.Load();
            Assert.That(persisted_model.Items.Count, Is.EqualTo(1));
            Assert.That(persisted_model.Items, Has.Member(expected_item));
        }

        private static IItemModel<InBoxItem> CreateModel()
        {
            return new ItemModel<InBoxItem>(new InBoxItemRepository());
        }
    }
}