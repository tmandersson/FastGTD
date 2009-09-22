using System;
using System.Collections.Generic;
using FastGTD.Domain;
using NUnit.Framework;

namespace FastGTD.DataAccessTests
{
    public abstract class ItemRepositoryFixtureBase<TItem> where TItem:GTDItem
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

        [Test]
        public void GetAll_LoadsExistingItems()
        {
            string name = Guid.NewGuid().ToString();
            var repo = CreateRepo();
            var item = CreateItem(name);
            repo.Save(item);

            var repo2 = CreateRepo();
            var actual_items = repo2.GetAll();
            Assert.That(actual_items, Has.Member(item));
        }

        [Test]
        public void GetAll_LoadsExistingItems2()
        {
            string name = Guid.NewGuid().ToString();
            var repo = CreateRepo();
            IList<TItem> existing = repo.GetAll();
            int expected_count = existing.Count + 1;
            var item = CreateItem(name);
            repo.Save(item);

            var repo2 = CreateRepo();
            IList<TItem> result = repo2.GetAll();

            Assert.That(result, Has.Count.EqualTo(expected_count));
            Assert.That(result, Has.Some.Property("Name").EqualTo(name));
        }

        [Test]
        public void DeleteAll_DeletesAllItems()
        {
            string name = Guid.NewGuid().ToString();
            var repo = CreateRepo();
            var item = CreateItem(name);
            repo.Save(item);

            repo.DeleteAll();

            var repo2 = CreateRepo();
            var actual_actions = repo2.GetAll();
            Assert.That(actual_actions, Has.Count.EqualTo(0));
        }

        [Test]
        public void Delete_OnOneOfThreeCreatedItems_ChangesTotalCountToTwo()
        {
            string name = Guid.NewGuid().ToString();
            var repo = CreateRepo();
            repo.DeleteAll();
            var item1 = CreateItem(name);
            repo.Save(item1);
            var item2 = CreateItem(name);
            repo.Save(item2);
            var item3 = CreateItem(name); ;
            repo.Save(item3);

            repo.Delete(item1);

            var repo2 = CreateRepo();
            IList<TItem> result = repo2.GetAll();
            Assert.That(result, Has.Count.EqualTo(2));
        }
    }
}