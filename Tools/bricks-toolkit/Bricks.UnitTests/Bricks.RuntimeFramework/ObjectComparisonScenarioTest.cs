using System;
using NUnit.Framework;

namespace Bricks.RuntimeFramework
{
    [TestFixture]
    public class ObjectComparisonScenarioTest
    {
        [Test]
        public void Parent_object_refers_to_two_objects___the_objects_havent_changed_but_the_reference_on_parent_has_changed()
        {
            ParentObjectForTest parentObjectForTest = new ParentObjectForTest();
            parentObjectForTest.ChildObjectForTest1 = new ChildObjectForTest(1);
            parentObjectForTest.ChildObjectForTest2 = new ChildObjectForTest(2);
            
            ParentObjectForTest clonedParent = new ParentObjectForTest();
            parentObjectForTest.ChildObjectForTest1 = new ChildObjectForTest(1);
            clonedParent.ChildObjectForTest2 = clonedParent.ChildObjectForTest1;

            LeafRegistry registry = new LeafRegistry();
            registry.IgnoreLeaf(typeof(MetaDataForTest));

            new ObjectComparer().Compare(parentObjectForTest, clonedParent, registry);
            Assert.AreEqual(Tristate.Eager, clonedParent.ComparisonStatus.State);
        }
    }

    [Serializable]
    public class ParentObjectForTest : TestMarkable
    {
        private ChildObjectForTest childObjectForTest1;
        private ChildObjectForTest childObjectForTest2;

        public virtual ChildObjectForTest ChildObjectForTest1
        {
            get { return childObjectForTest1; }
            set { childObjectForTest1 = value; }
        }
        public ChildObjectForTest ChildObjectForTest2
        {
            get { return childObjectForTest2; }
            set { childObjectForTest2 = value; }
        }
    }

    [Serializable]
    public class ChildObjectForTest : TestMarkable
    {
        private int i;

        public ChildObjectForTest(int i)
        {
            this.i = i;
        }
    }
}