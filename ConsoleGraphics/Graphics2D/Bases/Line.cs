namespace ConsoleGraphics.Graphics2D.Bases
{
    public class Line : Geometrical
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public char VerticalChar { get; set; }
        public char HorizontalChar { get; set; }
        public double Length => Math.Sqrt(Math.Pow(Point2.Y - Point1.Y, 2) + Math.Pow(Point2.X - Point1.X, 2));
        public Line(Point Point1, Point Point2, Plane2D Parent, ConsoleColor Color, char VerticalChar = '|', char HorizontalChar = '=') : base(Parent, Color)
        {
            this.Point1 = Point1;
            this.Point2 = Point2;
            this.Name = Point1.Name + Point2.Name;
            X = Point1.X;
            Y = Point1.Y;
            this.VerticalChar = VerticalChar;
            this.HorizontalChar = HorizontalChar;
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
        public override bool Equals(Geometrical? other) => (other is null || other is not Line) ? false : (this.Point1 == ((Line)other).Point1 && this.Point2 == ((Line)other).Point2) || (this.Point2 == ((Line)other).Point1 && this.Point1 == ((Line)other).Point2);
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
        public override object Clone() => new Line(Point1.Clone() as Point, Point2.Clone() as Point, Parent, Color, VerticalChar, HorizontalChar);
        public static Figure? operator +(Line? left, Point? right)
        {
            if (left == null || right == null) return null;
            return new Figure(new[] { left.Point1, left.Point2, right }, left.Parent, left.Color);
        }
        public static Figure? operator +(Line? left, Line? right)
        {
            if(left == null || right == null) return null;
            return new Figure(new[] { left.Point1, left.Point2, right.Point2, right.Point1 }, left.Parent, left.Color);
        }
        public override string ToString() => $"Line {Name}({X}, {Y}): {Point1.Name}({Point1.X}, {Point1.Y}) -> {Point2.Name}({Point2.X}, {Point2.Y})";
    }
}
