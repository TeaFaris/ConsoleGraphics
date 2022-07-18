namespace ConsoleGraphics.Graphics2D.Bases
{
    public class LineSegment : GeometricalObject
    {
        /// <summary>
        /// First line <see cref="Point"/>.
        /// </summary>
        public Point Point1 { get; set; }
        /// <summary>
        /// Second line <see cref="Point"/>.
        /// </summary>
        public Point Point2 { get; set; }
        /// <summary>
        /// <see cref="char"/> to <see cref="Draw">draw</see> vertical lines.
        /// </summary>
        public char VerticalChar { get; set; }
        /// <summary>
        /// <see cref="char"/> to <see cref="Draw">draw</see> horizontal lines.
        /// </summary>
        public char HorizontalChar { get; set; }
        /// <returns>Line segment length.</returns>
        public double Length => Math.Sqrt(Math.Pow(Point2.Y - Point1.Y, 2) + Math.Pow(Point2.X - Point1.X, 2));
        /// <summary>
        /// Creates an instance of a <see cref="LineSegment"/> for the subsequent construction of complex geometric shapes.
        /// </summary>
        /// <param name="Point1">First line <see cref="Point"/>.</param>
        /// <param name="Point2">Second line <see cref="Point"/>.</param>
        /// <param name="Parent">The <see cref="Plane2D"/> where the <see cref="LineSegment"/> will be drawn.</param>
        /// <param name="Color">The <see cref="ConsoleColor"/> with which the figure will be drawn.</param>
        /// <param name="VerticalChar">The vertical <see cref="char"/> that the <see cref="LineSegment"/> will be <see cref="Draw">drawn</see> with.</param>
        /// <param name="HorizontalChar">The horizontal <see cref="char"/> that the <see cref="LineSegment"/> will be <see cref="Draw">drawn</see> with.</param>
        /// <returns>A new instance of the <see cref="Point"/> class.</returns>
        public LineSegment(Point Point1, Point Point2, Plane2D Parent, ConsoleColor Color, char VerticalChar = '|', char HorizontalChar = '=') : base(Parent, Color)
        {
            this.Point1 = Point1;
            this.Point2 = Point2;
            this.Name = Point1.Name + Point2.Name;
            X = Point1.X;
            Y = Point1.Y;
            this.VerticalChar = VerticalChar;
            this.HorizontalChar = HorizontalChar;
            Parent.Add(this);
        }
        public override void Draw()
        {
            Point1.Draw();
            Point2.Draw();

            if (Point2.X < Point1.X)
                (Point1, Point2) = (Point2, Point1);

            int X1 = (int)Point1.X;
            int X2 = (int)Point2.X;

            int Y1 = (int)Point1.Y;
            int Y2 = (int)Point2.Y;

            int DX = (X2 > X1) ? X2 - X1 : X1 - X2;
            int DY = (Y2 > Y1) ? Y2 - Y1 : Y1 - Y2;

            int SX = (X2 >= X1) ? 1 : -1;
            int SY = (Y2 >= Y1) ? 1 : -1;

            int D, D1, D2, X, Y;
            if (DY < DX)
            {
                D = (DY << 1) - DX;
                D1 = DY << 1;
                D2 = (DY - DX) << 1;
                X = X1 + SX;
                Y = Y1;
                for (int i = 2; i <= DX; i++)
                {
                    if (D > 0)
                    {
                        D += D2;
                        Y += SY;
                    }
                    else
                        D += D1;
                    Parent.SetPoint(new Point(X, Y, Parent, Color, $"{this}(Connector)", HorizontalChar));
                    X++;
                }
            }
            else
            {
                D = (DX << 1) - DY;
                D1 = DX << 1;
                D2 = (DX - DY) << 1;
                X = X1;
                Y = Y1 + SY;
                for (int i = 2; i <= DY; i++)
                {
                    if (D > 0)
                    {
                        D += D2;
                        X += SX;
                    }
                    else
                        D += D1;
                    Parent.SetPoint(new Point(X, Y, Parent, Color, $"{this}(Connector)", VerticalChar));
                    if (Y2 < Y1)
                        Y--;
                    else
                        Y++;
                }
            }
        }
        public override bool Equals(GeometricalObject? other) => (other is null || other is not LineSegment) ? false : (this.Point1 == ((LineSegment)other).Point1 && this.Point2 == ((LineSegment)other).Point2) || (this.Point2 == ((LineSegment)other).Point1 && this.Point1 == ((LineSegment)other).Point2);
        public override void SetX(double X)
        {
            this.X = X;
            Point1.SetX(Point1.X + this.X);
            Point2.SetX(Point2.X + this.X);
            Console.Clear();
        }
        public override void SetY(double Y)
        {
            this.Y = Y;
            double Offset = Math.Abs(Point1.Y - Point2.Y);
            Point1.SetY(this.Y);
            Point2.SetY(this.Y + Offset);
            Console.Clear();
        }
        public override object Clone() => new LineSegment(Point1.Clone() as Point, Point2.Clone() as Point, Parent, Color, VerticalChar, HorizontalChar);
        /// <summary>
        /// Creates a <see cref="Shape"/> from a <see cref="Point"/> and a <see cref="LineSegment"/>.
        /// </summary>
        /// <param name="left"><see cref="LineSegment"/> on the plane</param>
        /// <param name="right"><see cref="Point"/> on the plane</param>
        /// <returns>Returns a new <see cref="Shape"/> instance from a <see cref="Point"/> and a <see cref="LineSegment"/>. And returns <see cref="null"/> if <paramref name="left"/> or <paramref name="right"/> <see cref="Equals(GeometricalObject?)">equals</see> <see cref="null"/></returns>
        public static Shape? operator +(LineSegment? left, Point? right)
        {
            if (left == null || right == null) return null;
            return new Shape(new[] { left.Point1, left.Point2, right }, left.Parent, left.Color);
        }
        /// <summary>
        /// Creates a <see cref="Shape"/> from a <see cref="LineSegment"/>s.
        /// </summary>
        /// <param name="left">First <see cref="LineSegment"/> on the plane</param>
        /// <param name="right">Second <see cref="LineSegment"/> on the plane</param>
        /// <returns>Returns a new <see cref="Shape"/> instance from a <see cref="LineSegment"/>s. And returns <see cref="null"/> if <paramref name="left"/> or <paramref name="right"/> <see cref="Equals(GeometricalObject?)">equals</see> <see cref="null"/></returns>
        public static Shape? operator +(LineSegment? left, LineSegment? right)
        {
            if(left == null || right == null) return null;
            return new Shape(new[] { left.Point1, left.Point2, right.Point2, right.Point1 }, left.Parent, left.Color);
        }
        public override string ToString() => $"Line {Name}({X}, {Y}): {Point1.Name}({Point1.X}, {Point1.Y}) -> {Point2.Name}({Point2.X}, {Point2.Y})";
    }
}
