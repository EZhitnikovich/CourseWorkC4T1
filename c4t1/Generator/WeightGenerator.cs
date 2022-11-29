using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using c4t1.Model;

namespace c4t1.Generator
{
    public static class WeightChanger
    {
        public static void FillRandomWeights(Labirinth labirinth, int changeFrom, int changeTo)
        {
            Random rand = new Random();

            for (int x = 0; x < labirinth.Width; x++)
            {
                for (int y = 0; y < labirinth.Height; y++)
                {
                    int sum = 0;
                    int sumCount = 0;

                    if (y != 0)
                    {
                        sum += labirinth.Cells[x, y - 1].Weight;
                        sumCount++;
                    }

                    if (y != labirinth.Height - 1)
                    {
                        sum += labirinth.Cells[x, y + 1].Weight;
                        sumCount++;
                    }

                    if (x != 0)
                    {
                        sum += labirinth.Cells[x - 1, y].Weight;
                        sumCount++;
                    }

                    if (x != labirinth.Width - 1)
                    {
                        sum += labirinth.Cells[x + 1, y].Weight;
                        sumCount++;
                    }

                    labirinth.Cells[x, y].Weight = sum / sumCount + rand.Next(changeFrom, changeTo) * (rand.Next(0, 3) == 1 ? 1 : -1);
                }
            }
        }

        public static void MakePositiveWeights(Labirinth labirinth)
        {
            var minAbs = Math.Abs(labirinth.Cells.Cast<Cell>().Min(x => x.Weight));

            foreach (var item in labirinth.Cells)
            {
                item.Weight += minAbs;
            }
        }

        public static void AddWeightsInRange(Labirinth labirinth, int weight, int X1, int Y1, int X2, int Y2)
        {
            (X1, X2) = X1 > X2 ? (X2, X1) : (X1, X2);
            (Y1, Y2) = Y1 > Y2 ? (Y2, Y1) : (Y1, Y2);

            for (int i = X1; i <= X2; i++)
            {
                for (int j = Y1; j <= Y2; j++)
                {
                    labirinth.AddWeight(i, j, weight);
                }
            }
        }
    }
}
