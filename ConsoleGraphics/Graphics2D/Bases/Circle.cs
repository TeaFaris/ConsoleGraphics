namespace ConsoleGraphics.Graphics2D.Bases
{
    public class Circle : GeometricalObject
    {
        /// <summary>
        /// Radius along the X-axis of this circle.
        /// </summary>
        public double RadiusX { get; set; }
        /// <summary>
        /// Radius along the Y-axis of this circle.
        /// </summary>
        public double RadiusY { get; set; }
        /// <summary>
        /// Creates an instance of a <see cref="Circle"/>.
        /// </summary>
        /// <param name="X">The <see cref="GeometricalObject.X">X0-Coordinate</see> that this <see cref="Circle"/> will have.</param>
        /// <param name="Y">The <see cref="GeometricalObject.Y">Y0-Coordinate</see> that this <see cref="Circle"/> will have.</param>
        /// <param name="Parent">The <see cref="Scene2D"/> where the figure will be drawn.</param>
        /// <param name="Color">The <see cref="ConsoleColor"/> with which the <see cref="Circle"/> will be drawn.</param>
        /// <param name="RadiusX">Radius along the <see cref="RadiusX">X</see> axis that this <see cref="Circle"/> will have.</param>
        /// <param name="RadiusY">Radius along the <see cref="RadiusY">Y</see> axis that this <see cref="Circle"/> will have.</param>
        /// <returns>A new instance of the <see cref="Point"/> class.</returns>
        public Circle(double X, double Y, double RadiusX, double RadiusY, ConsoleColor Color) : base(Color, X, Y)
        {
            this.X = X;
            this.Y = Y;
            this.RadiusX = RadiusX;
            this.RadiusY = RadiusY;
        }
        public override void Draw(Action<Point> DrawMethod)
        {
            if (Parent == null) throw new Exception("NoParentException: I don't know where to draw, add me to some Scene or use GeometricalObject.AddParent(Scene2D);.");
            int X0 = (int)this.X;
            int Y0 = (int)this.Y;
            int X = 0;
            int Y = (int)RadiusY;
            int ASqr = (int)(RadiusX * RadiusX);
            int BSqr = (int)(RadiusY * RadiusY);
            int D = 4 * BSqr * ((X + 1) * (X + 1)) + ASqr * ((2 * Y - 1) * (2 * Y - 1)) - 4 * ASqr * BSqr;
            while (ASqr * (2 * Y - 1) > 2 * BSqr * (X + 1))
            {
                DrawMethod(new Point(X + X0 + RadiusX, Y + Y0 + RadiusY, Color, "Connector"));
                DrawMethod(new Point(X + X0 + RadiusX, -Y + Y0 + RadiusY, Color, "Connector"));
                DrawMethod(new Point(-X + X0 + RadiusX, -Y + Y0 + RadiusY, Color, "Connector"));
                DrawMethod(new Point(-X + X0 + RadiusX, Y + Y0 + RadiusY, Color, "Connector"));
                if (D < 0)
                {
                    X++;
                    D += 4 * BSqr * (2 * X + 3);
                }
                else
                {
                    X++;
                    D = D - 8 * ASqr * (Y - 1) + 4 * BSqr * (2 * X + 3);
                    Y--;
                }
            }
            D = BSqr * ((2 * X + 1) * (2 * X + 1)) + 4 * ASqr * ((Y + 1) * (Y + 1)) - 4 * ASqr * BSqr;
            while (Y + 1 != 0)
            {
                DrawMethod(new Point(X + X0 + RadiusX, Y + Y0 + RadiusY, Color, "Connector"));
                DrawMethod(new Point(X + X0 + RadiusX, -Y + Y0 + RadiusY, Color, "Connector"));
                DrawMethod(new Point(-X + X0 + RadiusX, -Y + Y0 + RadiusY, Color, "Connector"));
                DrawMethod(new Point(-X + X0 + RadiusX, Y + Y0 + RadiusY, Color, "Connector"));
                if (D < 0)
                {
                    Y--;
                    D += 4 * ASqr * (2 * Y + 3);
                }
                else
                {
                    Y--;
                    D = D - 8 * BSqr * (X + 1) + 4 * ASqr * (2 * Y + 3);
                    X++;
                }
            }
        }
        public override bool Equals(GeometricalObject? other) => (other is null || other is not Circle) ? false : (other as Circle).RadiusX == RadiusX && (other as Circle).RadiusY == RadiusY;
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
    }
}
