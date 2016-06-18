namespace PrettyBars.UI
{
    internal class HsvColor
    {
        private HsvColor() { }

        public double H { get; private set; }
        public double S { get; private set; }
        public double V { get; private set; }

        public static HsvColor FromHsv(double h, double s, double v)
        {
            return new HsvColor { H = h % 360, S = s, V = v };
        }
    }
}
