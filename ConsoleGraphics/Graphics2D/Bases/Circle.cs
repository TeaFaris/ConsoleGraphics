﻿namespace ConsoleGraphics.Graphics2D.Bases
{
    public class Circle : GeometricalObject
    {
        private double A { get; set; }
        /// <summary>
        /// Radius along the X-axis of this circle.
        /// </summary>
        public double RadiusX
        {
            get => A;
            set
            {
                A = value;
                Console.Clear();
            }
        }
        private double B { get; set; }
        /// <summary>
        /// Radius along the Y-axis of this circle.
        /// </summary>
        public double RadiusY
        {
            get => B;
            set
            {
                B = value;
                Console.Clear();
            }
        }
        /// <summary>
        /// Creates an instance of a <see cref="Circle"/>.
        /// </summary>
        /// <param name="X">The <see cref="GeometricalObject.X">X0-Coordinate</see> that this <see cref="Circle"/> will have.</param>
        /// <param name="Y">The <see cref="GeometricalObject.Y">Y0-Coordinate</see> that this <see cref="Circle"/> will have.</param>
        /// <param name="Parent">The <see cref="Plane2D"/> where the figure will be drawn.</param>
        /// <param name="Color">The <see cref="ConsoleColor"/> with which the <see cref="Circle"/> will be drawn.</param>
        /// <param name="RadiusX">Radius along the <see cref="RadiusX">X</see> axis that this <see cref="Circle"/> will have.</param>
        /// <param name="RadiusY">Radius along the <see cref="RadiusY">Y</see> axis that this <see cref="Circle"/> will have.</param>
        /// <returns>A new instance of the <see cref="Point"/> class.</returns>
        public Circle(double X, double Y, double RadiusX, double RadiusY, Plane2D Parent, ConsoleColor Color) : base(Parent, Color)
        {
            this.X = X;
            this.Y = Y;
            this.RadiusX = RadiusX;
            this.RadiusY = RadiusY;
            Parent.Add(this);
        }
        public override void Draw()
        {
            int X0 = (int)this.X;
            int Y0 = (int)this.Y;
            int X = 0;
            int Y = (int)RadiusY;
            int ASqr = (int)(RadiusX * RadiusX);
            int BSqr = (int)(RadiusY * RadiusY);
            int D = 4 * BSqr * ((X + 1) * (X + 1)) + ASqr * ((2 * Y - 1) * (2 * Y - 1)) - 4 * ASqr * BSqr;
            while (ASqr * (2 * Y - 1) > 2 * BSqr * (X + 1))
            {
                Parent.SetPoint(new Point(X + X0 + RadiusX, Y + Y0 + RadiusY, Parent, Color, "Connector"));
                Parent.SetPoint(new Point(X + X0 + RadiusX, -Y + Y0 + RadiusY, Parent, Color, "Connector"));
                Parent.SetPoint(new Point(-X + X0 + RadiusX, -Y + Y0 + RadiusY, Parent, Color, "Connector"));
                Parent.SetPoint(new Point(-X + X0 + RadiusX, Y + Y0 + RadiusY, Parent, Color, "Connector"));
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
                Parent.SetPoint(new Point(X + X0 + RadiusX, Y + Y0 + RadiusY, Parent, Color, "Connector"));
                Parent.SetPoint(new Point(X + X0 + RadiusX, -Y + Y0 + RadiusY, Parent, Color, "Connector"));
                Parent.SetPoint(new Point(-X + X0 + RadiusX, -Y + Y0 + RadiusY, Parent, Color, "Connector"));
                Parent.SetPoint(new Point(-X + X0 + RadiusX, Y + Y0 + RadiusY, Parent, Color, "Connector"));
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
            this.X = X;
            Console.Clear();
        }
        public override void SetY(double Y)
        {
            this.Y = Y;
            Console.Clear();
        }
    }
}
