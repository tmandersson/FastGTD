namespace Bricks.RuntimeFramework
{
    public interface ObjectVisitor
    {
        void Accept(ReflectedObject reflectedObject);
    }
}