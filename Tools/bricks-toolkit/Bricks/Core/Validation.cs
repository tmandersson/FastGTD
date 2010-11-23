using System;
using System.Collections.Generic;
using System.Text;

namespace Bricks.Core
{
    [Serializable]
    public class Validation : IValidation
    {
        private List<Error> errorList = new List<Error>();

        public virtual IList<Error> Errors
        {
            get { return errorList; }
            set { errorList = (List<Error>) value; }
        }

        public static Validation Fail
        {
            get { return new FailingValidation(); }
        }

        public virtual bool HasErrors()
        {
            return errorList.Count > 0;
        }

        public virtual bool HasNoErrors()
        {
            return !HasErrors();
        }

        [Obsolete("Enum keys should not be used for messages directly")]
        public virtual void AddError(Enum propertyKey)
        {
            errorList.Add(Error.Make(propertyKey));
        }

        public virtual void AddError(object propertyKey, string message)
        {
            errorList.Add(new Error(propertyKey, message));
        }

        public virtual void AddError(object property, string message, int index)
        {
            errorList.Add(new ListItemError(property, message, index));
        }

        public virtual void AddError(object property, string message, object obj)
        {
            if (1 != ErrorOn(obj).Count)
                errorList.Add(new LogicalListItemError(property, message, obj));
        }

        public virtual void AddErrorFrom(int index, IValidation validation)
        {
            if (validation.HasErrors())
            {
                foreach (Error error in validation.Errors)
                    errorList.Add(new ListItemError(error.Key, error.Message, index));
            }
        }

        public virtual List<Error> ErrorOn(int index)
        {
            List<Error> errors = new List<Error>();
            foreach (Error error in errorList)
            {
                ListItemError listItemError = error as ListItemError;
                if (listItemError != null && listItemError.Index == index) errors.Add(error);
            }
            return errors;
        }

        public virtual List<Error> ErrorOn(object obj)
        {
            List<Error> errors = new List<Error>();
            foreach (Error error in errorList)
            {
                LogicalListItemError listItemError = error as LogicalListItemError;
                if (listItemError != null && obj != null && obj.Equals(listItemError.Obj)) errors.Add(error);
            }
            return errors;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\n");
            foreach (Error error in errorList)
                builder.Append("Validation Error : " + error.Message + "\n");
            return builder.ToString();
        }

        public virtual void Add(IValidation validation)
        {
            errorList.AddRange(validation.Errors);
        }
    }

    public class FailingValidation : Validation
    {
        public override bool HasErrors()
        {
            return true;
        }
    }
}