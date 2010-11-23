namespace Bricks.RuntimeFramework
{
    public class Instance
    {
        private readonly object o;
        private Class @class;

        public Instance(object o)
        {
            this.o = o;
            @class = new Class(o.GetType());
        }

        public virtual object GetField(string fieldName)
        {
            return @class.GetField(fieldName).GetValue(o);
        }
    }
}