using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c4t1
{
    public class Labirinth
    {
        public int Width { get; set; }
        public int Height { get; set; }

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

        public bool TryGetWeight(int x, int y, out int weight)
        {
            if(x > Width - 1 || x < 0 || y > Height - 1 || y < 0)
            {
                weight = -1;
                return false;
            }

            weight = Cells[x, y].Weight;
            return true;
        }
    }
}
