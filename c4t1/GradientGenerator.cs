namespace c4t1
{
    public static class GradientGenerator
    {
        public static List<Color> GetGradient(Color start, Color end, int length)
        {
            var colorList = new List<Color>();

            var avgR = end.R - start.R;
            var avgG = end.G - start.G;
            var avgB = end.B - start.B;

            for (int i = 0; i < length; i++)
            {
                var rAverage = start.R + (avgR * i / length);
                var gAverage = start.G + (avgG * i / length);
                var bAverage = start.B + (avgB * i / length);
                colorList.Add(Color.FromArgb(rAverage, gAverage, bAverage));
            }

            return colorList;
        }
    }
}
