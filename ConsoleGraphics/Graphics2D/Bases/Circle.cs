namespace ConsoleGraphics.Graphics2D.Bases
{
    public class Circle : Geometrical
    {
        private double A { get; set; }
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
        public double RadiusY
        {
            get => B;
            set
            {
                B = value;
                Console.Clear();
            }
        }
        public Circle(double X, double Y, double RadiusX, double RadiusY, Plane2D Parent, ConsoleColor Color) : base(Parent, Color)
        {
            this.X = X;
            this.Y = Y;
            this.RadiusX = RadiusX;
            this.RadiusY = RadiusY;
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
        public override bool Equals(Geometrical? other) => (other is null || other is not Circle) ? false : (other as Circle).RadiusX == RadiusX && (other as Circle).RadiusY == RadiusY;
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
