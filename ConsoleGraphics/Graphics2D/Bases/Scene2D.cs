namespace ConsoleGraphics.Graphics2D.Bases
{
    public class Scene2D : IDisposable
    {
        protected List<Point> Map1D { get; set; } = new List<Point>();
        public List<GeometricalObject> Geometricals { get; set; } = new List<GeometricalObject>();
        public CancellationTokenSource CTS { get; set; }
        public GraphicsType Type { get; protected set; }
        public bool CursorVisible
        {
            get => Console.CursorVisible;
            set => Console.CursorVisible = value;
        }
        public Scene2D(GraphicsType Type)
        {
            this.Type = Type;
            CursorVisible = false;
            CTS = new CancellationTokenSource();
            Init();
        }
        public void Init()
        {
            Map1D.Clear();
            for (int i = 0; (Console.WindowWidth * Console.WindowHeight) > i; i++)
                Map1D.Add(Point.GetEmptyPoint(Map1D.Count));
        }
        public void SetPoint(Point Point)
        {
            Map1D[Point.PixelInCosole] = Point;

            try
            {
                Console.SetCursorPosition((int)Point.X, (int)Point.Y);
            }
            catch
            {
                Init();
            }
            Console.SetCursorPosition((int)Point.X, (int)Point.Y);
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
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void DeletePoint(Point Point)
        {
            Point Empty = Point.GetEmptyPoint(Point.X, Point.Y);
            Map1D[(int)(Empty.X * Empty.Y)] = Empty;
            Console.SetCursorPosition((int)Empty.X, (int)Empty.Y);
            Console.Write(Empty.DrawChar);
        }
        public void Add(GeometricalObject Geo)
        {
            if (Geo == null) return;
            Geo.Parent = this;
            Geometricals.Add(Geo);
            Geo.Draw(SetPoint);
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
