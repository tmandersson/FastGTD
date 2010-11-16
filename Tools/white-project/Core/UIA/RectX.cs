using System.Windows;

namespace White.Core.UIA
{
    public static class RectX
    {
        public static bool IsZeroSize(this Rect rect)
        {
            return rect.Height == 0 && rect.Width == 0;
        }

        public static Point Center(this Rect rect)
        {
            return new Point((int) (rect.Left + (rect.Right - rect.Left)/2), (int) (rect.Top + (rect.Bottom - rect.Top)/2));
        }

        public static Point ImmediateExteriorEast(this Rect rectangle)
        {
            return new Point((int) (rectangle.Right + 1), rectangle.Center().Y);
        }

        public static Point ImmediateInteriorEast(this Rect rectangle)
        {
            return new Point((int) (rectangle.Right - 1), rectangle.Center().Y);
        }

        public static Point ImmediateExteriorWest(this Rect rectangle)
        {
            return new Point((int) (rectangle.Left - 1), rectangle.Center().Y);
        }

        public static Point ImmediateInteriorNorth(this Rect rectangle)
        {
            return new Point(rectangle.Center().X, rectangle.Top + 1);
        }

        public static Point ImmediateInteriorSouth(this Rect rectangle)
        {
            return new Point(rectangle.Center().X, rectangle.Bottom - 1);
        }

        public static bool IsContained(this Rect @this, Rect other, int tolerance)
        {
            other = new Rect(other.X - tolerance, 
                            other.Y - tolerance, 
                            other.Width + (tolerance * 2), 
                            other.Height + (tolerance * 2));
            return other.Contains(@this);
        }

        public static readonly Point UnlikelyWindowPosition = new Point(-10000, -10000);
    }
}