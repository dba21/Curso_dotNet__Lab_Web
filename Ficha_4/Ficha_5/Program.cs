﻿// See https://aka.ms/new-console-template for more information

using Ficha_4;
using Ficha_5;


Point p1 = new Point(1, 1);
Point p2 = new Point(4, 4);
Point p3 = new Point(6, 6);

Triangle t1 = new Triangle();
Triangle t2 = new Triangle(p1, p2, p3);

Rectangle r1 = new Rectangle();
Rectangle r2 = new Rectangle(new Point(0, 5), 5, 5);

Circle circle = new Circle();
Circle circle2 = new Circle(new Point(5, 5), 10);

Figure figure = new Figure(); //class, instancia
figure.Add(circle);

figure.Add(t1);
figure.Add(t2);

figure.Add(r1);
figure.Add(r2);

figure.Add(circle);
figure.Add(circle2);

foreach (var shape in figure.Shapes)
{
    Console.WriteLine(shape.Area());
}