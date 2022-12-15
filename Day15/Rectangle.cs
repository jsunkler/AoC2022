using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    record struct Rectangle(int X, int Y, int Width, int Height)
    {
        public int Left => X;
        public int Right => X + Width - 1;
        public int Top => Y;
        public int Bottom => Y + Height - 1;

        public IEnumerable<(int x, int y)> Corners
        {
            get
            {
                yield return (Left, Top);
                yield return (Left, Bottom);
                yield return (Right, Top);
                yield return (Right, Bottom);
            }
        }

        // Creates 4 smaller rectangles, might return empty ones with width or height == 0
        public IEnumerable<Rectangle> Split()
        {
            var w0 = Width / 2;
            var w1 = Width - w0;
            var h0 = Height / 2;
            var h1 = Height - h0;
            yield return new Rectangle(Left, Top, w0, h0);
            yield return new Rectangle(Left + w0, Top, w1, h0);
            yield return new Rectangle(Left, Top + h0, w0, h1);
            yield return new Rectangle(Left + w0, Top + h0, w1, h1);
        }
    }
}
