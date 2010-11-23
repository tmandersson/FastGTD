using System;
using System.Reflection;

namespace Bricks.Core
{
    public class BricksAttribute : Attribute
    {
        protected virtual object GetValue(string propertyName, object source)
        {
            return GetProperty(propertyName, source).GetValue(source, null);
        }

        protected virtual PropertyInfo GetProperty(string propertyName, object source)
        {
            Type domainType = source.GetType();
            return domainType.GetProperty(propertyName);
        }

        public virtual object GetValueOnField(object valueOnDomain, object value, PropertyInfo propertyInfo)
        {
            object valueOnField = value;
            if (valueOnDomain != null && typeof (CustomEnum).IsAssignableFrom(propertyInfo.PropertyType))
                valueOnField = CustomEnum.ValueOf(propertyInfo.PropertyType, value.ToString());
            return valueOnField;
        }
    }
}