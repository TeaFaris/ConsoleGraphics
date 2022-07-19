# ConsoleGraphics
A small library for drawing simple geometric shapes in console.
The library was created for fun, but then it turned into a small project for rendering simple graphics.

## NuGet install:
```
Install-Package ConsoleGraphics -Version 0.1.1
```

## Usage:
### Start
```C#
using ConsoleGraphics.Graphics2D.Bases;
```
First of all, let's create a plane for drawing.
```C#
Plane2D Plane = new Plane2D(GraphicsType.ColoredPoints);
```
Here, in the constructor, there is a GraphicsType value.
GraphicsType is the type of graphics that will be used to draw the dots.
There are 2 types:
```C#
GraphicsType.ColoredSymbols
GraphicsType.ColoredPoints
```
An example of each can be seen in the pictures:

GraphicsType.ColoredSymbols:

![image](https://user-images.githubusercontent.com/95927550/179634873-8f0103bd-d537-4fd4-88ba-9bb649bff65c.png)

GraphicsType.ColoredPoints:

![image](https://user-images.githubusercontent.com/95927550/179634964-4cd0a312-13b5-4fcb-af26-ed0f9873b9fc.png)

### First Object:
Okay, once we've chosen a graphic type and we've already created a plane instance, we can create our first object: Point.
```C#
Point A = new Point(X, Y, Parent, Color, Name, DrawChar);
```
Where X is the X-Coordinate that this point will have.

and Y is the Y-Coordinate that this point will have.

Parent is the Plane2D where the figure will be drawn.

Color is the ConsoleColor with which the Point will be drawn.

Name is name of the point, can be anything, just for convenience. Used in the ToString() method of GeometricalObject.

DrawChar is the char that the Point will be Draw drawn with.

Okey, now we can do this.
```C#
Point A = new Point(0, 0, Plane, ConsoleColor.Magenta, "A", 'O');
```
But if we run the program, then we will not see the point in the console, all because we need to add this point to Plane2D.
```C#
Plane.Add(A);
```
And, tadam, we now have a point drawn in the console!

![image](https://user-images.githubusercontent.com/95927550/179636299-441ce906-0ec1-4623-8ada-58fd138ca81b.png)

Oh, and don't forget to put ```Console.ReadLine();``` at the end of the main method, otherwise it will terminate and not even have time to appear.
### Lines, Shapes, Circles...
Well, what about more complex shapes? Such as line segments, rectangles and circles?

The answer lies here:

By adding 2 points between each other, we can get a line segment.
```C#
Plane2D Plane = new Plane2D(GraphicsType.ColoredPoints);
Point A = new Point(10, 5, Plane, ConsoleColor.Magenta, "A", '*');
Point B = new Point(15, 8, Plane, ConsoleColor.Magenta, "B", '*');
LineSegment AB = A + B;
Plane.Add(AB);
```
Result:

![image](https://user-images.githubusercontent.com/95927550/179637281-f6f16564-42cc-45b0-8c8b-03e3ae2473f2.png)

We can also create a line segment using the constructor.

By adding a point to a line segment, you can already get a shape.
```
Plane2D Plane = new Plane2D(GraphicsType.ColoredPoints);
Point A = new Point(10, 5, Plane, ConsoleColor.Magenta, "A");
Point B = new Point(30, 15, Plane, ConsoleColor.Magenta, "B");
Point C = new Point(10, 15, Plane, ConsoleColor.Magenta, "C");
LineSegment AB = A + B;
Shape ABC = AB + C;
Plane.Add(ABC);
```
Result:

![image](https://user-images.githubusercontent.com/95927550/179637681-673340c7-3675-4a89-bc4f-bd1d2680ae6a.png)

You can also create new shapes by adding a line segment to a line segment.
```C#
Plane2D Plane = new Plane2D(GraphicsType.ColoredPoints);
Point A = new Point(10, 5, Plane, ConsoleColor.Magenta, "A");
Point B = new Point(30, 5, Plane, ConsoleColor.Magenta, "B");
Point C = new Point(30, 15, Plane, ConsoleColor.Magenta, "C");
Point D = new Point(10, 15, Plane, ConsoleColor.Magenta, "D");
LineSegment AB = A + B;
LineSegment CD = C + D;
Shape ABCD = AB + CD;
Plane.Add(ABCD);
```

Result:

![image](https://user-images.githubusercontent.com/95927550/179638339-7d2e8c85-9b60-4327-9497-c44cbc496e91.png)

Points or lines can be added to shapes to create a new shape.
The figure can also be created through the constructor, as it is more convenient for you.

You can also create a circle, though only through the constructor.
```C#
Plane2D Plane = new Plane2D(GraphicsType.ColoredPoints);
Circle C = new Circle(10, 5, 3, 6, Plane, ConsoleColor.Magenta);
Plane.Add(C);
```

Result:

![image](https://user-images.githubusercontent.com/95927550/179638896-fb5fdfab-d83e-431f-ae4f-0448ca38ea05.png)

## At the end:
There are many possibilities with this library, but so far many of my ideas have not yet been implemented here.

I will update this library as soon as possible.

Thank you for your attention.
