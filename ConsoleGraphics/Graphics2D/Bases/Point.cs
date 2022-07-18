namespace ConsoleGraphics.Graphics2D.Bases
{
    public abstract class Geometrical : IEquatable<Geometrical>, ICloneable
    {
        public double Angle { get; protected set; }
        public double X { get; protected set; }
        public double Y { get; protected set; }
        public string Name { get; protected set; }
        public Plane2D Parent { get; protected set; }
        public ConsoleColor Color { get; set; }
        public abstract void Draw();
        public abstract bool Equals(Geometrical? other);
        public static bool operator ==(Geometrical? left, Geometrical? right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }
        public static bool operator !=(Geometrical? left, Geometrical? right) => !(left == right);
        public Geometrical(Plane2D Parent, ConsoleColor Color)
        {
            this.Color = Color;
            this.Parent = Parent;
        }
        public override bool Equals(object? obj) => (obj is Geometrical) ? Equals(obj as Geometrical) : false;
        public virtual object Clone() => MemberwiseClone();
        public abstract void SetX(double X);
        public abstract void SetY(double Y);
    }
    public class Point : Geometrical
    {
        public char DrawChar { get; set; } = '*';
        public int PixelInCosole => (int)Math.Round(X) + (int)Math.Round(Y);
        public Point(double X, double Y, Plane2D Parent, ConsoleColor Color, string Name, char DrawChar = '*') : base(Parent, Color)
        {
            this.Name = Name;
            this.X = X;
            this.Y = Y;
            this.Color = Color;
            this.DrawChar = DrawChar;
        } 
        public Point(int PositionInMap, Plane2D Parent, ConsoleColor Color, string Name, char DrawChar = '*') : this(PositionInMap / Console.WindowWidth - PositionInMap, PositionInMap % Console.WindowWidth, Parent, Color, Name, DrawChar) { }
        public override void Draw() => Parent.SetPoint(this);
        public override bool Equals(Geometrical? other) => (other is null || other is not Point) ? false : other.X == this.X && other.Y == this.Y;
        public override void SetX(double X)
        {
            this.X = X;
            Console.Clear();
        }
        public override void SetY(double Y)
        {
            this.Y = Y;
            Console.Clear();
        }
        public static Line? operator +(Point? left, Point? right)
        {
            if (left == null || right == null) return null;
            return new Line(left, right, left.Parent, left.Color);
        }
        public override string ToString() => $"Point {Name}({X}, {Y})";
    }
}
