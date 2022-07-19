namespace ConsoleGraphics.Graphics2D.Bases
{
    public class Plane2D : IDisposable
    {
        protected List<Point> Map1D { get; set; } = new List<Point>();
        public List<GeometricalObject> Geometricals { get; set; } = new List<GeometricalObject>();
        public CancellationTokenSource CTS { get; set; }
        public GraphicsType Type { get; protected set; }
        public Plane2D(GraphicsType Type)
        {
            this.Type = Type;
            CTS = new CancellationTokenSource();
            Task Upd = new Task(() => Update(CTS.Token), CTS.Token);
            Upd.Start();
        }
        public void Update(CancellationToken CT = default(CancellationToken))
        {
            for (int i = 0; (Console.WindowWidth * Console.WindowHeight) > i; i++)
                Map1D.Add(new Point(Map1D.Count, this, ConsoleColor.White, i.ToString(), ' '));
            while (!CT.IsCancellationRequested)
                foreach (GeometricalObject G in Geometricals)
                    G.Draw();
        }
        public void SetPoint(Point Point)
        {
            Map1D[Point.PixelInCosole] = Point;
            Console.CursorVisible = false;
            try
            {
                Console.SetCursorPosition((int)Point.X, (int)Point.Y);
            }
            catch
            {
                Map1D.Clear();
                for (int i = 0; (Console.WindowWidth * Console.WindowHeight) > i; i++)
                    Map1D.Add(new Point(Map1D.Count, this, ConsoleColor.White, i.ToString(), ' '));
            }
            switch (Type)
            {
                case GraphicsType.ColoredSymbols:
                    Console.ForegroundColor = Point.Color;
                    break;
                case GraphicsType.ColoredPoints:
                    Console.BackgroundColor = Point.Color;
                    goto case GraphicsType.ColoredSymbols;
                default:
                    goto case GraphicsType.ColoredSymbols;
            }
            Console.Write(Point.DrawChar);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void Add(GeometricalObject Geo)
        {
            if (Geo == null) return;
            Geometricals.Add(Geo);
        }

        public void Dispose()
        {
            CTS.Cancel();
            Console.Clear();
            Map1D.Clear();
        }
    }
    public enum GraphicsType
    {
        ColoredSymbols,
        ColoredPoints
    }
}
