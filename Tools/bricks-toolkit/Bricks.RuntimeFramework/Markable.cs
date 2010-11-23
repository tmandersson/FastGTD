using Bricks.RuntimeFramework;

namespace Bricks.Objects
{
    public interface Markable
    {
        bool IsProcessed { get; set; }
        ComparisonStatus ComparisonStatus { get; set; }
    }
}