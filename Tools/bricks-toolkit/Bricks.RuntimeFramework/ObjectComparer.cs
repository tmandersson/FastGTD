namespace Bricks.RuntimeFramework
{
    public class ObjectComparer
    {
        public virtual bool Compare(object original, object changed, LeafRegistry registry)
        {
            var reflectedChanged = new ReflectedObject(changed, registry);
            var reflectedOriginal = new ReflectedObject(original, registry);
            if (ReferenceEquals(original, changed))
            {
                return false;
            }
            else
            {
                reflectedOriginal.AreIdentical(reflectedChanged);
                return true;
            }
        }
    }
}