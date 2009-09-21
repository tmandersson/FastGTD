using System;
using FastGTD.Domain;
using NUnit.Framework;

namespace FastGTD.DataAccessTests
{
    public abstract class ItemRepositoryTests<TItem> where TItem:GTDItem
    {
        protected abstract TItem CreateItem(string name);
        protected abstract IItemPersistence<TItem> CreateRepo();

        [Test]
        public void Save_WithAnItem_SavesIt()
        {
            string name = Guid.NewGuid().ToString();
            var repo = CreateRepo();
            var item = CreateItem(name);
            repo.Save(item);

            var repo2 = CreateRepo();
            var actual_item = repo2.GetById(item.Id);
            Assert.That(actual_item.Name, Is.EqualTo(name));
        }
    }
}