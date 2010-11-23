using System;
using System.Collections.Generic;
using System.Reflection;
using Bricks.Objects;
using NUnit.Framework;

namespace Bricks.RuntimeFramework
{
    [TestFixture]
    public class ReflectedObjectTest
    {
        private TestObjectVisitor visitor;

        [SetUp]
        public void SetUp()
        {
            visitor = new TestObjectVisitor();
        }

        [Test]
        public void Visit_class_containing_no_fields()
        {
            ReflectedObject reflectedObject = new ReflectedObject(new ClassForObjectVisitorTest());
            reflectedObject.Visit(visitor);
            Assert.AreEqual(1, visitor.VisitedObjects.Count);
        }

        [Test]
        public void Visit_object_containing_a_primitive()
        {
            ReflectedObject reflectedObject = new ReflectedObject(new ClassContainingPrimitiveForObjectVisitorTest());
            reflectedObject.Visit(visitor);
            Assert.AreEqual(2, visitor.VisitedObjects.Count);
        }

        [Test]
        public void Visit_object_containing_another_object()
        {
            ReflectedObject reflectedObject = new ReflectedObject(new ClassContainingAnotherClassForObjectVisitorTest());
            reflectedObject.Visit(visitor);
            Assert.AreEqual(3, visitor.VisitedObjects.Count);
        }

        [Test]
        public void Visit_object_with_base_class()
        {
            ReflectedObject reflectedObject = new ReflectedObject(new ClassExtendingAnotherClassForObjectVisitorTest());
            reflectedObject.Visit(visitor);
            Assert.AreEqual(3, visitor.VisitedObjects.Count);
        }

        [Test]
        public void Ignore_Children_From_Defined_Leaves()
        {
            LeafRegistry registry = new LeafRegistry();
            registry.AddLeaf(typeof (Leaf));

            ReflectedObject reflectedObject = new ReflectedObject(new ClassContainingALeafClass(), registry);
            reflectedObject.Visit(visitor);
            Assert.AreEqual(2, visitor.VisitedObjects.Count);
        }

        [Test]
        public void Identical_object_containing_leaf()
        {
            ClassContainingALeafClass o1 = new ClassContainingALeafClass();
            ClassContainingALeafClass o2 = new ClassContainingALeafClass();

            Assert.AreEqual(true, ComparisonStatus.Lazy.Equals(new ReflectedObject(o1).AreIdentical(new ReflectedObject(o2))));
        }

        [Test]
        public void Not_Identical_When_Leaves_Are_Not_Equal()
        {
            ClassContainingALeafClass o1 = new ClassContainingALeafClass(3);
            ClassContainingALeafClass o2 = new ClassContainingALeafClass(4);
            Assert.AreEqual(false, ComparisonStatus.Lazy.Equals(new ReflectedObject(o1).AreIdentical(new ReflectedObject(o2))));
        }

        [Test]
        public void Fields()
        {
            ClassContainingALeafClass leafClass = new ClassContainingALeafClass();
            ReflectedObject reflectedObject = new ReflectedObject(leafClass);

            LeafClass leafNode = reflectedObject.Fields["leafnode"] as LeafClass;
            Assert.IsNotNull(leafNode, "Reflected Fields was Null");
        }

        [Test]
        public void Objects_of_Subclasses_Are_Not_Equal_To_Base()
        {
            TestBase testBase = new TestBase();
            TestChild testChild = new TestChild();

            Assert.AreEqual(ComparisonStatus.Eager, new ReflectedObject(testChild).AreIdentical(new ReflectedObject(testBase)));
        }

        [Test]
        public void Has_Field_Returns_True_For_A_Valid_Field()
        {
            ReflectedObject testBase = new ReflectedObject(new TestBase());
            Assert.IsTrue(testBase.HasField("yo"));
        }

        [Test]
        public void Has_Field_Returns_False_For_An_Invalid_Field()
        {
            ReflectedObject testBase = new ReflectedObject(new TestBase());
            Assert.IsFalse(testBase.HasField("bo"));
        }

        [Test]
        public void Each_Field()
        {
            TestBase obj = new TestBase();
            ReflectedObject testBase = new ReflectedObject(obj);
            testBase.EachField(delegate(FieldInfo fieldInfo) { fieldInfo.SetValue(obj, 5); });
            Assert.AreEqual(5, obj.Yo);
        }

        [Test]
        public void Items_In_Changed_Collection_ShouldBe_Iterated_Before_Checking_NumberOfItems()
        {
            LeafRegistry registry = new LeafRegistry();
            registry.IgnoreLeaf(typeof(MetaData));
            
            BricksCollection<Leaflet> collection = new BricksCollection<Leaflet>();
            Leaflet leaflet = new Leaflet(1,"Rajnikanth");
            Leaflet collectedLeaflet = new Leaflet(2, "Vijaykanth");
            collection.Add(collectedLeaflet);
            
            CollectionContainer referenceContainer = new CollectionContainer(collection,leaflet,1);
            CollectionContainer activeContainer = SerializationCloner.Clone(referenceContainer);

            activeContainer.Leaf.Name = "Palanikanth"; 

            activeContainer.Collection[0].Name = "Mahakanth";
            Leaflet newLeaf = new Leaflet(3,"Chamankanth");
            activeContainer.AddItem(newLeaf);

            
            ReflectedObject object1 = new ReflectedObject(referenceContainer,registry);
            ReflectedObject object2 = new ReflectedObject(activeContainer, registry);

            object1.AreIdentical(object2);

            Assert.AreEqual(ComparisonStatus.Eager, activeContainer.ComparisonStatus);
            Assert.AreEqual(ComparisonStatus.DontKnow, newLeaf.ComparisonStatus);
            Assert.AreEqual(ComparisonStatus.Eager, activeContainer.Leaf.ComparisonStatus);
            Assert.AreEqual(newLeaf, activeContainer.Collection[1]);
            Assert.AreEqual(ComparisonStatus.Eager, activeContainer.Collection[0].ComparisonStatus);
            Assert.AreEqual(ComparisonStatus.DontKnow, activeContainer.Collection[1].ComparisonStatus);
        }
    }

    public class TestBase
    {
        protected int yo;

        public int Yo
        {
            get { return yo; }
        }

        public void ThrowException()
        {
            throw new BricksException("foo");
        }
    }

    public class TestChild : TestBase {}

    public class TestObjectVisitor : ObjectVisitor
    {
        private readonly List<ReflectedObject> visitedObjects = new List<ReflectedObject>();

        public virtual void Accept(ReflectedObject reflectedObject)
        {
            visitedObjects.Add(reflectedObject);
        }

        public virtual List<ReflectedObject> VisitedObjects
        {
            get { return visitedObjects; }
        }
    }

    public class ClassForObjectVisitorTest {}

    public class ClassContainingPrimitiveForObjectVisitorTest
    {
        private int x = 10;
        public virtual int X
        {
            get { return x; }
            set { x = value; }
        }
    }

    public class ClassContainingAnotherClassForObjectVisitorTest
    {
        private ClassContainingPrimitiveForObjectVisitorTest o = new ClassContainingPrimitiveForObjectVisitorTest();
    }

    public class ClassExtendingAnotherClassForObjectVisitorTest : ClassContainingAnotherClassForObjectVisitorTest {}

    public class MarkableClassWithMarkableObject : TestMarkable
    {
        private TestObjectContainingPrimitives first = new TestObjectContainingPrimitives(2, ObjectComparisonTestEnum.Two);

        public TestObjectContainingPrimitives First
        {
            get { return first; }
            set { first = value; }
        }
    }

    public class ClassContainingALeafClass
    {
        private readonly LeafClass leafnode = new LeafClass();

        public ClassContainingALeafClass(int i)
        {
            leafnode.Clazz.X = i;
        }

        public ClassContainingALeafClass() {}
    }

    public class LeafClass : Leaf
    {
        private readonly ClassContainingPrimitiveForObjectVisitorTest clazz = new ClassContainingPrimitiveForObjectVisitorTest();

        public override bool Equals(object other)
        {
            LeafClass theOther = (LeafClass) other;
            return clazz.X == theOther.Clazz.X;
        }

        public override int GetHashCode()
        {
            return clazz.X.GetHashCode();
        }

        public virtual ClassContainingPrimitiveForObjectVisitorTest Clazz
        {
            get { return clazz; }
        }
    }

    [Serializable]
    public class CollectionContainer : Markable, IEquatable<CollectionContainer>
    {
        private BricksCollection<Leaflet> collection = new BricksCollection<Leaflet>();
        private Leaflet leaf;
        private MetaData metaData = new MetaData();
        private int id;
        public CollectionContainer(BricksCollection<Leaflet> collection, Leaflet leaf, int id)
        {
            this.collection = collection;
            this.id = id;
            this.leaf = leaf;
        }

        public Leaflet Leaf
        {
            get { return leaf; }
        }

        public void AddItem(Leaflet newLeaf)
        {
            collection.Add(newLeaf);
        }
        public bool IsProcessed {
            get { return metaData.IsProcessed; }
            set { metaData.IsProcessed = value; }
        }
        public ComparisonStatus ComparisonStatus {
            get { return metaData.ComparisonStatus; }
            set { metaData.ComparisonStatus = value; }
        }

        public bool Equals(CollectionContainer collectionContainer)
        {
            if (collectionContainer == null) return false;
            return id == collectionContainer.id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as CollectionContainer);
        }

        public override int GetHashCode()
        {
            return id;
        }

        public BricksCollection<Leaflet> Collection
        {
            get { return collection; }
        }
    }

    [Serializable]
    public class Leaflet : Markable
    {
        private int id;
        private string name;
        private readonly MetaData metaData = new MetaData();

        public Leaflet(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public bool IsProcessed
        {
            get { return metaData.IsProcessed; }
            set { metaData.IsProcessed = value; }
        }
        public ComparisonStatus ComparisonStatus
        {
            get { return metaData.ComparisonStatus; }
            set { metaData.ComparisonStatus = value; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            Leaflet leaflet = (Leaflet) obj;
            if (leaflet == null) return false;
            return id == leaflet.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
    [Serializable]
    public class MetaData
    {
        ComparisonStatus comparisonStatus = ComparisonStatus.DontKnow;
        private bool isProcessed;

        public ComparisonStatus ComparisonStatus
        {
            get { return comparisonStatus; }
            set { comparisonStatus = value; }
        }
        public bool IsProcessed
        {
            get { return isProcessed; }
            set { isProcessed = value; }
        }
    }
}