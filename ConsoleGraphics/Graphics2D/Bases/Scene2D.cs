namespace ConsoleGraphics.Graphics2D.Bases
{
    /// <summary>
    /// Scene2D is a 2D representation of the surface on which your objects are located and controlled.
    /// </summary>
    /// /// <remarks>Beginning? Check out a short <seealso href="https://github.com/TeaFaris/ConsoleGraphics#usage">guide</seealso> on how to get started using the library.</remarks>
    public class Scene2D : IDisposable
    {
        /// <summary>
        /// 1D Map projection of the console.
        /// </summary>
        protected List<Point> Map1D { get; set; } = new List<Point>();
        /// <summary>
        /// Geometric objects that are on the scene.
        /// </summary>
        public List<GeometricObject> GeometricObjects { get; protected set; } = new List<GeometricObject>();
        /// <summary>
        /// GraphicsType is the type of graphics that will be used to draw the points.
        /// </summary>
        public GraphicsType Type { get; protected set; }
        /// <summary>
        /// Console cursor.
        /// </summary>
        /// <returns>Whether the console cursor is visible to the user.</returns>
        public bool CursorVisible
        {
            get => Console.CursorVisible;
            set => Console.CursorVisible = value;
        }
        /// <summary>
        /// Creates an instance of the scene class in 2D projection.
        /// </summary>
        /// <remarks>Beginning? Check out a short <seealso href="https://github.com/TeaFaris/ConsoleGraphics#usage">guide</seealso> on how to get started using the library.</remarks>
        /// <param name="Type">GraphicsType is the type of graphics that will be used to draw the dots. There are 2 types: <seealso href="https://github.com/TeaFaris/ConsoleGraphics#start">See</seealso></param>
        public Scene2D(GraphicsType Type)
        {
            this.Type = Type;
            CursorVisible = false;
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
        public void Add(GeometricObject Geo)
        {
            if (Geo == null) return;
            Geo.Parent = this;
            GeometricObjects.Add(Geo);
            Geo.Draw(SetPoint);
        }
        public void Dispose()
        {
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
