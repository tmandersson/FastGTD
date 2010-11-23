using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Bricks.Objects;

namespace Bricks.RuntimeFramework
{
    public class ReflectedObject
    {
        private readonly object o;
        private readonly LeafRegistry leafRegister;
        private readonly Class @class;
        private static Type ignoredType;
        private readonly Fields fields;

        public ReflectedObject(object o) : this(o, new LeafRegistry()) {}

        public ReflectedObject(object o, LeafRegistry registry)
        {
            this.o = o;
            leafRegister = registry;
            if (o != null)
            {
                @class = new Class(o.GetType());
                @class.IgnoreClass(ignoredType);
                fields = new Fields(this.o);
            }
        }

        public virtual List<object> FieldValues
        {
            get
            {
                if (o == null) return null;

                List<object> values = new List<object>();
                @class.EachField(delegate(FieldInfo fieldInfo) { values.Add(fieldInfo.GetValue(o)); });
                return values;
            }
        }

        public virtual object FieldValue(string fieldName)
        {
            FieldInfo field = @class.GetField(fieldName);
            if (field == null) throw new BricksException("Could not find field " + field.Name + " in typ");
            return field.GetValue(o);
        }

        public virtual bool HasField(string fieldName)
        {
            return @class.GetField(fieldName) != null;
        }

        public virtual Class Class
        {
            get { return @class; }
        }

        public virtual object Invoke(MethodInfo methodInfo, object[] arguments)
        {
            if (o == null) throw new NullReferenceException("Trying to invoke method on a null object");
            try
            {
                return methodInfo.Invoke(o, arguments);
            }
            catch (TargetInvocationException e)
            {
                throw new BricksException(string.Format("Error invoking {0}.{1}", o.GetType().Name, methodInfo.Name), e.InnerException);
            }
        }

        public virtual void Visit(ObjectVisitor visitor)
        {
            if (o == null || o.GetType().Equals(typeof (object))) return;
            Markable markable = o as Markable;
            if (markable != null && markable.IsProcessed)
                return;
            if (markable != null)
                markable.IsProcessed = true;
            @class.EachField(delegate(FieldInfo fieldInfo)
                                 {
                                     var reflectedObject = new ReflectedObject(fieldInfo.GetValue(o), leafRegister);
                                     if (TreatAsPrimitive(fieldInfo.FieldType) || leafRegister.IsIgnored(fieldInfo.FieldType))
                                         visitor.Accept(reflectedObject);
                                     else
                                         reflectedObject.Visit(visitor);
                                 });

            visitor.Accept(this);
        }

        public virtual ComparisonStatus AreIdentical(ReflectedObject other)
        {
            ComparisonStatus comparisonStatus = ComparisonStatus.DontKnow;

            if (o == null && other.o != null)
            {
                Mark(ComparisonStatus.Eager, other.o);
                return ComparisonStatus.Eager;
            }
            if (other.o == null && o != null)
                return ComparisonStatus.Eager;

            if (leafRegister.ShouldNotMark(other.o))
                return ComparisonStatus.Create(O.Equals(other.O));

            if (leafRegister.IsIgnored(o.GetType()))
            {
                if (other.o != null)
                    Mark(ComparisonStatus.Lazy, other.o);
                return ComparisonStatus.Lazy;
            }

            if (TreatAsPrimitive(o.GetType()))
            {
                bool equal = o.Equals(other.o);
                if (equal)
                {
                    Mark(ComparisonStatus.Lazy, other.o);
                    return ComparisonStatus.Lazy;
                }
                return ComparisonStatus.Eager;
            }

            if (!o.GetType().Equals(other.o.GetType()))
            {
                Mark(ComparisonStatus.Eager, other.o);
                return ComparisonStatus.Eager;
            }

            var markable = other.o as Markable;

            if (markable != null && markable.IsProcessed)
                return markable.ComparisonStatus;
            if (markable != null)
                markable.IsProcessed = true;

            List<object> otherFieldValues = other.FieldValues;
            List<object> myFieldValues = FieldValues;

            for (int i = 0; i < myFieldValues.Count; i++)
            {
                object myValue = myFieldValues[i];
                object otherValue = otherFieldValues[i];
                ComparisonStatus status = CompareValues(myValue, otherValue);
                if (Tristate.Lazy.Equals(status.State) && (otherValue is Markable && !Equals(myValue, otherValue)))
                    status.State = Tristate.Eager;
                comparisonStatus.Combine(status);
            }

            Mark(comparisonStatus, other.o);
            return comparisonStatus;
        }

        private bool TreatAsPrimitive(Type type)
        {
            return leafRegister.Contains(type) || type.IsPrimitive || type.IsEnum || type.Equals(typeof (string));
        }

        private ComparisonStatus CompareValues(object myValue, object otherValue)
        {
            if ((myValue != null && leafRegister.IsIgnored(myValue.GetType())) || (otherValue != null && leafRegister.IsIgnored(otherValue.GetType())))
            {
                if (otherValue != null)
                    Mark(ComparisonStatus.Lazy, otherValue);
                return ComparisonStatus.Lazy;
            }

            if (null == myValue)
                return ComparisonStatus.Create(otherValue == null);

            if (null == otherValue)
                return ComparisonStatus.Eager;

            else if (TreatAsPrimitive(myValue.GetType()))
                return ComparisonStatus.Create(myValue.Equals(otherValue));

            else if (typeof (ICollection).IsAssignableFrom(myValue.GetType()))
            {
                ICollection myCollection = (ICollection) myValue;
                ICollection otherCollection = (ICollection) otherValue;

                IEnumerator myEnumerator = myCollection.GetEnumerator();
                IEnumerator otherEnumerator = otherCollection.GetEnumerator();
                ComparisonStatus recurseStatus = ComparisonStatus.DontKnow;

                while (myEnumerator.MoveNext() && otherEnumerator.MoveNext())
                {
                    recurseStatus.Combine(Recurse(myEnumerator.Current, otherEnumerator.Current));
                }

                if (myCollection.Count != otherCollection.Count)
                    return ComparisonStatus.Eager;

                return recurseStatus;
            }
            else if (!myValue.GetType().Equals(otherValue.GetType()))
            {
                Mark(ComparisonStatus.Eager, otherValue);
                return ComparisonStatus.Eager;
            }
            else
                return Recurse(myValue, otherValue);
        }

        private ComparisonStatus Recurse(object mine, object others)
        {
            return new ReflectedObject(mine, leafRegister).AreIdentical(new ReflectedObject(others, leafRegister));
        }

        private static void Mark(ComparisonStatus comparisonStatus, object other)
        {
            var markable = other as Markable;
            if (markable == null) return;
            markable.ComparisonStatus.CopyFrom(comparisonStatus);
        }

        public static void IgnoreTypeForComparison(Type type)
        {
            ignoredType = type;
        }

        public virtual object O
        {
            get { return o; }
        }

        public virtual Fields Fields
        {
            get { return fields; }
        }

        public void EachField(Class.FieldDelegate operation)
        {
            @class.EachField(operation);
        }
    }

    public class Fields
    {
        private readonly object o;

        public Fields(object o)
        {
            this.o = o;
        }

        public object this[string fieldName]
        {
            get { return o.GetType().GetField(fieldName, BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(o); }
        }
    }
}