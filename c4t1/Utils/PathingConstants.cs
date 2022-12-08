public static class PathingConstants
{

    public static readonly Point[] Directions = {

            new Point(-1, +0), // W
            new Point(+1, +0), // E
            new Point(+0, +1), // N 
            new Point(+0, -1), // S
            
            //// Diagonal
            //new Point(-1, -1), // NW
            //new Point(-1, +1), // SW
            //new Point(+1, -1), // NE
            //new Point(+1, +1)  // SE
        };
}