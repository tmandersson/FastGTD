using System;
using System.Collections.Generic;
using System.Reflection;
using Bricks.Core;
using Lunar.Shared;
using Lunar.Shared.Common;
using Lunar.Shared.Domain;
using NHibernate;
using NHibernate.Metadata;
using NHibernate.Type;

namespace Repository
{
    public class NhibernateChangeDetectingStrategy
    {
        public delegate void ChangeHandler(AbstractEntity e, string propertyName, string oldValue, string newValue);

        private readonly ISessionFactory factory;
        protected AbstractEntity newEntity;

        public NhibernateChangeDetectingStrategy(ISessionFactory factory)
        {
            this.factory = factory;
        }

        public void FindChanges(AbstractEntity newObject, AbstractEntity oldEntity, ChangeHandler changes)
        {
            newEntity = newObject;
            TraceChanges(newObject, oldEntity, changes, string.Empty);
        }

        protected virtual void TraceChanges(object newObject, object oldObject, ChangeHandler changes, string parentClassName)
        {
            if (newObject != null && oldObject != null)
            {
                TypeMetadata metadata = new TypeMetadata(factory.GetClassMetadata(E.Type(newObject.GetType())), newObject);
                TypeMetadata oldmetadata = new TypeMetadata(factory.GetClassMetadata(E.Type(oldObject.GetType())), oldObject);
                foreach (HibernateProperty property in metadata.Properties)
                {
                    if (!property.IgnoreAudit)
                        TraceChanges(property, oldmetadata.Property(property.Name), changes, parentClassName);
                }
            }
            else if (oldObject == null && newObject != null)
                changes(newEntity, parentClassName, String.Empty, newObject.ToString());
            else if (oldObject != null)
                changes(newEntity, parentClassName, oldObject.ToString(), String.Empty);
        }


        private void TraceChanges(HibernateProperty newProperty, HibernateProperty oldProperty, ChangeHandler changes, string parentClassName)
        {
            if (!newProperty.Type.IsCollection)
            {
                if (newProperty.Type.IsComponent)
                {
                    TraceChangesInComponent(changes, newProperty, oldProperty);
                    return;
                }

                if (newProperty.ShouldTraverse())
                {
                    TraceChanges(newProperty.Value, oldProperty.Value, changes, newProperty.ReturnClassName);
                    return;
                }

                MarkIfChanged(changes, newProperty, oldProperty, parentClassName);
            }
        }

        private void MarkIfChanged(ChangeHandler changes, HibernateProperty newProperty, HibernateProperty oldProperty, string parentClassName)
        {
            if (S.AreDifferent(newProperty.Value, oldProperty.Value) && !newProperty.IgnoreAudit)
            {
                string oldValue = S.Value(oldProperty.Value);
                string newValue = S.Value(newProperty.Value);

                if (oldValue.Length >= 2000 || newValue.Length >= 2000) return;
                changes(newEntity, parentClassName + " " + newProperty.Name, oldValue, newValue);
            }
        }

        private void TraceChangesInComponent(ChangeHandler changes, HibernateProperty newProperty, HibernateProperty oldProperty)
        {
            if (newProperty.Value != null)
            {
                List<HibernateProperty> properties = newProperty.ComponentProperties;
                List<HibernateProperty> oldProperties;

                if (oldProperty != null && oldProperty.Value != null)
                    oldProperties = oldProperty.ComponentProperties;
                else
                {
                    oldProperties = new List<HibernateProperty>();
                    for (int i = 0; i < properties.Count; i++)
                        oldProperties.Add(new NullHibernateProperty());
                }
                for (int i = 0; i < properties.Count; i++)
                    TraceChanges(properties[i], oldProperties[i], changes, newProperty.Name);
            }
        }
    }

    internal class NullHibernateProperty : HibernateProperty
    {
        public NullHibernateProperty() : base(null, null, null, null) {}
    }

    internal class TypeMetadata
    {
        private List<HibernateProperty> properties = new List<HibernateProperty>();

        public TypeMetadata(IClassMetadata metadata, object obj)
        {
            string[] prop = metadata.PropertyNames;
            IType[] types = metadata.PropertyTypes;
            for (int i = 0; i < prop.Length; i++)
            {
                if (!IgnoreAuditHelper.IgnoreAudit(new HibernateType(types[i]), obj, prop[i]))
                {
                    properties.Add(new HibernateProperty(prop[i], metadata.GetPropertyValue(obj, prop[i]), types[i], obj));    
                }
                
            }
        }

        public List<HibernateProperty> Properties
        {
            get { return properties; }
        }

        public bool HasPropertyByName(string property)
        {
            HibernateProperty p = Property(property);
            return p == null ? false : true;
        }

        public HibernateProperty Property(string name)
        {
            foreach (HibernateProperty prop in properties)
            {
                if (prop.Name.Equals(name))
                    return prop;
            }
            return null;
        }
    }

    public class HibernateProperty
    {
        public readonly string Name;
        private readonly object val;
        private readonly object owner;
        private readonly HibernateType type;

        public HibernateProperty(string name, object val, IType type, object owner)
        {
            Name = name;
            this.val = val;
            this.owner = owner;
            this.type = new HibernateType(type);
        }

        public virtual HibernateType Type
        {
            get { return type; }
        }

        public virtual object Value
        {
            get { return val; }
        }

        public List<HibernateProperty> ComponentProperties
        {
            get
            {
                List<HibernateProperty> list = new List<HibernateProperty>();
                ComponentType compType = (ComponentType) type.Type;
                object[] newValue = compType.GetPropertyValues(val);
                string[] properties = compType.PropertyNames;
                IType[] types = compType.Subtypes;
                for (int i = 0; i < properties.Length; i++)
                    list.Add(new HibernateProperty(properties[i], newValue[i], types[i], val));
                return list;
            }
        }

        public override string ToString()
        {
            return string.Format("Property name ={0}, type ={1}, value ={2}", Name, type, val);
        }

        public string ReturnClassName
        {
            get { return Type.Type.ReturnedClass.Name; }
        }

        public bool IgnoreAudit
        {
            get { return IgnoreAuditHelper.IgnoreAudit(type,owner,Name); }
        }
    }

    internal class IgnoreAuditHelper
    {
        
        public static bool IgnoreAudit(HibernateType type, Object owner, string name)
        {
            return type.Type is StringClobType || DontAudit(owner, name);
        }

        private static bool DontAudit(Object owner, string name)
        {
            PropertyInfo property = owner.GetType().GetProperty(name);
            if (property == null)
            {
                FieldInfo info = owner.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                return info != null && info.GetCustomAttributes(typeof(DontAuditAttribute), false).Length > 0;
            }

            return property.GetCustomAttributes(typeof(DontAuditAttribute), false).Length > 0;
        }

    }

    public class HibernateType
    {
        private readonly IType type;

        public HibernateType(IType type)
        {
            this.type = type;
        }

        public bool IsCollection
        {
            get { return type.GetType().Equals(typeof (SetType)) || type.GetType().Equals(typeof (BagType)); }
        }

        public bool IsComponent
        {
            get { return type.GetType().Equals(typeof (ComponentType)); }
        }

        public bool IsManyToOne
        {
            get { return type.GetType().Equals(typeof (ManyToOneType)); }
        }

        public IType Type
        {
            get { return type; }
        }

        public override string ToString()
        {
            return type.ToString();
        }
    }
}
