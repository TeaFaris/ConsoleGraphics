namespace ConsoleGraphics.Graphics2D.Bases
{
    /// <summary>
    /// Abstraction of abstractions...
    /// </summary>
    /// /// <remarks>Beginning? Check out a short <seealso href="https://github.com/TeaFaris/ConsoleGraphics#usage">guide</seealso> on how to get started using the library.</remarks>
    public abstract class GeometricObject : IEquatable<GeometricObject>, ICloneable
    {
        /// <summary>
        /// The angle of inclination of a <see cref="GeometricObject"/>. Not working yet, see in later updates!
        /// </summary>
        public double Angle { get; protected set; }
        /// <summary>
        /// X coordinate of a <see cref="GeometricObject"/>.
        /// </summary>
        public double X { get; protected set; }
        protected double ActualX { get; set; }
        /// <summary>
        /// Y coordinate of a <see cref="GeometricObject"/>.
        /// </summary>
        public double Y { get; protected set; }
        protected double ActualY { get; set; }
        /// <summary>
        /// Used in the <see cref="Point.ToString"/> method of <see cref="GeometricObject"/>.
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// The <see cref="Scene2D"/> where the figure will be drawn.
        /// </summary>
        public Scene2D? Parent { get; set; }
        /// <summary>
        /// The <see cref="ConsoleColor"/> with which the figure will be drawn.
        /// </summary>
        public ConsoleColor Color { get; set; }
        /// <summary>
        /// Draws an <see cref="GeometricObject"/> in the plane (console).
        /// </summary>
        public abstract void Draw(Action<Point> DrawMethod);
        /// <summary>
        /// Compares 2 <see cref="GeometricObject"/>s for inequality.
        /// </summary>
        /// <remarks>See also <seealso cref="operator ==(GeometricObject?, GeometricObject?)"/></remarks>
        /// <param name="other"><see cref="GeometricObject"/> to compare</param>
        /// <returns>Returns <see cref="bool">true</see> if these objects are equal and <see cref="bool">false</see> if they are not equal.</returns>
        public abstract bool Equals(GeometricObject? other);
        /// <summary>
        /// Compares 2 <see cref="GeometricObject"/>s for inequality.
        /// </summary>
        /// <remarks>See also <seealso cref="operator !=(GeometricObject?, GeometricObject?)"/>.</remarks>
        /// <param name="left">First сompared <see cref="GeometricObject"/>.</param>
        /// <param name="right">Second сompared <see cref="GeometricObject"/>.</param>
        /// <returns>Returns <see cref="bool">true</see> if these <see cref="GeometricObject"/>s are equal and <see cref="bool">false</see> if they are not equal.</returns>
        public static bool operator ==(GeometricObject? left, GeometricObject? right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }
        /// <summary>
        /// Compares 2 <see cref="GeometricObject"/>s for inequality.
        /// </summary>
        /// <remarks>See also <seealso cref="operator ==(GeometricObject?, GeometricObject?)"/>.</remarks>
        /// <param name="left">First сompared <see cref="GeometricObject"/>.</param>
        /// <param name="right">Second сompared <see cref="GeometricObject"/>.</param>
        /// <returns>Returns <see cref="bool">false</see> if these <see cref="GeometricObject"/>s are equal and <see cref="bool">true</see> if they are not equal.</returns>
        public static bool operator !=(GeometricObject? left, GeometricObject? right) => !(left == right);
        public GeometricObject(ConsoleColor Color, double ActualX, double ActualY)
        {
            this.Color = Color;
            this.ActualX = ActualX;
            this.ActualY = ActualY;
        }
        /// <summary>
        /// Compares 2 geometric objects for inequality.
        /// </summary>
        /// <remarks>See also <seealso cref="operator ==(GeometricObject?, GeometricObject?)"/></remarks>
        /// <param name="other">Object to compare</param>
        /// <returns>Returns <see cref="bool">true</see> if these objects are equal and <see cref="bool">false</see> if they are not equal.</returns>
        public override bool Equals(object? obj) => (obj is GeometricObject) ? Equals(obj as GeometricObject) : false;
        /// <summary>
        /// Creates an exact clone of an <see cref="GeometricObject"/> for future work with it.
        /// </summary>
        /// <returns>Returns exactly the same <see cref="GeometricObject"/> clone</returns>
        public virtual object Clone() => MemberwiseClone();
        /// <summary>
        /// Sets the passed <see cref="X">X</see> coordinate to the <see cref="GeometricObject"/>.
        /// </summary>
        /// <remarks>See also <seealso cref="SetY(double)"/></remarks>
        /// <param name="X">X coordinate to be set.</param>
        public virtual void SetX(double X)
        {
            if (Parent != null)
                Draw(Parent.DeletePoint);
            this.X = ActualX + X;
        }
        /// <summary>
        /// Sets the passed <see cref="Y">Y</see> coordinate to the <see cref="GeometricObject"/>.
        /// </summary>
        /// <remarks>See also <seealso cref="SetX(double)"/></remarks>
        /// <param name="Y">Y coordinate to be set.</param>
        public virtual void SetY(double Y)
        {
            if (Parent != null)
                Draw(Parent.DeletePoint);
            this.Y = ActualY + Y;
        }
    }
    /// <summary>
    /// <see cref="Point"/> is one of the fundamental concepts for drawing graphics. Represents a single <see cref="char"/> in the console.
    /// </summary>
    /// /// <remarks>Beginning? Check out a short <seealso href="https://github.com/TeaFaris/ConsoleGraphics#usage">guide</seealso> on how to get started using the library.</remarks>
    public class Point : GeometricObject
    {
        /// <summary>
        /// The <see cref="char"/> that the <see cref="Point"/> will be <see cref="Draw">drawn</see> with.
        /// </summary>
        public char DrawChar { get; set; }
        /// <summary>
        /// The pixel in the <see cref="Console"/> that belongs to this <see cref="Point"/>.
        /// </summary>
        public int PixelInCosole => (int)Math.Round(X) + (int)Math.Round(Y);
        /// <summary>
        /// Creates an instance of a <see cref="Point"/> for the subsequent construction of complex geometric shapes.
        /// </summary>
        /// <param name="X">The <see cref="GeometricObject.X">X-Coordinate</see> that this <see cref="Point"/> will have.</param>
        /// <param name="Y">The <see cref="GeometricObject.Y">Y-Coordinate</see> that this <see cref="Point"/> will have.</param>
        /// <param name="Parent">The <see cref="Scene2D"/> where the figure will be drawn.</param>
        /// <param name="Color">The <see cref="ConsoleColor"/> with which the <see cref="Point"/> will be drawn.</param>
        /// <param name="Name"><see cref="string"/> name of the point, can be anything, just for convenience. Used in the <see cref="ToString"/> method of <see cref="GeometricObject"/></param>
        /// <param name="DrawChar">The <see cref="char"/> that the <see cref="Point"/> will be <see cref="Draw">drawn</see> with.</param>
        /// <returns>A new instance of the <see cref="Point"/> class.</returns>
        public Point(double X, double Y, ConsoleColor Color, string Name = "Point", char DrawChar = '*') : base(Color, X, Y)
        {
            this.Name = Name;
            this.X = X;
            this.Y = Y;
            this.Color = Color;
            this.DrawChar = DrawChar;
        }
        /// <summary>
        /// Creates an instance of a <see cref="Point"/> for the subsequent construction of complex geometric shapes.
        /// </summary>
        /// <param name="PositionInMap">Position in <see cref="List{Point}"/> of <see cref="Scene2D.Map1D"/>.</param>
        /// <param name="Parent">The <see cref="Scene2D"/> where the figure will be drawn.</param>
        /// <param name="Color">The <see cref="ConsoleColor"/> with which the figure will be drawn.</param>
        /// <param name="Name"><see cref="string"/> name of the point, can be anything, just for convenience. Used in the <see cref="ToString"/> method of <see cref="GeometricObject"/></param>
        /// <param name="DrawChar">The <see cref="char"/> that the <see cref="Point"/> will be <see cref="Draw">drawn</see> with.</param>
        /// <returns>A new instance of the <see cref="Point"/> class.</returns>
        public Point(int PositionInMap, ConsoleColor Color, string Name, char DrawChar = '*') : this(PositionInMap / Console.WindowWidth, PositionInMap % Console.WindowWidth, Color, Name, DrawChar) { }
        public override void Draw(Action<Point> DrawMethod)
        {
            if (Parent == null) throw new Exception("NoParentException: I don't know where to draw, add me to some Scene or use GeometricalObject.AddParent(Scene2D);.");
            DrawMethod(this);
        }
        public override bool Equals(GeometricObject? other) => (other is null || other is not Point) ? false : other.X == this.X && other.Y == this.Y;
        /// <summary>
        /// Creates a <see cref="LineSegment"/> from 2 <see cref="Point"/>s on the plane.
        /// </summary>
        /// <param name="left">First <see cref="Point"/> on the plane</param>
        /// <param name="right">Second <see cref="Point"/> on the plane</param>
        /// <returns>A new instance of the <see cref="LineSegment"/> from the passed <see cref="Point"/>s. And returns <see cref="null"/> if <paramref name="left"/> or <paramref name="right"/> <see cref="Equals(GeometricObject?)">equals</see> <see cref="null"/></returns>
        public static LineSegment? operator +(Point? left, Point? right)
        {
            if (left == null || right == null) return null;
            return new LineSegment(left, right, left.Color);
        }
        public override void SetX(double X)
        {
            base.SetX(X);
            if (Parent != null)
                Draw(Parent.SetPoint);
        }
        public override void SetY(double Y)
        {
            base.SetY(Y);
            if (Parent != null)
                Draw(Parent.SetPoint);
        }
        public static Point GetEmptyPoint() => new Point(0, 0, ConsoleColor.Black, "Empty", ' ');
        public static Point GetEmptyPoint(double X, double Y) => new Point(X, Y, ConsoleColor.Black, "Empty", ' ');
        public static Point GetEmptyPoint(int PositionInMap) => new Point(PositionInMap / Console.WindowWidth, PositionInMap % Console.WindowWidth, ConsoleColor.Black, "Empty", ' ');
        public override string ToString() => $"Point {Name}({X}, {Y})";
    }
}
