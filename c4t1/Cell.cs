using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c4t1
{
    [DebuggerDisplay("Weight:{Weight}")]
    public class Cell
    {
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }
        public int Weight { get; set; } = 0;
    }
}
