using System.Collections.Generic;
using System.IO;
using Bricks.Core;

namespace Bricks.FileSystem
{
    public class TextFileContent
    {
        private readonly string contents;

        public TextFileContent(string contents)
        {
            this.contents = contents;
        }

        public virtual List<string> Lines
        {
            get
            {
                using (StringReader stringReader = new StringReader(contents))
                {
                    List<string> strings = new List<string>();
                    string line;
                    while ((line = stringReader.ReadLine()) != null)
                    {
                        if (S.IsNotEmpty(line)) strings.Add(line);
                    }
                    return strings;
                }
            }
        }
    }
}