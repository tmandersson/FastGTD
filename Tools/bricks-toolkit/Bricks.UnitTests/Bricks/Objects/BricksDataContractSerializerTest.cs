using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Bricks.Objects;
using NUnit.Framework;

namespace Bricks.UnitTests.Bricks.Objects
{
    [TestFixture]
    public class BricksDataContractSerializerTest
    {
        private BricksDataContractSerializer dataContractSerializer;

        [SetUp]
        public void SetUp()
        {
            dataContractSerializer = new BricksDataContractSerializer();
        }

        [Test]
        public void TestToString()
        {
            string s = dataContractSerializer.ToString(new ForBricksDataContractSerializerTest(1));
            Assert.AreEqual(false, string.IsNullOrEmpty(s));

            var deserializedObject = dataContractSerializer.ToObject<ForBricksDataContractSerializerTest>(s, new List<Type>());
            Assert.AreNotEqual(null, deserializedObject);
            Assert.AreNotEqual(1, deserializedObject.X);
        }

        [Test]
        public void SerializeGenericContainerObject()
        {
            var objects = new object[] {new ForBricksDataContractSerializerTest(1)};
            var knownTypes = new List<Type> {typeof (ForBricksDataContractSerializerTest)};
            string s = dataContractSerializer.ToString(objects, knownTypes);
            Assert.AreEqual(false, string.IsNullOrEmpty(s));
            var deserializedObject = dataContractSerializer.ToObject<object[]>(s, knownTypes);
            Assert.AreNotEqual(null, deserializedObject);
        }

        [Test]
        public void SerializeGenericContainerObjectWithJustPrimitives()
        {
            var knownTypes = new List<Type> { typeof(ForBricksDataContractSerializerTest), typeof(object[]) };
            var objects = new List<object> { 1, new object[0], new ForBricksDataContractSerializerTest(1) };
            string s = dataContractSerializer.ToString(objects, knownTypes);
            Assert.AreEqual(false, string.IsNullOrEmpty(s));
            var deserializedObject = dataContractSerializer.ToObject<List<object>>(s, knownTypes);
            Assert.AreNotEqual(null, deserializedObject);
        }
    }

    [DataContract]
    public class ForBricksDataContractSerializerTest
    {
        private readonly int x;

        public ForBricksDataContractSerializerTest(int x)
        {
            this.x = x;
        }

        public int X
        {
            get { return x; }
        }
    }
}