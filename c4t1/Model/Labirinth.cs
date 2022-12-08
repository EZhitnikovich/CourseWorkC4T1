using System;
namespace c4t1.Model;

public sealed class Labirinth
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Cell[,] Cells { get; private set; }

    public Labirinth(int width, int height)
    {
        Width = width;
        Height = height;
        Cells = new Cell[Width, Height];
    }

    public Labirinth()
    {
        Cells = new Cell[0, 0];
    }

    public void ChangeSize(int width, int height)
    {
        Width = width;
        Height = height;
        Cells = new Cell[Width, Height];
    }

    public void AddWeight(int x, int y, int weight)
    {
        if (x > Width - 1 || x < 0 || y > Height - 1 || y < 0)
        {
            return;
        }

        Cells[x, y].Weight += weight;
        if (Cells[x, y].Weight < 0)
        {
            Cells[x, y].Weight = 0;
        }
    }
}
