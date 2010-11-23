namespace Bricks.Core
{
    public interface UniqueEntity
    {
        void Validate(Validation validation);
        Error IsValidGiven(UniqueEntity otherEntity);
    }
}