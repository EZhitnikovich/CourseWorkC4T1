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
    }
}
