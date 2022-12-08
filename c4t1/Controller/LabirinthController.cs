using c4t1.Model;
using System;
using System.IO;

namespace c4t1.Controller;

internal sealed class LabirinthController
{
    public Labirinth Labirinth { get; private set; }

    public LabirinthController()
    {
        Labirinth = new Labirinth();
    }

    public void Generate(int width, int height)
    {
        if (width < 0)
        {
            throw new ArgumentException($"{nameof(width)} can't be less then zero");
        }
        if (height < 0)
        {
            throw new ArgumentException($"{nameof(height)} can't be less then zero");
        }

        Labirinth.ChangeSize(width, height);

        for (int i = 0; i < Labirinth.Width; i++)
        {
            for (int j = 0; j < Labirinth.Height; j++)
            {
                Labirinth.Cells[i, j] = new Cell();
            }
        }
    }

    public void ClearPath()
    {
        foreach (var item in Labirinth.Cells)
        {
            if (item.State == CellState.Path)
            {
                item.State = CellState.Common;
            }
        }
    }

    public void SetPathPoints(List<Point> points)
    {
        foreach (var item in points)
        {
            Labirinth.Cells[item.X, item.Y].State = CellState.Path;
        }
    }

    public int GetPathWeight(List<Point> points)
    {
        int weight = 0;

        foreach (var item in points)
        {
            weight += Labirinth.Cells[item.X, item.Y].Weight;
        }

        return weight;
    }

    public int GetMinPathWeight(List<Point> points)
    {
        int min = int.MaxValue;


        foreach (var item in points)
        {
            if (Labirinth.Cells[item.X, item.Y].Weight < min)
            {
                min = Labirinth.Cells[item.X, item.Y].Weight;
            }
        }

        return min;
    }

    public int GetMaxPathWeight(List<Point> points)
    {
        int max = int.MinValue;

        foreach (var item in points)
        {
            if (Labirinth.Cells[item.X, item.Y].Weight > max)
            {
                max = Labirinth.Cells[item.X, item.Y].Weight;
            }
        }

        return max;
    }

    public Point GetFirstByState(CellState cellState)
    {
        var point = new Point(-1, -1);

        for (int i = 0; i < Labirinth.Width; i++)
        {
            for (int j = 0; j < Labirinth.Height; j++)
            {
                if (Labirinth.Cells[i, j].State == cellState)
                {
                    return new Point(i, j);
                }
            }
        }

        return point;
    }

    public void SetNewState(int x, int y, CellState state)
    {
        if (x < 0 || x > Labirinth.Width - 1)
        {
            throw new ArgumentException($"{nameof(x)} value is incorrect");
        }
        if (y < 0 || y > Labirinth.Height - 1)
        {
            throw new ArgumentException($"{nameof(y)} value is incorrect");
        }

        var point = GetFirstByState(state);

        if (point != new Point(-1, -1))
        {
            Labirinth.Cells[point.X, point.Y].State = CellState.Common;
        }

        Labirinth.Cells[x, y].State = state;
    }

    public void SetCommonState(int x, int y)
    {
        if (x < 0 || x > Labirinth.Width - 1)
        {
            throw new ArgumentException($"{nameof(x)} value is incorrect");
        }
        if (y < 0 || y > Labirinth.Height - 1)
        {
            throw new ArgumentException($"{nameof(y)} value is incorrect");
        }

        Labirinth.Cells[x, y].State = CellState.Common;
    }
}
