using System.Diagnostics;

namespace c4t1.Model;

[DebuggerDisplay("Weight:{Weight}")]
public class Cell
{
    public bool Up { get; set; }
    public bool Down { get; set; }
    public bool Left { get; set; }
    public bool Right { get; set; }
    public CellState State { get; set; } = CellState.Common;
    public int Weight { get; set; } = 0;
}
