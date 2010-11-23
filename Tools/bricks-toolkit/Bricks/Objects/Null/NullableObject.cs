namespace Bricks.Objects
{
    public interface NullableObject
    {
        bool IsNull { get; }
        bool IsNotNull { get; }
    }
}