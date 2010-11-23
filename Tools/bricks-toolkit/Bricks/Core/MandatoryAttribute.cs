using System;
using System.Reflection;

namespace Bricks.Core
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class MandatoryAttribute : ValidationAttribute
    {
        public override Result Validate(object source, string field, string labelText)
        {
            return new Result(IsValid(source, field), labelText + " is mandatory");
        }

        protected virtual bool IsValid(object source, string propertyName)
        {
            object value = GetValue(propertyName, source);
            return !
                   (value == null || S.IsEmpty(value.ToString()) || S.IsUnknown(value.ToString()) || (value is Boolean && false.Equals((Boolean) value)));
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class MandatoryIfEqualsAttribute : MandatoryAttribute
    {
        private readonly object[] values;
        private readonly string property;

        public MandatoryIfEqualsAttribute(string property, params object[] values)
        {
            this.property = property;
            this.values = values;
        }

        public override Result Validate(object source, string field, string labelText)
        {
            PropertyInfo propertyInfo = GetProperty(property, source);
            object valueOnDomain = propertyInfo.GetValue(source, null);

            foreach (object value in values)
            {
                object valueOnField = GetValueOnField(valueOnDomain, value, propertyInfo);
                if (valueOnDomain.Equals(valueOnField))
                    return new Result(IsValid(source, field), labelText + " is mandatory");
            }

            return Result.SUCCESS;
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class MandatoryIfNotEqualsAttribute : MandatoryAttribute
    {
        private readonly object[] values;
        private readonly string property;

        public MandatoryIfNotEqualsAttribute(string property, params object[] values)
        {
            this.property = property;
            this.values = values;
        }

        public override Result Validate(object source, string field, string labelText)
        {
            PropertyInfo propertyInfo = GetProperty(property, source);
            object valueOnDomain = propertyInfo.GetValue(source, null);
            bool result = false;
            foreach (object value in values)
            {
                object valueOnField = GetValueOnField(valueOnDomain, value, propertyInfo);
                if (valueOnField == null)
                    result = result | valueOnDomain == valueOnField;
                else
                    result = result | valueOnField.Equals(valueOnDomain);
            }
            if (result)
                return Result.SUCCESS;
            else
                return new Result(IsValid(source, field), labelText + " is mandatory");
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class NotEqualsAttribute : MandatoryAttribute
    {
        private readonly Type type;
        private Object objectToCheck;

        public NotEqualsAttribute(Type type, object objectToCheck)
        {
            this.type = type;
            this.objectToCheck = objectToCheck;
        }

        public override Result Validate(object source, string field, string labelText)
        {
            object val = GetValue(field, source);

            if (typeof (CustomEnum).IsAssignableFrom(type)) objectToCheck = CustomEnum.ValueOf(type, (string) objectToCheck);

            if (val.Equals(objectToCheck)) return new Result(false, string.Format("{0} cannot be {1}", labelText, objectToCheck));
            return Result.SUCCESS;
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class MaxSizeAttribute : ValidationAttribute
    {
        private int size;

        public MaxSizeAttribute(int size)
        {
            this.size = size;
        }

        public override Result Validate(object source, string field, string labelText)
        {
            object value = GetValue(field, source);
            string message = string.Format("Foo");
            bool valid = value == null ? true : size >= value.ToString().Length;
            return new Result(valid, message);
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ExactLengthAttribute : ValidationAttribute
    {
        private int size;

        public ExactLengthAttribute(int size)
        {
            this.size = size;
        }

        public override Result Validate(object source, string field, string labelText)
        {
            object value = GetValue(field, source);
            string message = string.Format(string.Format("{0} must be exactly {1} characters in size", labelText, size));
            bool valid = value == null ? false : size == value.ToString().Length;
            return new Result(valid, message);
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class DateCannontBeAfterTodayAttribute : ValidationAttribute
    {
        public override Result Validate(object source, string field, string labelText)
        {
            Date value = (Date) GetValue(field, source);
            if (value == null) return Result.SUCCESS;
            bool valid = value <= Date.Today;
            return new Result(valid, "{0} should be less than today.");
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class BetweenAttribute : ValidationAttribute
    {
        private int maxsize;
        private int minsize;

        public BetweenAttribute(int minsize, int maxSize)
        {
            this.minsize = minsize;
            maxsize = maxSize;
        }

        public override Result Validate(object source, string field, string labelText)
        {
            object value = GetValue(field, source);
            string message = string.Format("{0} should be between {1} and {2}", field, minsize, maxsize);
            return new Result(((int) value >= minsize && (int) value <= maxsize), message);
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class MinimumAttribute : ValidationAttribute
    {
        private int minSize;

        public MinimumAttribute(int minSize)
        {
            this.minSize = minSize;
        }

        public override Result Validate(object source, string field, string labelText)
        {
            object value = GetValue(field, source);
            string message = string.Format("{0} should be greater than or equal to {1}", field, minSize);
            return new Result(value != null && (int) value >= minSize, message);
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class MinimumDecimalAttribute : ValidationAttribute
    {
        private readonly double minSize;

        public MinimumDecimalAttribute(double minDecimal)
        {
            minSize = minDecimal;
        }

        public override Result Validate(object source, string field, string labelText)
        {
            object value = GetValue(field, source);
            string message = string.Format("{0} should be greater than or equal to {1}", field, minSize);
            return new Result(value != null && (double.Parse(value.ToString())) >= minSize, message);
        }
    }

    public class Result
    {
        public Result(bool valid, string message)
        {
            Valid = valid;
            Message = message;
        }

        public bool Valid;
        public string Message;

        public static Result SUCCESS
        {
            get { return new Result(true, ""); }
        }
    }
}