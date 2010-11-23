using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Bricks.RuntimeFramework;
using NUnit.Framework;

namespace Bricks.RuntimeFramework
{
    [TestFixture]
    public class BricksCollectionTest
    {
        [Test]
        public void ItemwiseEquals()
        {
            Assert.AreEqual(new BricksCollection<string>("a", "b"), new BricksCollection<string>("a", "b"));
            Assert.AreNotEqual(new BricksCollection<string>("b", "a"), new BricksCollection<string>("a", "b"));
        }

        [Test]
        public void TestToString()
        {
            BricksCollection<Foo> foos = new BricksCollection<Foo>(new Foo(), new Foo());
            Assert.AreEqual("0,0", foos.ToString());
        }

        [Test]
        public void ShouldSortObjectsBasedOnDateProperty()
        {
            BricksCollection<Foo> bricksCollection = new BricksCollection<Foo>(new Foo(new DateTime(1954, 3, 14)), new Foo(DateTime.Today.AddMonths(-12)));
            List<Foo> sortedList = bricksCollection.SortedList(ListSortDirection.Ascending, "Date");
            Assert.IsTrue(sortedList[0].Date <= sortedList[1].Date);
        }

        [Test]
        public void ShouldGetDistinctObjectsBasedOnPropertyPassed()
        {
            MyString pandu = new MyString("pandu");
            MyString jhaadu = new MyString("jhaadu");
            MyString ekAurJhaadu = new MyString("jhaadu");
            BricksCollection<MyString> coll = new BricksCollection<MyString>(pandu, jhaadu, ekAurJhaadu);
            BricksCollection<MyString> distinctValue = coll.Distinct("Str");
            Assert.AreEqual(2, distinctValue.Count);
            Assert.IsTrue(distinctValue.Contains(pandu));
        }

        [Test]
        public void Filter()
        {
            Names names = new Names("ABC", "EFG", "XYZ", "XYZQQQ");
            Assert.AreEqual(4, names.AllNames.Count);
            Assert.AreEqual(3, names.ShorterThan4Chars.Count);
        }

        [Test]
        public void FilterByPropertyValue()
        {
            Names names = new Names("ABC", "EFG", "XYZ", "XYZQQQ");
            BricksCollection<string> filtered = names.Filter(delegate(string obj, int length) { return obj.Length == length; }, 6);
            Assert.AreEqual(1, filtered.Count);
        }

        [Test]
        public void Last()
        {
            BricksCollection<String> names = new BricksCollection<string>("ABC", "EFG", "XYZ", "XYZQQQ");
            Assert.AreEqual("XYZQQQ", names.Last);
        }

        [Test]
        public void Merge()
        {
            BricksCollection<Foo> food = new BricksCollection<Foo>(Bar(1), Bar(2));

            Foo modifiedFoo = Bar(2);
            BricksCollection<Foo> changedAgents = new BricksCollection<Foo>(Bar(3), modifiedFoo);

            food.Merge(changedAgents);

            Assert.AreEqual(3, food.Count);
            Assert.AreSame(modifiedFoo, food[2]);
            Assert.AreEqual(1, food[0].Id);
            Assert.AreEqual(3, food[1].Id);
            Assert.AreEqual(2, food[2].Id);
        }

        [Test]
        public void MergeNull()
        {
            BricksCollection<Foo> food = new BricksCollection<Foo>();
            food.Merge(null);
            Assert.AreEqual(0, food.Count);
        }

        private static Foo Bar(int id)
        {
            Foo foo = new Foo();
            foo.Id = id;
            return foo;
        }

        public class Foo
        {
            private readonly DateTime date;
            private int id;
            public Foo() { }

            public Foo(DateTime date)
            {
                this.date = date;
            }

            public virtual DateTime Date
            {
                get { return date; }
            }

            public virtual int Id
            {
                get { return id; }
                set { id = value; }
            }

            public override bool Equals(object obj)
            {
                Foo bar = (Foo)obj;
                return Id.Equals(bar.id);
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }

            public override string ToString()
            {
                return id.ToString();
            }
        }

        internal class Names : BricksCollection<string>
        {
            public Names(IEnumerable collection) : base(collection) { }
            public Names() { }

            public Names(params string[] collection) : base(collection) {}

            public virtual Names AllNames
            {
                get { return new Names(Filter(delegate { return true; })); }
            }

            public virtual Names ShorterThan4Chars
            {
                get { return new Names(Filter(delegate(string s) { return s.Length <= 3; })); }
            }
        }
    }

    public class MyString
    {
        private readonly string str;

        protected MyString() { }

        public MyString(string str)
        {
            this.str = str;
        }

        public virtual string Str
        {
            get { return str; }
        }

        public virtual int Length
        {
            get { return Str.Length; }
        }
    }
}