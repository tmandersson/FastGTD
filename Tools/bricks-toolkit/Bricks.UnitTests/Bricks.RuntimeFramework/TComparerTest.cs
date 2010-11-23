using System;
using System.Collections.Generic;
using System.ComponentModel;
using Bricks.Core;
using NUnit.Framework;

namespace Bricks.RuntimeFramework
{
    [TestFixture]
    public class TComparerTest
    {
        [Test]
        public void ShouldCompareOnMultipleProperties()
        {
            TComparer<TComparerLabRat> comparer =
                new TComparer<TComparerLabRat>(ListSortDirection.Ascending, "Name",
                                               "RollNo");
            Assert.AreEqual(-1, comparer.Compare(TComparerLab.Create("A", new DateTime(2006, 6, 21), 2), TComparerLab.Create("B", new DateTime(2006, 6, 21), 1)));

            Assert.AreEqual(1, comparer.Compare(TComparerLab.Create("A", new DateTime(2006, 6, 21), 2), TComparerLab.Create("A", new DateTime(2006, 6, 21), 1)));
        }

        [Test]
        public void CompareDates()
        {
            TComparer<TComparerLabRat> comparer =
                new TComparer<TComparerLabRat>(ListSortDirection.Ascending, "Dob");
            Assert.AreEqual(1, comparer.Compare(TComparerLab.Create("A", new DateTime(2006, 6, 21), 2), TComparerLab.Create("B", new DateTime(2006, 6, 20), 1)));

            Assert.AreEqual(0, comparer.Compare(TComparerLab.Create("A", new DateTime(2006, 6, 21), 2), TComparerLab.Create("A", new DateTime(2006, 6, 21), 1)));
        }

        [Test]
        public void ShouldCompareOn3Properties()
        {
            List<TComparerLabRat> rats = new List<TComparerLabRat>();
            rats.Add(TComparerLab.Create("A", new DateTime(2006, 6, 21), 3));
            rats.Add(TComparerLab.Create("A", new DateTime(2006, 6, 21), 1));
            rats.Add(TComparerLab.Create("A", new DateTime(2006, 6, 21), 2));
            rats.Sort(
                new TComparer<TComparerLabRat>(ListSortDirection.Ascending, "Name",
                                               "Dob", "RollNo"));
            Assert.AreEqual(1, rats[0].RollNo);
            Assert.AreEqual(2, rats[1].RollNo);
            Assert.AreEqual(3, rats[2].RollNo);
        }
    }

    public class TComparerLabRat
    {
        private string name;
        private DateTime dob;
        private int rollNo;

        public TComparerLabRat() {}

        public TComparerLabRat(string name, DateTime dob, int rollNo)
        {
            this.name = name;
            this.dob = dob;
            this.rollNo = rollNo;
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual DateTime Dob
        {
            get { return dob; }
            set { dob = value; }
        }

        public virtual int RollNo
        {
            get { return rollNo; }
            set { rollNo = value; }
        }
    }

    public class TComparerLab
    {
        public static TComparerLabRat Create(string name, DateTime date, int rollNo)
        {
            return new TComparerLabRat(name, date, rollNo);
        }
    }
}