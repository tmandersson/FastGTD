namespace Bricks.Core
{
    public abstract class ValidationAttribute : BricksAttribute
    {
        public abstract Result Validate(object obj, string field, string labelText);
    }
}