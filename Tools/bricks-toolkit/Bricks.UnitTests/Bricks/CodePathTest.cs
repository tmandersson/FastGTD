using System;
using NUnit.Framework;

namespace Bricks
{
    [TestFixture]
    public class CodePathTest
    {
        [SetUp]
        public void SetUp()
        {
            TraceStub.CleanUp();
        }

        [Test]
        public void Get()
        {
            Assert.AreEqual("ToString", CodePath.Get(CodePath.New<CodePathTestClass>().ToString()));
            Assert.AreEqual("StringProperty", CodePath.Get(CodePath.New<CodePathTestClass>().StringProperty));
            Assert.AreEqual("DateTimeQuestion", CodePath.Get(CodePath.New<CodePathTestClass>().DateTimeQuestion));
            Assert.AreEqual("IntProperty", CodePath.Get(CodePath.New<CodePathTestClass>().IntProperty));
            Assert.AreEqual("EnumProperty", CodePath.Get(CodePath.New<CodePathTestClass>().EnumProperty));
            Assert.AreEqual("AnotherClass.Name", CodePath.Get(CodePath.New<CodePathTestClass>().AnotherClass.Name));
            Assert.AreEqual("AnotherClass", CodePath.Get(CodePath.New<CodePathTestClass>().AnotherClass));
            Assert.AreEqual("YetanotherClass.AnotherClass.Name", CodePath.Get(CodePath.New<CodePathTestClass>().YetanotherClass.AnotherClass.Name));
            Assert.AreEqual("YetanotherClass.AnotherClass.Name", CodePath.Get(CodePath.New<CodePathTestClass>().YetanotherClass.AnotherClass.Name));
            Assert.AreEqual("DoubleProperty", CodePath.Get(CodePath.New<CodePathTestClass>().DoubleProperty));
            Assert.AreEqual("ObjectProperty", CodePath.Get(CodePath.New<CodePathTestClass>().ObjectProperty));
            CodePath.New<CodePathTest>().OwnMethod();
            Assert.AreEqual("OwnMethod", CodePath.VoidMember);
            Assert.AreEqual("DecimalProperty", CodePath.Get(CodePath.New<CodePathTestClass>().DecimalProperty));
            Assert.AreEqual("AbstractClass", CodePath.Get(CodePath.New<CodePathTestClass>().AbstractClass));
            Assert.AreEqual("Sum", CodePath.Get(CodePath.New<CodePathTestClass>().Sum(null, null)));
        }

        [Test]
        public void VirtualMethodCallInConstructor()
        {
            Assert.AreEqual("AnotherProperty", CodePath.Get(CodePath.New<CodePathTestClassContainingCallToVirtualMethod>().AnotherProperty));
        }

        [Test, ExpectedException(typeof (NullReferenceException))]
        public void ShouldThrowExceptionWhenTryToTraceAbstractClass()
        {
            CodePath.Get(CodePath.New<CodePathTestClass>().AbstractClass.SomeProperty);
        }

        [Test]
        public void Complete()
        {
            Assert.AreEqual("CodePathTestClass.StringProperty", CodePath.Complete(CodePath.New<CodePathTestClass>().StringProperty));
            Assert.AreEqual("CodePathTestClass.IntProperty", CodePath.Complete(CodePath.New<CodePathTestClass>().IntProperty));
            Assert.AreEqual("CodePathTestClass.EnumProperty", CodePath.Complete(CodePath.New<CodePathTestClass>().EnumProperty));
            Assert.AreEqual("CodePathTestClass.AnotherClass.Name", CodePath.Complete(CodePath.New<CodePathTestClass>().AnotherClass.Name));
            Assert.AreEqual("CodePathTestClass.AnotherClass", CodePath.Complete(CodePath.New<CodePathTestClass>().AnotherClass));
            Assert.AreEqual("CodePathTestClass.YetanotherClasses", CodePath.Complete(CodePath.New<CodePathTestClass>().YetanotherClasses));
            Assert.AreEqual("CodePathTestClass.YetanotherClass.AnotherClass.Name",
                            CodePath.Complete(CodePath.New<CodePathTestClass>().YetanotherClass.AnotherClass.Name));
        }

        [Test, ExpectedException(typeof(TraceException))]
        public void ThrowTraceExceptionWhenPathIsNotCalled()
        {
            Assert.IsNull(CodePath.New<CodePathTestClass>().StringProperty);
            Assert.IsNull(CodePath.New<CodePathTestClass>().StringProperty);
        }

        [Test, ExpectedException(typeof (TraceException)), Ignore("Don't know how to implement it")]
        public void ThrowTraceExceptionWhenMethodIsNotVirtual()
        {
            CodePath.Get(CodePath.New<CodePathTestClass>().NonVirtualProperty);
        }

        [Test, ExpectedException(typeof (TraceException))]
        [Ignore("Missing Feature- Vivek.")]
        public void ThrowTraceExceptionWhenChainedMethodIsNotVirtual()
        {
            CodePath.Get(CodePath.New<CodePathTestClass>().AnotherClass.NonVirtualProperty);
        }

        [Test]
        public void RecordASetterMethod()
        {
            CodePath.New<CodePathTestClass>().StringProperty = null;
            Assert.AreEqual("StringProperty", CodePath.Last);
        }

        [TestFixtureTearDown]
        public void Reset()
        {
            CodePath.Reset();
        }

        public virtual void OwnMethod() {}

        private class TraceStub : CodePath
        {
            internal static void CleanUp()
            {
                pathRetrieved = false;
            }
        }
    }

    public class CodePathTestClassContainingCallToVirtualMethod
    {
        public CodePathTestClassContainingCallToVirtualMethod()
        {
            string s = VirtualPropertyCalledInConstructor;
        }

        public virtual string VirtualPropertyCalledInConstructor
        {
            get { return string.Empty; }
        }

        public virtual string AnotherProperty
        {
            get { return string.Empty; }
        }
    }

    public class CodePathTestClass
    {
        private string stringProperty;
        private int intProperty;

        private decimal decimalProperty;
        private double doubleProperty;
        private AnotherClass anotherClass;
        private YetAnotherClass yetanotherClass;
        private YetAnotherClass[] yetanotherClasses;

        public virtual string StringProperty
        {
            get { return stringProperty; }
            set { throw new Exception("Making it hard for the code path"); }
        }

        public virtual string NonVirtualProperty
        {
            get { return stringProperty; }
            set { stringProperty = value; }
        }

        public virtual int IntProperty
        {
            get { return intProperty; }
            set { intProperty = value; }
        }

        public virtual TraceEnum EnumProperty
        {
            get { return TraceEnum.One; }
        }

        public virtual AnotherClass AnotherClass
        {
            get { return anotherClass; }
            set { anotherClass = value; }
        }

        public virtual YetAnotherClass YetanotherClass
        {
            get { return yetanotherClass; }
            set { yetanotherClass = value; }
        }

        public virtual DateTime? DateTimeQuestion
        {
            get { return DateTime.MinValue; }
        }

        public virtual double DoubleProperty
        {
            get { return doubleProperty; }
            set { doubleProperty = value; }
        }

        public virtual decimal DecimalProperty
        {
            get { return decimalProperty; }
            set { decimalProperty = value; }
        }

        public virtual bool NonVirtualBoolTypeProperty
        {
            get { return true; }
        }

        public virtual YetAnotherClass[] YetanotherClasses
        {
            get { return yetanotherClasses; }
            set { yetanotherClasses = value; }
        }

        public virtual object ObjectProperty
        {
            get { throw new NotImplementedException(); }
        }

        public virtual AbstractClass AbstractClass
        {
            get { return null; }
        }

        public virtual int Sum(int? a, int? b)
        {
            return 0;
        }

        // DONOT REMOVE THIS, needed for test
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public enum TraceEnum
    {
        One,
        Two
    }

    public abstract class AbstractClass
    {
        public abstract string SomeProperty { get; }
    }

    public class AnotherClass
    {
        private string name;

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string NonVirtualProperty
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class YetAnotherClass
    {
        private string name;
        private AnotherClass anotherClass;

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual AnotherClass AnotherClass
        {
            get { return anotherClass; }
            set { anotherClass = value; }
        }
    }
}