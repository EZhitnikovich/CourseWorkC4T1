using System.Collections.Immutable;
using c4t1.Model;

namespace c4t1.Utils;

public class AStar
{
    public PathCell[,] Grid { get; set; }
    public PriorityQueue<PathCell, double> Open { get; set; }

    public AStar(Labirinth labirinth)
    {
        Grid = new PathCell[labirinth.Width, labirinth.Height];

        for (int x = 0; x < labirinth.Width; x++)
        {
            for (int y = 0; y < labirinth.Height; y++)
            {
                Grid[x, y] = new(new Vector3Int(x, y, labirinth.Cells[x, y].Weight));
            }
        }

        Open = new();
    }

    private double Heuristic(PathCell cell, PathCell goal)
    {
        var dX = Math.Pow(cell.Location.X - goal.Location.X, 2);
        var dY = Math.Pow(cell.Location.Y - goal.Location.Y, 2);
        var dZ = Math.Pow(cell.Location.Z - goal.Location.Z, 2);

        return Math.Sqrt(dX + dY + dZ);

        //var dX = Math.Abs(cell.Location.X - goal.Location.X);
        //var dY = Math.Abs(cell.Location.Y - goal.Location.Y);
        //var dZ = Math.Abs(cell.Location.Z - goal.Location.Z);

        //return (dX + dY + dZ);
    }

    public List<Point> Find(Point start, Point finish)
    {
        var startCell = Grid[start.X, start.Y];
        var finishCell = Grid[finish.X, finish.Y];

        startCell.F = startCell.G + Heuristic(startCell, finishCell);

        Open.Enqueue(startCell, startCell.Location.Z);

        var boundX = Grid.GetLength(0);
        var bountY = Grid.GetLength(1);

        PathCell node = null;

        while (Open.Count > 0)
        {
            node = Open.Dequeue();

            node.Closed = true;

            if (node.Location == finishCell.Location) break;

            var g = node.G + 1;
            var proposed = new Point(0, 0);

            for (int i = 0; i < PathingConstants.Directions.Length; i++)
            {
                var direction = PathingConstants.Directions[i];

                proposed.X = node.Location.X + direction.X;
                proposed.Y = node.Location.Y + direction.Y;

                if (proposed.X < 0 || proposed.X >= boundX ||
                    proposed.Y < 0 || proposed.Y >= bountY)
                    continue;

                var neighbour = Grid[proposed.X, proposed.Y];

                if (neighbour.Closed) continue;

                if (!Open.UnorderedItems.ToImmutableList().Any(p => p.Element == neighbour))
                {
                    neighbour.G = g + neighbour.Location.Z;
                    neighbour.H = Heuristic(node, neighbour);
                    neighbour.Parent = node;
                    Open.Enqueue(neighbour, neighbour.G + neighbour.H);
                }
                else if (g + neighbour.H < neighbour.F)
                {
                    neighbour.G = g;
                    neighbour.F = neighbour.G + neighbour.H;
                    neighbour.Parent = node;
                }
            }
        }

        var path = new Stack<Point>();

        while (node != null)
        {
            path.Push(new Point(Convert.ToInt32(node.Location.X), Convert.ToInt32(node.Location.Y)));
            node = node.Parent;
        }

        return path.ToList();
    }
}