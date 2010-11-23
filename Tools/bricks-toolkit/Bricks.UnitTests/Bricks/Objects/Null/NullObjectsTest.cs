using System.Collections.Generic;
using Lunar.Client.Common;
using Lunar.Shared.Common.DynamicProxy;
using NUnit.Framework;

namespace Bricks.Objects.Null
{
    [TestFixture]
    public class NullObjectsTest
    {
        private NullObjects nullObjects;

        [SetUp]
        public void SetUp()
        {
            nullObjects = new NullObjects();
        }

        [Test]
        public void CreateNullObject()
        {
            ClassForNullTest forNullTest = nullObjects.Get<ClassForNullTest>();
            Assert.AreEqual(true, forNullTest.IsNull);
            Assert.AreEqual(false, forNullTest.IsNotNull);
        }

        [Test]
        public void SetReturnValuesOnNullObject()
        {
            Dictionary<string, object> returnValues = new Dictionary<string, object>();
            string toStringReturnValue = "I am null";
            returnValues.Add("ToString", toStringReturnValue);
            ClassForNullTest forNullTest = nullObjects.Get<ClassForNullTest>(returnValues);

            Assert.AreEqual(toStringReturnValue, forNullTest.ToString());
        }
    }
}