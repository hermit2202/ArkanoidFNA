namespace Arkanoid.Logic.Models
{
    public struct RectangleF
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        public bool IntersectsWith(RectangleF other)
        {
            return Left < other.Right && Right > other.Left &&
                   Top < other.Bottom && Bottom > other.Top;
        }
    }
}
