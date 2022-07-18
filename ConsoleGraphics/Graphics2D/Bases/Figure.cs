using System.Text;

namespace ConsoleGraphics.Graphics2D.Bases
{
    public class Figure : Geometrical
    {
        public char VerticalChar { get; set; }
        public char HorizontalChar { get; set; }
        public Point[] Points { get; set; }
        public Line[] Lines { get; set; }
        public double Perimeter => Lines.Select(x => x.Length).Sum();
        public Figure(IEnumerable<Point> Points, Plane2D Parent, ConsoleColor Color, char VerticalChar = '|', char HorizontalChar = '=') : base(Parent, Color)
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
            this.Lines = new Line[Points.ToArray().Length];
            for (int i = 0; i < Lines.Length; i++)
                Lines[i] = new Line(this.Points[i], this.Points[(i + 1 > this.Points.Length - 1) ? 0 : i + 1], Parent, Color, VerticalChar, HorizontalChar);
        }
        public static Figure? CreateSquare(double UpperLeftPointX, double UpperLeftPointY, double Width, double Height, Plane2D Parent, ConsoleColor Color = ConsoleColor.White, char PointsDrawChar = '*', char VerticalChar = '-', char HorizontalChar = '|')
        {
            if (Parent == null) return null;
            Point A = new Point(UpperLeftPointX, UpperLeftPointY, Parent, Color, "A", PointsDrawChar);
            Point B = new Point(UpperLeftPointX + Width, UpperLeftPointY, Parent, Color, "B", PointsDrawChar);
            Point C = new Point(UpperLeftPointX + Width, UpperLeftPointY + Height, Parent, Color, "C", PointsDrawChar);
            Point D = new Point(UpperLeftPointX, UpperLeftPointY + Height, Parent, Color, "D", PointsDrawChar);
            return A + B + C + D;
        }
        public override void Draw()
        {
            Points.ToList().ForEach(x => x.Draw());
            Lines.ToList().ForEach(x => x.Draw());
        }
        public override bool Equals(Geometrical? other) => (other is null || other is not Figure) ? false : Points.SequenceEqual(((Figure)other).Points);
        public override void SetX(double X)
        {
            this.X = X;
            for (int i = 0; i < Points.Length; i++)
                Points[i].SetX(Points[i].X + this.X);
            RecalculateLines();
            Console.Clear();
        }
        public override void SetY(double Y)
        {
            this.Y = Y;
            for (int i = 0; i < Points.Length; i++)
                Points[i].SetY(Points[i].Y + this.Y);
            RecalculateLines();
            Console.Clear();
        }
        public static Figure operator +(Figure? left, Point? right)
        {
            if (left == null)
                return null;
            return new Figure(new List<Point>(left.Points) { right }, left.Parent, left.Color, left.VerticalChar, left.HorizontalChar);
        }
        public override object Clone() => new Figure(this.Points.Select(x => x.Clone() as Point), Parent, Color, VerticalChar, HorizontalChar);
        public override string ToString()
        {
            StringBuilder SB = new StringBuilder($"Figure {Name}({X}, {Y}):\n");
            foreach (Line Line in Lines)
                SB.AppendLine(Line.ToString());
            return SB.ToString();
        }
    }
}
