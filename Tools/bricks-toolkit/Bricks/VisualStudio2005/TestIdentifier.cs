namespace Bricks.VisualStudio2005
{
    public interface TestIdentifier
    {
        bool IsATest(CSharpProject project, string fileName);
        bool IsTestProject(VisualStudioProject project);
    }
}