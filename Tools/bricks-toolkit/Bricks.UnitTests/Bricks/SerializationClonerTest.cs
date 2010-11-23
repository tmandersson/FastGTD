using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Bricks.Core
{
    [TestFixture]
    public class SerializationClonerTest
    {
        [Test]
        public void ShouldClone()
        {
            Name name = new Name();
            name.First = "Gabbar";
            name.Last = "Singh";
            Name nameClone = SerializationCloner.Clone(name);
            Assert.AreNotSame(name, nameClone);
            Assert.AreEqual(name.First, nameClone.First);
            Assert.AreNotSame(name.First, nameClone.First);
            Assert.AreEqual(name.Last, nameClone.Last);
            Assert.AreNotSame(name.Last, nameClone.Last);
        }

        [Test]
        public void ShouldCloneEverything()
        {
            Person person = Person("Gabbar");
            Person personClone = SerializationCloner.Clone(person);
            Assert.AreNotSame(personClone, person);
            Assert.AreNotSame(personClone.Name, person.Name);
        }

        [Test]
        public void StringReferencesOnClone()
        {
            string foo = "foo";
            String bar = new StringBuilder(foo).ToString();
            Assert.AreNotSame(foo, bar);
            Assert.AreSame(foo, foo.Clone());
        }


        [Test]
        public void ShouldCloneCollections()
        {
            List<Person> list = new List<Person>();
            list.Add(Person("Gabbar"));
            ObjectWithList obj = new ObjectWithList(list);
            ObjectWithList clonedObj = SerializationCloner.Clone(obj);

            Assert.AreNotSame(obj, clonedObj);
            Assert.AreNotSame(obj.list, clonedObj.list);

            ArrayList originalList = new ArrayList(obj.list);
            ArrayList clonedList = new ArrayList(clonedObj.list);

            Assert.AreEqual(originalList[0], clonedList[0]);
            Assert.AreNotSame(originalList[0], clonedList[0]);

            ((Person) clonedList[0]).Name.First = "Foo";
            Assert.IsFalse(ReferenceEquals(((Person) clonedList[0]).Name.First, ((Person) originalList[0]).Name.First));
            Assert.AreNotEqual("Gabbar", ((Person) clonedList[0]).Name.First);
        }

        private static Person Person(string fname)
        {
            Person person = new Person();
            person.Name = new Name();
            person.Name.First = fname;
            person.Name.Last = "Singh";
            person.Address = "30/3";
            return person;
        }
    }

    [Serializable]
    public class ObjectWithList : ICloneable
    {
        public IList list;
        public ObjectWithList() {}

        public ObjectWithList(IList list)
        {
            this.list = list;
        }

        public virtual object Clone()
        {
            return SerializationCloner.Clone(this);
        }
    }

    [Serializable]
    public class Name
    {
        private string first;
        private string last;

        public virtual string First
        {
            get { return first; }
            set { first = value; }
        }

        public virtual string Last
        {
            get { return last; }
            set { last = value; }
        }
    }

    [Serializable]
    public class Person
    {
        private Name name;
        private string address;

        public virtual Name Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Address
        {
            get { return address; }
            set { address = value; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            Person person = obj as Person;
            if (person == null) return false;
            return Equals(address, person.address);
        }

        public override int GetHashCode()
        {
            return address.GetHashCode();
        }
    }
}