namespace Bricks.NAnt.Commands.FileSystem
{
    public class FileName
    {
        public static string ToString(string directory, string file)
        {
            return string.Format(@"{0}\{1}", directory, file);
        }
    }
}