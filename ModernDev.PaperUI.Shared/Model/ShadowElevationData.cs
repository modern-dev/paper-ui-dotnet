namespace ModernDev.PaperUI
{
    public sealed class ShadowElevationData
    {
        public ShadowElevationData(double opacity, int depth, int radius)
        {
            Opacity = opacity;
            ShadowDepth = depth;
            BlurRadius = radius;
        }

        public double Opacity { get; }
        public int ShadowDepth { get; }
        public int BlurRadius { get; }
    }
}