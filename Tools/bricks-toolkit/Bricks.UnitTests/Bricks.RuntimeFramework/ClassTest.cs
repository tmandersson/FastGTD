using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Bricks.RuntimeFramework;
using NUnit.Framework;

namespace Bricks.UnitTests.Bricks.RuntimeFramework
{
    [TestFixture]
    public class ClassTest
    {
        [Test]
        public void HasAttribute()
        {
            var @class = new Class(typeof(ClassForClassTest));
            Assert.AreEqual(true, @class.HasAttribute(typeof (SerializableAttribute)));
            Assert.AreEqual(false, @class.HasAttribute(typeof (TestFixtureAttribute)));
        }

        [Test]
        public void IsProperty()
        {
            var @class = new Class(typeof(ClassForClassTest));
            Assert.AreEqual(true, @class.IsProperty("StringProperty"));
            Assert.AreEqual(false, @class.IsProperty("ToString"));
        }

        [Test, ExpectedException(typeof (BricksException))]
        public void NewWhenTheParametersAreWrong()
        {
            var @class = new Class(typeof(ClassForClassTest));
            @class.New(0, 0);
        }

        [Test]
        public void New()
        {
            var @class = new Class(typeof(ClassForClassTest));
            Assert.AreNotEqual(null, @class.New());
        }

        [Test]
        public void NewWhenObjectTypeIsSubtype()
        {
            var @class = new Class(typeof(ClassForClassTest));
            @class.New(string.Empty);
        }

        [Test]
        public void NonVirtuals()
        {
            var @class = new Class(typeof(ClassForClassTest));
            Assert.AreEqual(2, @class.NonVirtuals.Count);
        }

        [Test]
        public void SubClasses()
        {
            var @class = new Class(typeof(ClassForClassTest));
            Assert.AreEqual(1, @class.SubClassesInAssembly().Count);
        }

        [Test]
        public void GetMethod()
        {
            var @class = new Class(typeof(ClassForClassTest));
            Assert.AreNotEqual(null, @class.GetMethod("SomeMethod"));
        }

        [Test]
        public void GetFieldGetsFieldsFromBaseClasses()
        {
            var @class = new Class(typeof(ClassForClassTest));
            Assert.AreNotEqual(null, @class.GetField("fieldInBaseClass"));
        }

        [Test]
        public void GetProperty()
        {
            var @class = new Class(typeof(ClassForClassTest));
            Assert.AreNotEqual(null, @class.GetProperty("SomeProperty"));
        }

        [Test]
        public void GetMethodFromTheBaseClassToo()
        {
            var @class = new Class(typeof(ClassForClassTest));
            Assert.AreNotEqual(null, @class.GetMethod("SomeBaseClassMethod"));
        }

        [Test]
        public void HasAttributes()
        {
            var @class = new Class(typeof(ClassForClassTest));
            bool b = @class.GetProperty("SomeProperty").HasAttribute(typeof(XmlIgnoreAttribute));
            Assert.AreEqual(true, b);
        }

        [Test]
        public void AllNonStandardTypes()
        {
            var @class = new Class(typeof(ClassContainingVarietyOfTypes));
            List<Type> types = @class.AllNonAutoSerializableTypes();
            Assert.AreEqual(5, types.Count);
            Assert.AreEqual(true, types.Contains(typeof(ClassContainingVarietyOfTypes)));
            Assert.AreEqual(true, types.Contains(typeof(NestedClassContainingVarietyOfTypes)));
            Assert.AreEqual(true, types.Contains(typeof(FileOptions)));
            Assert.AreEqual(true, types.Contains(typeof(BaseClassForClassTest)));
            Assert.AreEqual(true, types.Contains(typeof(InternalConstructorClass)));
        }

        [Test, ExpectedException(typeof(BricksException))]
        public void AllNonStandardTypesFromArray()
        {
            var @class = new Class(typeof(object[]));
            @class.AllNonAutoSerializableTypes();
        }
    }

    public class BaseClassContainingVarietyOfTypes
    {
        private InternalConstructorClass foo;
    }

    public class ClassContainingVarietyOfTypes : BaseClassContainingVarietyOfTypes
    {
        private int a;
        private string b;
        private NestedClassContainingVarietyOfTypes custom;
    }

    public class NestedClassContainingVarietyOfTypes
    {
        private FileOptions @enum;
        private BaseClassForClassTest custom;
    }

    public class BaseClassForClassTest
    {
        private int fieldInBaseClass;
        public virtual void SomeBaseClassMethod(){}

        public virtual int FieldInBaseClass
        {
            get { return fieldInBaseClass; }
            set { fieldInBaseClass = value; }
        }
    }

    public interface IClassForClassTest {
        void InterfaceMethodNonVirtualMethod();
    }

    public class SubClassForTest : ClassForClassTest{}

    public class InternalConstructorClass
    {
        internal InternalConstructorClass() {}
    }

    [Serializable]
    public class ClassForClassTest : BaseClassForClassTest, IClassForClassTest
    {
        private object s;

        public ClassForClassTest() { }

        public ClassForClassTest(object s)
        {
            this.s = s;
        }

        public void NonVirtualMethod()
        {
        }

        public void InterfaceMethodNonVirtualMethod() { }

        public virtual object StringProperty
        {
            get { return s; }
            set { s = value; }
        }

        public virtual void SomeMethod() { }

        [XmlIgnore]
        public virtual string SomeProperty { get { return string.Empty; } }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}