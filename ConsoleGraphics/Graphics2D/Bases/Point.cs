namespace ConsoleGraphics.Graphics2D.Bases
{
    public abstract class GeometricalObject : IEquatable<GeometricalObject>, ICloneable
    {
        /// <summary>
        /// The angle of inclination of a <see cref="GeometricalObject"/>.
        /// </summary>
        public double Angle { get; protected set; }
        /// <summary>
        /// X coordinate of a <see cref="GeometricalObject"/>.
        /// </summary>
        public double X { get; protected set; }
        /// <summary>
        /// Y coordinate of a <see cref="GeometricalObject"/>.
        /// </summary>
        public double Y { get; protected set; }
        /// <summary>
        /// Used in the <see cref="Point.ToString"/> method of <see cref="GeometricalObject"/>.
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// The <see cref="Plane2D"/> where the figure will be drawn.
        /// </summary>
        public Plane2D Parent { get; protected set; }
        /// <summary>
        /// The <see cref="ConsoleColor"/> with which the figure will be drawn.
        /// </summary>
        public ConsoleColor Color { get; set; }
        /// <summary>
        /// Draws an <see cref="GeometricalObject"/> in the plane (console).
        /// </summary>
        public abstract void Draw();
        /// <summary>
        /// Compares 2 <see cref="GeometricalObject"/>s for inequality.
        /// </summary>
        /// <remarks>See also <seealso cref="operator ==(GeometricalObject?, GeometricalObject?)"/></remarks>
        /// <param name="other"><see cref="GeometricalObject"/> to compare</param>
        /// <returns>Returns <see cref="bool">true</see> if these objects are equal and <see cref="bool">false</see> if they are not equal.</returns>
        public abstract bool Equals(GeometricalObject? other);
        /// <summary>
        /// Compares 2 <see cref="GeometricalObject"/>s for inequality.
        /// </summary>
        /// <remarks>See also <seealso cref="operator !=(GeometricalObject?, GeometricalObject?)"/>.</remarks>
        /// <param name="left">First сompared <see cref="GeometricalObject"/>.</param>
        /// <param name="right">Second сompared <see cref="GeometricalObject"/>.</param>
        /// <returns>Returns <see cref="bool">true</see> if these <see cref="GeometricalObject"/>s are equal and <see cref="bool">false</see> if they are not equal.</returns>
        public static bool operator ==(GeometricalObject? left, GeometricalObject? right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }
        /// <summary>
        /// Compares 2 <see cref="GeometricalObject"/>s for inequality.
        /// </summary>
        /// <remarks>See also <seealso cref="operator ==(GeometricalObject?, GeometricalObject?)"/>.</remarks>
        /// <param name="left">First сompared <see cref="GeometricalObject"/>.</param>
        /// <param name="right">Second сompared <see cref="GeometricalObject"/>.</param>
        /// <returns>Returns <see cref="bool">false</see> if these <see cref="GeometricalObject"/>s are equal and <see cref="bool">true</see> if they are not equal.</returns>
        public static bool operator !=(GeometricalObject? left, GeometricalObject? right) => !(left == right);
        public GeometricalObject(Plane2D Parent, ConsoleColor Color)
        {
            this.Color = Color;
            this.Parent = Parent;
        }
        /// <summary>
        /// Compares 2 geometric objects for inequality.
        /// </summary>
        /// <remarks>See also <seealso cref="operator ==(GeometricalObject?, GeometricalObject?)"/></remarks>
        /// <param name="other">Object to compare</param>
        /// <returns>Returns <see cref="bool">true</see> if these objects are equal and <see cref="bool">false</see> if they are not equal.</returns>
        public override bool Equals(object? obj) => (obj is GeometricalObject) ? Equals(obj as GeometricalObject) : false;
        /// <summary>
        /// Creates an exact clone of an <see cref="GeometricalObject"/> for future work with it.
        /// </summary>
        /// <returns>Returns exactly the same <see cref="GeometricalObject"/> clone</returns>
        public virtual object Clone() => MemberwiseClone();
        /// <summary>
        /// Sets the passed <see cref="X">X</see> coordinate to the <see cref="GeometricalObject"/>.
        /// </summary>
        /// <remarks>See also <seealso cref="SetY(double)"/></remarks>
        /// <param name="X">X coordinate to be set.</param>
        public abstract void SetX(double X);
        /// <summary>
        /// Sets the passed <see cref="Y">Y</see> coordinate to the <see cref="GeometricalObject"/>.
        /// </summary>
        /// <remarks>See also <seealso cref="SetX(double)"/></remarks>
        /// <param name="Y">Y coordinate to be set.</param>
        public abstract void SetY(double Y);
    }
    public class Point : GeometricalObject
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
        /// <param name="X">The <see cref="GeometricalObject.X">X-Coordinate</see> that this <see cref="Point"/> will have.</param>
        /// <param name="Y">The <see cref="GeometricalObject.Y">Y-Coordinate</see> that this <see cref="Point"/> will have.</param>
        /// <param name="Parent">The <see cref="Plane2D"/> where the figure will be drawn.</param>
        /// <param name="Color">The <see cref="ConsoleColor"/> with which the <see cref="Point"/> will be drawn.</param>
        /// <param name="Name"><see cref="string"/> name of the point, can be anything, just for convenience. Used in the <see cref="ToString"/> method of <see cref="GeometricalObject"/></param>
        /// <param name="DrawChar">The <see cref="char"/> that the <see cref="Point"/> will be <see cref="Draw">drawn</see> with.</param>
        /// <returns>A new instance of the <see cref="Point"/> class.</returns>
        public Point(double X, double Y, Plane2D Parent, ConsoleColor Color, string Name = "Point", char DrawChar = '*') : base(Parent, Color)
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
        /// <param name="PositionInMap">Position in <see cref="List{Point}"/> of <see cref="Plane2D.Map1D"/>.</param>
        /// <param name="Parent">The <see cref="Plane2D"/> where the figure will be drawn.</param>
        /// <param name="Color">The <see cref="ConsoleColor"/> with which the figure will be drawn.</param>
        /// <param name="Name"><see cref="string"/> name of the point, can be anything, just for convenience. Used in the <see cref="ToString"/> method of <see cref="GeometricalObject"/></param>
        /// <param name="DrawChar">The <see cref="char"/> that the <see cref="Point"/> will be <see cref="Draw">drawn</see> with.</param>
        /// <returns>A new instance of the <see cref="Point"/> class.</returns>
        public Point(int PositionInMap, Plane2D Parent, ConsoleColor Color, string Name, char DrawChar = '*') : this(PositionInMap / Console.WindowWidth, PositionInMap % Console.WindowWidth, Parent, Color, Name, DrawChar) { }
        public override void Draw() => Parent.SetPoint(this);
        public override bool Equals(GeometricalObject? other) => (other is null || other is not Point) ? false : other.X == this.X && other.Y == this.Y;
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
        /// <summary>
        /// Creates a <see cref="LineSegment"/> from 2 <see cref="Point"/>s on the plane.
        /// </summary>
        /// <param name="left">First <see cref="Point"/> on the plane</param>
        /// <param name="right">Second <see cref="Point"/> on the plane</param>
        /// <returns>A new instance of the <see cref="LineSegment"/> from the passed <see cref="Point"/>s. And returns <see cref="null"/> if <paramref name="left"/> or <paramref name="right"/> <see cref="Equals(GeometricalObject?)">equals</see> <see cref="null"/></returns>
        public static LineSegment? operator +(Point? left, Point? right)
        {
            if (left == null || right == null) return null;
            return new LineSegment(left, right, left.Parent, left.Color);
        }
        public override string ToString() => $"Point {Name}({X}, {Y})";
    }
}
