using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using c4t1.Model;

namespace c4t1
{
    internal static class LabirinthChanger
    {
        public static Labirinth Generate(int width, int height)
        {
            var labirinth = new Labirinth(width, height);
            
            for (int i = 0; i < labirinth.Width; i++)
            {
                for (int j = 0; j < labirinth.Height; j++)
                {
                    labirinth.Cells[i, j] = new Cell();
                }
            }

            return labirinth;
        }

        public static Point GetFirstByState(Labirinth labirinth, CellState cellState)
        {
            var point = new Point(-1, -1);

            for (int i = 0; i < labirinth.Width; i++)
            {
                for (int j = 0; j < labirinth.Height; j++)
                {
                    if (labirinth.Cells[i,j].State == cellState)
                    {
                        return new Point(i, j);
                    }
                }
            }

            return point;
        }

        public static void SetNewStartPoint(Labirinth labirinth, int x, int y)
        {
            var point = GetFirstByState(labirinth, CellState.Start);

            if(point != new Point(-1, -1))
            {
                labirinth.Cells[point.X, point.Y].State = CellState.Common;
            }

            labirinth.Cells[x,y].State = CellState.Start;
        }

        public static void SetNewFinishPoint(Labirinth labirinth, int x, int y)
        {
            var point = GetFirstByState(labirinth, CellState.Finish);

            if (point != new Point(-1, -1))
            {
                labirinth.Cells[point.X, point.Y].State = CellState.Common;
            }

            labirinth.Cells[x, y].State = CellState.Finish;
        }

        public static void SetCommonState(Labirinth labirinth, int x, int y)
        {
            labirinth.Cells[x, y].State = CellState.Common;
        }
    }
}
