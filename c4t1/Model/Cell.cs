using System.Diagnostics;

namespace c4t1.Model;

[DebuggerDisplay("Weight:{Weight}")]
public sealed class Cell
{
    public CellState State { get; set; } = CellState.Common;
    public int Weight { get; set; } = 0;
}
