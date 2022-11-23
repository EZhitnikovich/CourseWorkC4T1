using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c4t1
{
    internal static class LabirinthGenerator
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
    }
}
