namespace Bricks.VisualStudio2005
{
    public class ProjectName
    {
        private readonly string name;
        private const string TestLessString = "TestLess";

        public ProjectName(string name)
        {
            this.name = name;
        }

        public virtual string Name
        {
            get { return name; }
        }

        public virtual bool IsTestProject
        {
            get { return name.Replace(TestLessString, string.Empty).Contains("Test"); }
        }

        public virtual ProjectName TestLessName
        {
            get { return new ProjectName(name + TestLessString); }
        }

        public virtual string WithExtension
        {
            get { return string.Format("{0}.csproj", name); }
        }

        public virtual string TestLessFileWithExtension
        {
            get { return string.Format("{0}" + TestLessString + ".csproj", name); }
        }

        public virtual bool Equals(string s)
        {
            return name.Equals(s);
        }
    }
}