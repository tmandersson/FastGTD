namespace Bricks.RuntimeFramework
{
    public class NullBricksDictionary : BricksDictionary
    {
        public bool Exists(object value)
        {
            return false;
        }
    }
}