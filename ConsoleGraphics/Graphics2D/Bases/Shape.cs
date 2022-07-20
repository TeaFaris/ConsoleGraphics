using System.Text;

namespace ConsoleGraphics.Graphics2D.Bases
{
    public class Shape : GeometricalObject
    {
        /// <summary>
        /// <see cref="char"/> to <see cref="Draw">draw</see> vertical lines.
        /// </summary>
        public char VerticalChar { get; set; }
        /// <summary>
        /// <see cref="char"/> to <see cref="Draw">draw</see> horizontal lines.
        /// </summary>
        public char HorizontalChar { get; set; }
        /// <summary>
        /// The <see cref="Point"/>s that build this <see cref="Shape"/>.
        /// </summary>
        public Point[] Points { get; set; }
        /// <summary>
        /// The <see cref="LineSegment"/>s that build this <see cref="Shape"/>.
        /// </summary>
        public LineSegment[] Lines { get; set; }
        /// <summary>
        /// The perimeter of this <see cref="Shape"/>.
        /// </summary>
        public double Perimeter => Lines.Select(x => x.Length).Sum();
        /// <summary>
        /// Creates an instance of a <see cref="Shape"/>.
        /// </summary>
        /// <param name="Points">The <see cref="Point"/>s that build this <see cref="Shape"/>.</param>
        /// <param name="Parent">The <see cref="Scene2D"/> where the <see cref="Shape"/> will be drawn.</param>
        /// <param name="Color">The <see cref="ConsoleColor"/> with which the <see cref="Shape"/> will be drawn.</param>
        /// <param name="VerticalChar">The vertical <see cref="char"/> that the <see cref="Shape"/> will be <see cref="Draw">drawn</see> with.</param>
        /// <param name="HorizontalChar">The horizontal <see cref="char"/> that the <see cref="Shape"/> will be <see cref="Draw">drawn</see> with.</param>
        /// <returns>A new instance of the <see cref="Point"/> class.</returns>
        public Shape(IEnumerable<Point> Points, ConsoleColor Color, char VerticalChar = '|', char HorizontalChar = '=') : base(Color, 0, 0)
        {
            this.Points = Points.ToArray();
            X = this.Points[0].X;
            Y = this.Points[0].Y;
            this.VerticalChar = VerticalChar;
            this.HorizontalChar = HorizontalChar;
            this.Name = string.Concat(Points.Select(x => x.Name));
            RecalculateLines();
        }
        private void RecalculateLines()
        {
            this.Lines = new LineSegment[Points.ToArray().Length];
            for (int i = 0; i < Lines.Length; i++)
                Lines[i] = new LineSegment(this.Points[i], this.Points[(i + 1 > this.Points.Length - 1) ? 0 : i + 1], Color, VerticalChar, HorizontalChar);
        }
        private void RecalculatePoints()
        {
            Points = new Point[0];
            foreach (LineSegment Line in Lines)
                Points.Append(Line.Point1).Append(Line.Point2);
        }
        /// <summary>
        /// Creates a simple rectangle.
        /// </summary>
        /// <param name="UpperLeftPointX">The <see cref="GeometricalObject.X">X-Coordinate</see> that upper-left <see cref="Point"/> will have.</param>
        /// <param name="UpperLeftPointY">The <see cref="GeometricalObject.Y">Y-Coordinate</see> that upper-left <see cref="Point"/> will have.</param>
        /// <param name="Width">The width that the rectangle will have.</param>
        /// <param name="Height">The height that the rectangle will have.</param>
        /// <param name="Parent">The <see cref="Scene2D"/> where the <see cref="Shape"/> will be drawn.</param>
        /// <param name="Color">The <see cref="ConsoleColor"/> with which the <see cref="Shape"/> will be drawn.</param>
        /// <param name="VerticalChar">The vertical <see cref="char"/> that the <see cref="Point"/> will be <see cref="Draw">drawn</see> with.</param>
        /// <param name="HorizontalChar">The horizontal <see cref="char"/> that the <see cref="Point"/> will be <see cref="Draw">drawn</see> with.</param>
        /// <returns>A new instance of the <see cref="Point"/> class.</returns>
        public static Shape? CreateRectangle(double UpperLeftPointX, double UpperLeftPointY, double Width, double Height, ConsoleColor Color = ConsoleColor.White, char PointsDrawChar = '*', char VerticalChar = '-', char HorizontalChar = '|')
        {
            Point A = new Point(UpperLeftPointX, UpperLeftPointY, Color, "A", PointsDrawChar);
            Point B = new Point(UpperLeftPointX + Width, UpperLeftPointY, Color, "B", PointsDrawChar);
            Point C = new Point(UpperLeftPointX + Width, UpperLeftPointY + Height, Color, "C", PointsDrawChar);
            Point D = new Point(UpperLeftPointX, UpperLeftPointY + Height, Color, "D", PointsDrawChar);
            return A + B + C + D;
        }
        public override void Draw(Action<Point> DrawMethod)
        {
            if (Parent == null) throw new Exception("NoParentException: I don't know where to draw, add me to some Scene or use GeometricalObject.AddParent(Scene2D);.");
            Points.Where(x => x.Parent == null).ToList().ForEach(x => x.Parent = Parent);
            Lines.Where(x => x.Parent == null).ToList().ForEach(x => x.Parent = Parent);
            Points.ToList().ForEach(x => x.Draw(DrawMethod));
            Lines.ToList().ForEach(x => x.Draw(DrawMethod));
        }
        public override bool Equals(GeometricalObject? other) => (other is null || other is not Shape) ? false : Points.SequenceEqual(((Shape)other).Points);
        public override void SetX(double X)
        {
            base.SetX(X);
            foreach (LineSegment Line in Lines)
                Line.SetX(X);
            if (Parent != null)
                Draw(Parent.SetPoint);
            RecalculatePoints();
        }
        public override void SetY(double Y)
        {
            base.SetY(Y);
            foreach (LineSegment Line in Lines)
                Line.SetY(Y);
            if (Parent != null)
                Draw(Parent.SetPoint);
            RecalculatePoints();
        }
        /// <summary>
        /// Creates a <see cref="Shape"/> from a <see cref="Shape"/> and a <see cref="Point"/>.
        /// </summary>
        /// <param name="left"><see cref="Shape"/> on the plane</param>
        /// <param name="right"><see cref="Point"/> on the plane</param>
        /// <returns>Returns a new <see cref="Shape"/> instance from a <see cref="Shape"/> and a <see cref="Point"/>.</returns>
        public static Shape? operator +(Shape? left, Point? right)
        {
            if (left == null)
                return null;
            if (right == null)
                return left;
            return new Shape(new List<Point>(left.Points) { right }, left.Color, left.VerticalChar, left.HorizontalChar);
        }
        /// <summary>
        /// Creates a <see cref="Shape"/> from a <see cref="Shape"/> and a <see cref="LineSegment"/>.
        /// </summary>
        /// <param name="left"><see cref="Shape"/> on the plane</param>
        /// <param name="right"><see cref="LineSegment"/> on the plane</param>
        /// <returns>Returns a new <see cref="Shape"/> instance from a <see cref="Shape"/> and a <see cref="LineSegment"/>. And returns <see cref="null"/> if <paramref name="left"/> <see cref="Equals(GeometricalObject?)">equals</see> <see cref="null"/></returns>
        public static Shape? operator +(Shape? left, LineSegment? right)
        {
            if (left == null)
                return null;
            if (right == null)
                return left;
            return new Shape(new List<Point>(left.Points) { right.Point1, right.Point2 }, left.Color, left.VerticalChar, left.HorizontalChar);
        }
        public override object Clone() => new Shape(this.Points.Select(x => x.Clone() as Point), Color, VerticalChar, HorizontalChar);
        public override string ToString()
        {
            StringBuilder SB = new StringBuilder($"Shape {Name}({X}, {Y}):\n");
            foreach (LineSegment Line in Lines)
                SB.AppendLine(Line.ToString());
            return SB.ToString();
        }
    }
}
