using System;
using System.Collections.Generic;
using Bricks.Objects;
using NUnit.Framework;

namespace Bricks.RuntimeFramework
{
    [TestFixture]
    public class ObjectComparisonTest
    {
        public ObjectComparisonTest()
        {
            ReflectedObject.IgnoreTypeForComparison(typeof (TestMarkable));
        }

        [Test]
        public void IdenticalObject()
        {
            TestObjectContainingPrimitives first = new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One);
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingPrimitives second = new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Lazy, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Lazy, second.ComparisonStatus.State);
        }

        [Test]
        public void DifferentObject()
        {
            TestObjectContainingPrimitives first = new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One);
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingPrimitives second = new TestObjectContainingPrimitives(11, ObjectComparisonTestEnum.One);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
        }

        [Test]
        public void Object_with_one_field_equal_other_field_different()
        {
            TestObjectContainingPrimitives first = new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One);
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingPrimitives second = new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.Two);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
        }

        [Test]
        public void NestedIdenticalObject()
        {
            TestObjectContainingNestedObject first =
                new TestObjectContainingNestedObject(new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One));
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingPrimitives secondNestedObject = new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One);
            TestObjectContainingNestedObject second = new TestObjectContainingNestedObject(secondNestedObject);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Lazy, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Lazy, second.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Lazy, secondNestedObject.ComparisonStatus.State);
        }

        [Test]
        public void NestedDifferentObject()
        {
            TestObjectContainingNestedObject first =
                new TestObjectContainingNestedObject(new TestObjectContainingPrimitives(11, ObjectComparisonTestEnum.One));
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingPrimitives secondNestedObject = new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One);
            TestObjectContainingNestedObject second = new TestObjectContainingNestedObject(secondNestedObject);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Eager, secondNestedObject.ComparisonStatus.State);
        }

        [Test]
        public void NestedObjectNull()
        {
            TestObjectContainingNestedObject first = new TestObjectContainingNestedObject(null);
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingNestedObject second = new TestObjectContainingNestedObject(null);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Lazy, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Lazy, second.ComparisonStatus.State);
        }

        [Test]
        public void First_nested_object_null()
        {
            TestObjectContainingNestedObject first = new TestObjectContainingNestedObject(null);
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingPrimitives secondNestedObject = new TestObjectContainingPrimitives(11, ObjectComparisonTestEnum.One);
            TestObjectContainingNestedObject second = new TestObjectContainingNestedObject(secondNestedObject);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
        }

        [Test]
        public void Second_nested_object_null()
        {
            TestObjectContainingNestedObject first =
                new TestObjectContainingNestedObject(new TestObjectContainingPrimitives(11, ObjectComparisonTestEnum.One));
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingNestedObject second = new TestObjectContainingNestedObject(null);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
        }

        [Test]
        public void EmptyCollectionOfMarkables()
        {
            TestObjectContainingCollection first = new TestObjectContainingCollection();
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingCollection second = new TestObjectContainingCollection();
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreNotEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreNotEqual(Tristate.Eager, second.ComparisonStatus.State);
        }

        [Test]
        public void FirstCollectionContainingNull()
        {
            TestObjectContainingCollection first = new TestObjectContainingCollection(new TestObjectContainingPrimitives[] {null});
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingPrimitives listedInSecond = new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.Two);
            TestObjectContainingCollection second = new TestObjectContainingCollection(listedInSecond);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Eager, listedInSecond.ComparisonStatus.State);
        }

        [Test]
        public void SecondCollectionContainingNull()
        {
            TestObjectContainingCollection first = new TestObjectContainingCollection(new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One));
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingCollection second = new TestObjectContainingCollection(new TestObjectContainingPrimitives[] {null});
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
        }

        [Test]
        public void ContainingIdenticalCollection()
        {
            TestObjectContainingCollection first = new TestObjectContainingCollection(new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One));
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingPrimitives listedInSecond = new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One);
            TestObjectContainingCollection second = new TestObjectContainingCollection(listedInSecond);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Lazy, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Lazy, second.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Lazy, listedInSecond.ComparisonStatus.State);
        }

        [Test]
        public void DifferentNumberOfItems()
        {
            TestObjectContainingCollection first =
                new TestObjectContainingCollection(new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One),
                                                   new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One));
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingPrimitives listedInSecond = new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One);
            TestObjectContainingCollection second = new TestObjectContainingCollection(listedInSecond);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
        }

        [Test]
        public void ContainingDifferentValueInListItem()
        {
            TestObjectContainingCollection first = new TestObjectContainingCollection(new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One));
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingPrimitives listedInSecond = new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.Two);
            TestObjectContainingCollection second = new TestObjectContainingCollection(listedInSecond);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Eager, listedInSecond.ComparisonStatus.State);
        }

        [Test]
        public void NestedIdenticalNonMarkableObject()
        {
            TestObjectContainingNonMarkable first = new TestObjectContainingNonMarkable(new TestNonMarkableObject(10));
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestNonMarkableObject secondNestedObject = new TestNonMarkableObject(10);
            TestObjectContainingNonMarkable second = new TestObjectContainingNonMarkable(secondNestedObject);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Lazy, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Lazy, second.ComparisonStatus.State);
        }

        [Test]
        public void NestedDifferentNonMarkableObject()
        {
            TestObjectContainingNonMarkable first = new TestObjectContainingNonMarkable(new TestNonMarkableObject(11));
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestNonMarkableObject secondNestedObject = new TestNonMarkableObject(10);
            TestObjectContainingNonMarkable second = new TestObjectContainingNonMarkable(secondNestedObject);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
        }

        [Test]
        public void NestedNonMarkableObjectNull()
        {
            TestObjectContainingNonMarkable first = new TestObjectContainingNonMarkable(null);
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingNonMarkable second = new TestObjectContainingNonMarkable(null);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Lazy, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Lazy, second.ComparisonStatus.State);
        }

        [Test]
        public void FirstNestedNonMarkableObjectNull()
        {
            TestObjectContainingNonMarkable first = new TestObjectContainingNonMarkable(null);
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestNonMarkableObject secondNestedObject = new TestNonMarkableObject(10);
            TestObjectContainingNonMarkable second = new TestObjectContainingNonMarkable(secondNestedObject);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
        }

        [Test]
        public void SecondNestedNonMarkableObjectNull()
        {
            TestObjectContainingNonMarkable first = new TestObjectContainingNonMarkable(new TestNonMarkableObject(10));
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingNonMarkable second = new TestObjectContainingNonMarkable(null);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
        }

        [Test]
        public void NestedObjectOfDifferentSubType1()
        {
            TestObjectContainingNestedObject first =
                new TestObjectContainingNestedObject(new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One));
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingPrimitives secondNestedObject = new ExtendedTestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One, 1000);
            TestObjectContainingNestedObject second = new TestObjectContainingNestedObject(secondNestedObject);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Eager, secondNestedObject.ComparisonStatus.State);
        }

        [Test]
        public void NestedObjectOfDifferentSubType2()
        {
            TestObjectContainingNestedObject first =
                new TestObjectContainingNestedObject(new ExtendedTestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One, 1000));
            ReflectedObject reflectedObject1 = new ReflectedObject(first);
            TestObjectContainingPrimitives secondNestedObject = new TestObjectContainingPrimitives(10, ObjectComparisonTestEnum.One);
            TestObjectContainingNestedObject second = new TestObjectContainingNestedObject(secondNestedObject);
            ReflectedObject reflectedObject2 = new ReflectedObject(second);
            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Eager, secondNestedObject.ComparisonStatus.State);
        }

        [Test]
        public void SelfReferencingObjectGraph()
        {
            TestObjectReferingToItself testObjectReferingToItself = new TestObjectReferingToItself(10);
            TestObjectReferingToItself first = new TestObjectReferingToItself(10, testObjectReferingToItself);
            testObjectReferingToItself.SetReference = first;
            ReflectedObject reflectedObject1 = new ReflectedObject(first);

            TestObjectReferingToItself testObjectReferingToItself2 = new TestObjectReferingToItself(10);
            TestObjectReferingToItself second = new TestObjectReferingToItself(10, testObjectReferingToItself2);
            testObjectReferingToItself2.SetReference = second;
            ReflectedObject reflectedObject2 = new ReflectedObject(second);

            Assert.AreEqual(Tristate.Lazy, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Lazy, second.ComparisonStatus.State);
        }

        [Test]
        public void Self_ReferencingObjectGraph_with_non_identical_objects()
        {
            TestObjectReferingToItself testObjectReferingToItself = new TestObjectReferingToItself(10);
            TestObjectReferingToItself first = new TestObjectReferingToItself(10, testObjectReferingToItself);
            testObjectReferingToItself.SetReference = first;
            ReflectedObject reflectedObject1 = new ReflectedObject(first);

            TestObjectReferingToItself testObjectReferingToItself2 = new TestObjectReferingToItself(100);
            TestObjectReferingToItself second = new TestObjectReferingToItself(10, testObjectReferingToItself2);
            testObjectReferingToItself2.SetReference = second;
            ReflectedObject reflectedObject2 = new ReflectedObject(second);

            Assert.AreEqual(Tristate.Eager, reflectedObject1.AreIdentical(reflectedObject2).State);
            Assert.AreEqual(Tristate.Eager, second.ComparisonStatus.State);
        }

        [Test]
        public void DirectCircle()
        {
            TestParentContainingChildWhichContainsIt mp1 = new TestParentContainingChildWhichContainsIt();
            TestChildContainingParentReference p1 = new TestChildContainingParentReference();
            mp1.TestChildContainingParentReference = p1;
            p1.TestParentContainingChildWhichContainsIt = mp1;

            TestParentContainingChildWhichContainsIt mp2 = new TestParentContainingChildWhichContainsIt();
            TestChildContainingParentReference p2 = new TestChildContainingParentReference();
            mp2.TestChildContainingParentReference = p2;
            p2.TestParentContainingChildWhichContainsIt = mp2;

            Assert.AreEqual(ComparisonStatus.Lazy, new ReflectedObject(mp1).AreIdentical(new ReflectedObject(mp2)));
            Assert.AreEqual(ComparisonStatus.Lazy, p2.ComparisonStatus);
            Assert.AreEqual(ComparisonStatus.Lazy, mp2.ComparisonStatus);

            mp2 = new TestParentContainingChildWhichContainsIt();
            p2 = new TestChildContainingParentReference();
            p2.TestParentContainingChildWhichContainsIt = mp2;
            mp2.TestChildContainingParentReference = p2;
            p1.Y = "hi";
            mp1.X = "hi";
            p2.Y = "hi";
            mp2.X = "hi";

            Assert.AreEqual(ComparisonStatus.Lazy, new ReflectedObject(mp1).AreIdentical(new ReflectedObject(mp2)));
            Assert.AreEqual(ComparisonStatus.Lazy, p2.ComparisonStatus);
            Assert.AreEqual(ComparisonStatus.Lazy, mp2.ComparisonStatus);
        }

        [Test]
        public void A_Has_B_Has_D_And_C_Which_Has_A()
        {
            AForTest a1 = new AForTest();
            BForTest b1 = new BForTest();
            CForTest c1 = new CForTest();
            DForTest d1 = new DForTest();
            a1.BForTest = b1;
            b1.CForTest = c1;
            b1.DForTest = d1;
            c1.AForTest = a1;

            AForTest a2 = new AForTest();
            BForTest b2 = new BForTest();
            CForTest c2 = new CForTest();
            DForTest d2 = new DForTest();
            a2.BForTest = b2;
            b2.CForTest = c2;
            b2.DForTest = d2;
            c2.AForTest = a2;

            Assert.AreEqual(ComparisonStatus.Lazy, new ReflectedObject(a1).AreIdentical(new ReflectedObject(a2)));
            Assert.AreEqual(Tristate.Lazy, a2.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Lazy, b2.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Lazy, c2.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Lazy, d2.ComparisonStatus.State);
        }

        [Test]
        public void W_Has_X_Has_Y_And_Collection_Of_Z_All_Of_Which_Have_W()
        {
            WForTest w1 = new WForTest();
            XForTest x1 = new XForTest();
            YForTest y1 = new YForTest();
            ZForTest z11 = new ZForTest();
            ZForTest z12 = new ZForTest();
            ZsForTest zeus1 = new ZsForTest();
            zeus1.Add(z11);
            zeus1.Add(z12);
            w1.XForTest = x1;
            x1.YForTest = y1;
            x1.Zeus = zeus1;
            z11.WForTest = w1;
            z12.WForTest = w1;
            w1.V = "foolish";

            WForTest w2 = new WForTest();
            XForTest x2 = new XForTest();
            YForTest y2 = new YForTest();
            ZForTest z21 = new ZForTest();
            ZForTest z22 = new ZForTest();
            ZsForTest zeus2 = new ZsForTest();
            zeus2.Add(z21);
            zeus2.Add(z22);
            w2.XForTest = x2;
            x2.YForTest = y2;
            x2.Zeus = zeus2;
            z21.WForTest = w2;
            z22.WForTest = w2;
            w2.V = "V";

            Assert.AreEqual(ComparisonStatus.Eager, new ReflectedObject(w1).AreIdentical(new ReflectedObject(w2)));
            Console.WriteLine(w2.ComparisonStatus + " " + w2.ComparisonStatus.ChildStatuses.Count);
            Console.WriteLine(x2.ComparisonStatus + " " + x2.ComparisonStatus.ChildStatuses.Count);
            Console.WriteLine(y2.ComparisonStatus + " " + y2.ComparisonStatus.ChildStatuses.Count);
            Console.WriteLine(z21.ComparisonStatus + " " + z21.ComparisonStatus.ChildStatuses.Count);
            Console.WriteLine(z22.ComparisonStatus + " " + z22.ComparisonStatus.ChildStatuses.Count);

            Assert.AreEqual(true, x2.ComparisonStatus.ChildStatuses.Contains(z21.ComparisonStatus));
            Assert.AreEqual(true, x2.ComparisonStatus.ChildStatuses.Contains(z22.ComparisonStatus));
            Assert.AreEqual(true, z22.ComparisonStatus.ChildStatuses.Contains(w2.ComparisonStatus));
            Assert.AreEqual(true, z21.ComparisonStatus.ChildStatuses.Contains(w2.ComparisonStatus));

            Assert.AreEqual(Tristate.Eager, w2.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Eager, x2.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Eager, z21.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Eager, z22.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Lazy, y2.ComparisonStatus.State);
        }

        [Test]
        public void P_Has_Q_Which_Has_2_Pees()
        {
            PForTest p1 = new PForTest();
            QForTest q1 = new QForTest();
            p1.QForTest = q1;
            q1.PeesForTest.Add(p1);
            q1.PeesForTest.Add(p1);

            q1.X = "123";

            PForTest p2 = new PForTest();
            QForTest q2 = new QForTest();
            p2.QForTest = q2;
            q2.PeesForTest.Add(p2);
            q2.PeesForTest.Add(p2);
            q2.X = "1234";

            ReflectedObject op1 = new ReflectedObject(p1);
            ReflectedObject op2 = new ReflectedObject(p2);

            Assert.AreEqual(ComparisonStatus.Eager, op1.AreIdentical(op2));
            Assert.AreEqual(Tristate.Eager, p2.ComparisonStatus.State);
            Assert.AreEqual(Tristate.Eager, q2.ComparisonStatus.State);
        }

        [Test]
        public void If_top_object_isLeaf_mark_children_as_DontKnow()
        {
            RenewalOptionForTest option1 = new RenewalOptionForTest(10);
            RenewalOptionForTest option2 = new RenewalOptionForTest(10);
            BranchForTest branch1 = new BranchForTest(option1);
            BranchForTest branch2 = new BranchForTest(option2);
            ReflectedObject op1 = new ReflectedObject(branch1, Registry);
            ReflectedObject op2 = new ReflectedObject(branch2, Registry);
            Assert.AreEqual(ComparisonStatus.Eager, op1.AreIdentical(op2));
            Assert.AreEqual(Tristate.DontKnow, branch2.ComparisonStatus.State);
            Assert.AreEqual(Tristate.DontKnow, option2.ComparisonStatus.State);
        }

        [Test]
        public void Should_not_mark_object_in_dictionary()
        {
            WForTest wForTest = new WForTest();
            wForTest.V = "abc";
            wForTest.ComparisonStatus = ComparisonStatus.Eager;

            ZForTest zForTest1 = new ZForTest();
            zForTest1.WForTest = wForTest;

            ZForTest zForTest2 = new ZForTest();
            zForTest2.WForTest = wForTest;

            LeafRegistry registry = Registry;
            BricksDictionaryForTest dictionary = new BricksDictionaryForTest();
            dictionary.Add(wForTest);
            registry.DoNotMarkObjectsIn(dictionary);
            
            new ObjectComparer().Compare(zForTest1, zForTest2, registry);
            Assert.AreEqual(ComparisonStatus.Eager, zForTest2.WForTest.ComparisonStatus);
        }

    	[Test]
    	public void Parent_of_ignored_dictionary_object_should_be_marked_eager_if_children_not_equal()
    	{
			WForTest wForTest1 = new WForTest("abc");
			WForTest wForTest2 = new WForTest("def");

			ZForTest zForTest1 = new ZForTest();
			zForTest1.WForTest = wForTest1;

			ZForTest zForTest2 = new ZForTest();
			zForTest2.WForTest = wForTest2;

    		new ObjectComparer().Compare(zForTest1, zForTest2, CreateDictionaryWithObjectsToBeLeftUnmarked(wForTest1, wForTest2));
			Assert.AreEqual(ComparisonStatus.Eager, zForTest2.ComparisonStatus);
		}

		[Test]
		public void Parent_of_ignored_dictionary_object_should_be_marked_lazy_if_children_equal()
		{
			WForTest wForTest = new WForTest("abc");

			ZForTest zForTest1 = new ZForTest();
			zForTest1.WForTest = wForTest;

			ZForTest zForTest2 = new ZForTest();
			zForTest2.WForTest = wForTest;

			new ObjectComparer().Compare(zForTest1, zForTest2, CreateDictionaryWithObjectsToBeLeftUnmarked(wForTest));
			Assert.AreEqual(ComparisonStatus.Lazy, zForTest2.ComparisonStatus);
		}

		private LeafRegistry CreateDictionaryWithObjectsToBeLeftUnmarked(params object[] ignoredObjects)
		{
    		LeafRegistry registry = Registry;
    		BricksDictionaryForTest dictionary = new BricksDictionaryForTest();
    		foreach (object ignoredObject in ignoredObjects)
    			dictionary.Add(ignoredObject);
    		registry.DoNotMarkObjectsIn(dictionary);
    		return registry;
    	}

    	private LeafRegistry Registry
        {
            get
            {
                LeafRegistry registry = new LeafRegistry();
                registry.AddLeaf(typeof (CachedObjectForTest));
                return registry;
            }
        }
    }

    internal interface CachedObjectForTest {}

    public class AdminEntityForTest : TestMarkable, CachedObjectForTest {}

    public class SimpleAdminEntityForTest : TestMarkable, CachedObjectForTest {}

    public class BricksDictionaryForTest : BricksDictionary {
        private Dictionary<object, object> dictionary = new Dictionary<object, object>();

        public BricksDictionaryForTest() {}

        public void Add(object value)
        {
            dictionary.Add(value, value);
        }

        public bool Exists(object value)
        {
            object result;
            return dictionary.TryGetValue(value, out result);
        }
    }

    public class BranchForTest : SimpleAdminEntityForTest
    {
        private RenewalOptionForTest optionForTest;

        public BranchForTest(RenewalOptionForTest optionForTest)
        {
            this.optionForTest = optionForTest;
        }

        public RenewalOptionForTest OptionForTest
        {
            get { return optionForTest; }
            set { optionForTest = value; }
        }
    }

    public class RenewalOptionForTest : AdminEntityForTest
    {
        private int i;

        public RenewalOptionForTest(int i)
        {
            this.i = i;
        }

        public int I
        {
            get { return i; }
            set { i = value; }
        }

        public bool Equals(RenewalOptionForTest renewalOptionForTest)
        {
            if (renewalOptionForTest == null) return false;
            return i == renewalOptionForTest.i;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as RenewalOptionForTest);
        }

        public override int GetHashCode()
        {
            return i;
        }
    }

    public class PForTest : TestMarkable
    {
        private QForTest qForTest;

        public QForTest QForTest
        {
            get { return qForTest; }
            set { qForTest = value; }
        }
    }

    public class PeesForTest : List<PForTest> {}

    public class QForTest : TestMarkable
    {
        private PeesForTest p = new PeesForTest();
        private string x;

        public string X
        {
            get { return x; }
            set { x = value; }
        }

        public PeesForTest PeesForTest
        {
            get { return p; }
            set { p = value; }
        }

        public bool Equals(QForTest qForTest)
        {
            if (qForTest == null) return false;
            return Equals(x, qForTest.x);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as QForTest);
        }

        public override int GetHashCode()
        {
            return x != null ? x.GetHashCode() : 0;
        }
    }

    public class WForTest : TestMarkable
    {
        private XForTest xForTest;
        private string v;

    	public WForTest() {}

    	public WForTest(string v)
    	{
    		this.v = v;
    	}

    	public string V
        {
            get { return v; }
            set { v = value; }
        }

        public XForTest XForTest
        {
            get { return xForTest; }
            set { xForTest = value; }
        }

        public bool Equals(WForTest wForTest)
        {
            if (wForTest == null) return false;
            return Equals(v, wForTest.v);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as WForTest);
        }

        public override int GetHashCode()
        {
            return v != null ? v.GetHashCode() : 0;
        }
    }

    public class XForTest : TestMarkable
    {
        private YForTest yForTest;
        private ZsForTest zeus;

        public ZsForTest Zeus
        {
            get { return zeus; }
            set { zeus = value; }
        }

        public YForTest YForTest
        {
            get { return yForTest; }
            set { yForTest = value; }
        }
    }

    public class YForTest : TestMarkable
    {
        private string time;

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public bool Equals(YForTest yForTest)
        {
            if (yForTest == null) return false;
            return Equals(time, yForTest.time);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as YForTest);
        }

        public override int GetHashCode()
        {
            return time != null ? time.GetHashCode() : 0;
        }
    }

    public class ZForTest : TestMarkable
    {
        private WForTest wForTest;

        public WForTest WForTest
        {
            get { return wForTest; }
            set { wForTest = value; }
        }
    }

    public class ZsForTest : List<ZForTest> {}

    public class AForTest : TestMarkable
    {
        private BForTest bForTest;

        public BForTest BForTest
        {
            get { return bForTest; }
            set { bForTest = value; }
        }

        public override bool Equals(object obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }

    public class BForTest : TestMarkable
    {
        private CForTest cForTest;
        private DForTest dForTest;

        public CForTest CForTest
        {
            get { return cForTest; }
            set { cForTest = value; }
        }

        public DForTest DForTest
        {
            get { return dForTest; }
            set { dForTest = value; }
        }

        public override bool Equals(object obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }

    public class CForTest : TestMarkable
    {
        private AForTest aForTest;

        public AForTest AForTest
        {
            get { return aForTest; }
            set { aForTest = value; }
        }

        public override bool Equals(object obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }

    public class DForTest : TestMarkable
    {
        private string x;

        public string X
        {
            get { return x; }
            set { x = value; }
        }

        public override bool Equals(object obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }

    [Serializable]
    public class TestMarkable : Markable
    {
        private readonly MetaDataForTest metaDataForTest = new MetaDataForTest();

        public bool IsProcessed
        {
            get { return metaDataForTest.IsProcessed; }
            set { metaDataForTest.IsProcessed = value; }
        }

        public ComparisonStatus ComparisonStatus
        {
            get { return metaDataForTest.ComparisonStatus; }
            set { metaDataForTest.ComparisonStatus = value; }
        }
    }

    [Serializable]
    public class MetaDataForTest
    {
        private bool processed;
        private ComparisonStatus comparisonStatus = ComparisonStatus.DontKnow;

        public virtual bool IsProcessed
        {
            get { return processed; }
            set { processed = value; }
        }

        public virtual ComparisonStatus ComparisonStatus
        {
            get { return comparisonStatus; }
            set { comparisonStatus = value; }
        }
    }

    public class TestParentContainingChildWhichContainsIt : TestMarkable
    {
        private TestChildContainingParentReference testChildContainingParentReference;
        private string x;

        public string X
        {
            get { return x; }
            set { x = value; }
        }

        public TestChildContainingParentReference TestChildContainingParentReference
        {
            get { return testChildContainingParentReference; }
            set { testChildContainingParentReference = value; }
        }

        public bool Equals(TestParentContainingChildWhichContainsIt testParentContainingChildWhichContainsIt)
        {
            if (testParentContainingChildWhichContainsIt == null) return false;
            return Equals(x, testParentContainingChildWhichContainsIt.x);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as TestParentContainingChildWhichContainsIt);
        }

        public override int GetHashCode()
        {
            return x != null ? x.GetHashCode() : 0;
        }
    }

    public class TestChildContainingParentReference : TestMarkable
    {
        private TestParentContainingChildWhichContainsIt testParentContainingChildWhichContainsIt;
        private string y;

        public string Y
        {
            get { return y; }
            set { y = value; }
        }

        public TestParentContainingChildWhichContainsIt TestParentContainingChildWhichContainsIt
        {
            get { return testParentContainingChildWhichContainsIt; }
            set { testParentContainingChildWhichContainsIt = value; }
        }

        public bool Equals(TestChildContainingParentReference testChildContainingParentReference)
        {
            if (testChildContainingParentReference == null) return false;
            return Equals(y, testChildContainingParentReference.y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as TestChildContainingParentReference);
        }

        public override int GetHashCode()
        {
            return y != null ? y.GetHashCode() : 0;
        }
    }

    public class TestObjectContainingPrimitives : TestMarkable
    {
        private readonly int x;
        private readonly ObjectComparisonTestEnum testEnum;

        public TestObjectContainingPrimitives(int x, ObjectComparisonTestEnum y)
        {
            this.x = x;
            testEnum = y;
        }

        public bool Equals(TestObjectContainingPrimitives testObjectContainingPrimitives)
        {
            if (testObjectContainingPrimitives == null) return false;
            return x == testObjectContainingPrimitives.x && Equals(testEnum, testObjectContainingPrimitives.testEnum);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as TestObjectContainingPrimitives);
        }

        public override int GetHashCode()
        {
            return x + 29*testEnum.GetHashCode();
        }
    }

    public class ExtendedTestObjectContainingPrimitives : TestObjectContainingPrimitives
    {
        private readonly int z;

        public ExtendedTestObjectContainingPrimitives(int x, ObjectComparisonTestEnum y, int z) : base(x, y)
        {
            this.z = z;
        }

        public bool Equals(ExtendedTestObjectContainingPrimitives extendedTestObjectContainingPrimitives)
        {
            if (extendedTestObjectContainingPrimitives == null) return false;
            if (!base.Equals(extendedTestObjectContainingPrimitives)) return false;
            return z == extendedTestObjectContainingPrimitives.z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ExtendedTestObjectContainingPrimitives);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + 29*z;
        }
    }

    public class TestObjectContainingNestedObject : TestMarkable
    {
        private TestObjectContainingPrimitives testObjectContainingPrimitives;

        public TestObjectContainingNestedObject(TestObjectContainingPrimitives testObjectContainingPrimitives)
        {
            this.testObjectContainingPrimitives = testObjectContainingPrimitives;
        }
    }

    public class TestObjectContainingCollection : TestMarkable
    {
        private List<TestObjectContainingPrimitives> collection = new List<TestObjectContainingPrimitives>();

        public TestObjectContainingCollection(params TestObjectContainingPrimitives[] testObjectContainingPrimitiveses)
        {
            collection.AddRange(testObjectContainingPrimitiveses);
        }
    }

    public class TestObjectContainingNonMarkable : TestMarkable
    {
        private TestNonMarkableObject nonMarkableObject;

        public TestObjectContainingNonMarkable(TestNonMarkableObject nonMarkableObject)
        {
            this.nonMarkableObject = nonMarkableObject;
        }
    }

    public class TestNonMarkableObject
    {
        private int x;

        public TestNonMarkableObject(int x)
        {
            this.x = x;
        }

        public bool Equals(TestNonMarkableObject testNonMarkableObject)
        {
            if (testNonMarkableObject == null) return false;
            return x == testNonMarkableObject.x;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as TestNonMarkableObject);
        }

        public override int GetHashCode()
        {
            return x;
        }
    }

    public enum ObjectComparisonTestEnum
    {
        One,
        Two
    }

    public class TestObjectReferingToItself : TestMarkable
    {
        private readonly int i;
        private TestObjectReferingToItself testObjectReferingToItself;

        public TestObjectReferingToItself(int i)
        {
            this.i = i;
        }

        public TestObjectReferingToItself(int i, TestObjectReferingToItself testObjectReferingToItself)
        {
            this.i = i;
            this.testObjectReferingToItself = testObjectReferingToItself;
        }

        public virtual TestObjectReferingToItself SetReference
        {
            set { testObjectReferingToItself = value; }
        }

        public bool Equals(TestObjectReferingToItself testObjectReferingToItself)
        {
            if (testObjectReferingToItself == null) return false;
            return i == testObjectReferingToItself.i;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as TestObjectReferingToItself);
        }

        public override int GetHashCode()
        {
            return i;
        }
    }
}
