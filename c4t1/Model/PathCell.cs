namespace c4t1.Model;

public class PathCell
{
    public Vector3Int Location { get; set; }

    public double G { get; set; }
    public double H { get; set; }
    public double F { get; set; }

    public bool Closed { get; set; }

    public PathCell Parent { get; set; }

    public PathCell(Vector3Int location)
    {
        Location = location;
    }
}