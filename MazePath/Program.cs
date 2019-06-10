using System;
using System.Collections.Generic;
using System.Linq;

namespace MazePath
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] array2D = new char[,] { { 'O', 'O', 'O', 'O' }, { 'D', 'O', 'D', 'O' }, { 'O', 'O', 'O', 'O' }, { 'X', 'D', 'D', 'O' } };  // 4 X 4 Matrix
            Point start = new Point(0, 0);

            int rowlength = array2D.GetLength(0); //x dimension
            int collength = array2D.GetLength(1); // y dimension

            //Queue<Point> queue = new Queue<Point>();
            Stack<Point> stack = new Stack<Point>();
            List<Point> visited = new List<Point>();
            List<Point> pathtraversed = new List<Point>();
            Point end = new Point(3, 0);

            if (IsSafe(array2D, start, rowlength, collength))
            {
                visited.Add(start);
                //queue.Enqueue(start);
                stack.Push(start);
            }

            while (stack.Count > 0) // queue.Count
            {
                //Point p = queue.Dequeue();
                Point p = stack.Pop();
                pathtraversed.Add(p);
                if (p.X == end.X && p.Y == end.Y) break; // reached the destination point break
                // enqueue all the safe neighbors
                var neighborlst = GetNeighbors(p.X, p.Y);
                foreach (var item in neighborlst)
                {
                    if (!visited.Any(point => point.X == item.X && point.Y == item.Y))
                    {
                        if (IsSafe(array2D, item, rowlength, collength))
                        {
                            visited.Add(item);
                            //queue.Enqueue(item);
                            stack.Push(item);
                        }
                    }
                }
            }
            Console.WriteLine("Path taken to reach the destination point");
            foreach (var item in pathtraversed)
            {
                Console.WriteLine(item.X + "," + item.Y);
            }

            Console.ReadKey();

        }

        public static List<Point> GetNeighbors(int x, int y)
        {
            List<Point> neighborlst = new List<Point>
            {
                new Point(x, y + 1), // top
                new Point(x + 1, y), //bottom
                new Point(x - 1, y), // right
                new Point(x, y - 1) // left
            };

            return neighborlst;
        }


        public static bool IsSafe(char[,] maze, Point p, int rowlength, int collength)
        {
            return (p.X >= 0 && p.X < rowlength) && (p.Y >= 0 && p.Y < collength) && maze[p.X, p.Y] != 'D';
        }


        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }

        }
    }

}
