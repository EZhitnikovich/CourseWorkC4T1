using System.Collections.ObjectModel;
using c4t1.Model;

namespace c4t1.Utils;

internal class PathFinder
{
    public static List<Point> FindPath(Cell[,] field, Point start, Point goal, Graphics g)
    {
        // Шаг 1.
        var explored = new List<PathNode>();
        var reachable = new List<PathNode>();

        // Шаг 2.
        PathNode startNode = new PathNode()
        {
            Position = start,
            CameFrom = null,
            PathLengthFromStart = field[start.X, start.Y].Weight,
            HeuristicEstimatePathLength = GetPathLength(start, goal, field)
        };

        reachable.Add(startNode);

        while (reachable.Count > 0)
        {
            // Шаг 3.
            var currentNode = reachable.OrderBy(node => node.EstimateFullPathLength).First();

            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(currentNode.Position.X * 20, currentNode.Position.Y * 20, 20, 20));

            // Шаг 4.
            if (currentNode.Position == goal)
            {
                return GetPathForNode(currentNode);
            }
            // Шаг 5.
            reachable.Remove(currentNode);
            explored.Add(currentNode);
            // Шаг 6.
            foreach (var neighbourNode in GetNeighbours(currentNode, goal, field))
            {
                // Шаг 7.
                if (explored.Count(node => node.Position == neighbourNode.Position) > 0)
                    continue;

                if (!reachable.Any(x => x.Position == neighbourNode.Position))
                {
                    reachable.Add(neighbourNode);
                }

                else if (currentNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                {
                    // Шаг 9.
                    currentNode.CameFrom = currentNode;
                    currentNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                }
            }
        }

        // Шаг 10.
        return new List<Point>();
    }

    private static int GetPathLength(Point from, Point to, Cell[,] field)
    {
        //return (int)Math.Sqrt(Math.Pow(from.X - to.X, 2) + Math.Pow(from.Y - to.Y, 2) + Math.Pow(field[from.X, from.Y].Weight - field[from.X, from.Y].Weight, 2));

        return (Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y) + Math.Abs(field[from.X, from.Y].Weight - field[from.X, from.Y].Weight));
    }

    private static Collection<PathNode> GetNeighbours(PathNode pathNode, Point goal, Cell[,] field)
    {
        var result = new Collection<PathNode>();

        // Соседними точками являются соседние по стороне клетки.
        Point[] neighbourPoints = new Point[4];
        neighbourPoints[0] = new Point(pathNode.Position.X + 1, pathNode.Position.Y);
        neighbourPoints[1] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);
        neighbourPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y + 1);
        neighbourPoints[3] = new Point(pathNode.Position.X, pathNode.Position.Y - 1);

        foreach (var point in neighbourPoints)
        {
            // Проверяем, что не вышли за границы карты.
            if (point.X < 0 || point.X >= field.GetLength(0))
                continue;
            if (point.Y < 0 || point.Y >= field.GetLength(1))
                continue;

            // Заполняем данные для точки маршрута.
            var neighbourNode = new PathNode()
            {
                Position = point,
                CameFrom = pathNode,
                PathLengthFromStart = pathNode.PathLengthFromStart +
                GetDistanceBetweenNeighbours(field, point),
                HeuristicEstimatePathLength = GetPathLength(point, goal, field)
            };
            result.Add(neighbourNode);
        }
        return result;
    }

    private static List<Point> GetPathForNode(PathNode pathNode)
    {
        var result = new List<Point>();
        var currentNode = pathNode;
        while (currentNode != null)
        {
            result.Add(currentNode.Position);
            currentNode = currentNode.CameFrom;
        }
        result.Reverse();
        return result;
    }

    private static int GetDistanceBetweenNeighbours(Cell[,] field, Point point)
    {
        return field[point.X, point.Y].Weight;
    }
}