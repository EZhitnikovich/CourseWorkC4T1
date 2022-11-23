using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c4t1
{
    internal class LabirinthDrawer
    {
        public static void Draw(Graphics g, Labirinth labirinth, List<Color> gradient)
        {
            if(labirinth.Cells.Length == 0)
            {
                return;
            }

            var cellSize = 10;
            var castedArr = labirinth.Cells.Cast<Cell>();
            var minWeight = Math.Abs(castedArr.Min(x => x.Weight));
            var maxWeight = Math.Abs(castedArr.Max(x => x.Weight)) + minWeight;

            for (int i = 0; i < labirinth.Width; i++)
            {
                for (int j = 0; j < labirinth.Height; j++)
                {
                    var index = 0;
                    if (maxWeight != minWeight)
                    {
                        index = Convert.ToInt32(Convert.ToDouble(labirinth.Cells[i, j].Weight + minWeight) / Convert.ToDouble(maxWeight) * (gradient.Count - 1));
                    }
                    g.FillRectangle(new SolidBrush(gradient[index]), new Rectangle(i * cellSize, j * cellSize, cellSize, cellSize));

                    //Debugger.Log(0, "", $"{index}, [{i}:{j}], {labirinth.Cells[i, j].Weight}, {minWeight}:{labirinth.Cells[i, j].Weight==minWeight}, {maxWeight}:{labirinth.Cells[i, j].Weight==maxWeight+minWeight}\n");
                }
            }
        }
    }
}
