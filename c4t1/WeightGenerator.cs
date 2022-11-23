using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c4t1
{
    public static class WeightGenerator
    {
        public static void FillWeights(Labirinth labirinth, int changeFrom, int changeTo)
        {
            Random rand = new Random();

            for (int i = 0; i < labirinth.Width; i++)
            {
                for (int j = 0; j < labirinth.Height; j++)
                {
                    int sum = 0;
                    int sumCount = 0;

                    if (j != 0)
                    {
                        sum += labirinth.Cells[i, j - 1].Weight;
                        sumCount++;
                    }

                    if (j != labirinth.Height - 1)
                    {
                        sum += labirinth.Cells[i, j + 1].Weight;
                        sumCount++;
                    }

                    if (i != 0)
                    {
                        sum += labirinth.Cells[i - 1, j].Weight;
                        sumCount++;
                    }

                    if (i != labirinth.Width - 1)
                    {
                        sum += labirinth.Cells[i + 1, j].Weight;
                        sumCount++;
                    }

                    labirinth.Cells[i, j].Weight = sum / sumCount + rand.Next(changeFrom, changeTo) * (rand.Next(0, 3) == 1 ? 1 : -1);
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
    }
}
