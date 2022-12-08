using c4t1.Model;
using System;

namespace c4t1.Generator;

public sealed class WeightGenerator
{
    public Labirinth Labirinth { get; private set; }

    public WeightGenerator(Labirinth labirinth)
    {
        Labirinth = labirinth;
    }

    public void FillRandomWeights(int changeFrom, int changeTo)
    {
        Random rand = new Random();

        for (int x = 0; x < Labirinth.Width; x++)
        {
            for (int y = 0; y < Labirinth.Height; y++)
            {
                Labirinth.Cells[x, y].Weight += rand.Next(changeFrom, changeTo) * (rand.Next(0, 5) == 1 ? 1 : -1);
            }
        }
    }

    public void Smooth(int changeFrom, int changeTo)
    {
        Random rand = new Random();

        for (int x = 0; x < Labirinth.Width; x++)
        {
            for (int y = 0; y < Labirinth.Height; y++)
            {
                int sum = 0;
                int sumCount = 0;

                if (y != 0)
                {
                    sum += Labirinth.Cells[x, y - 1].Weight;
                    sumCount++;
                }

                if (y != Labirinth.Height - 1)
                {
                    sum += Labirinth.Cells[x, y + 1].Weight;
                    sumCount++;
                }

                if (x != 0)
                {
                    sum += Labirinth.Cells[x - 1, y].Weight;
                    sumCount++;
                }

                if (x != Labirinth.Width - 1)
                {
                    sum += Labirinth.Cells[x + 1, y].Weight;
                    sumCount++;
                }

                Labirinth.Cells[x, y].Weight = sum / sumCount + rand.Next(changeFrom, changeTo);
            }
        }
    }

    public void Normalize()
    {
        var minAbs = Math.Abs(Labirinth.Cells.Cast<Cell>().Min(x => x.Weight));

        foreach (var item in Labirinth.Cells)
        {
            item.Weight += minAbs;
        }
    }

    public void AddWeightsInRange(int weight, int X1, int Y1, int X2, int Y2)
    {
        (X1, X2) = X1 > X2 ? (X2, X1) : (X1, X2);
        (Y1, Y2) = Y1 > Y2 ? (Y2, Y1) : (Y1, Y2);

        for (int i = X1; i <= X2; i++)
        {
            for (int j = Y1; j <= Y2; j++)
            {
                Labirinth.AddWeight(i, j, weight);
            }
        }
    }
}
