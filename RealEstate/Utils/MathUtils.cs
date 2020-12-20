using Xamarin.Forms;

namespace RealEstate.Utils
{
    public static class MathUtils
    {
        public static double Lerp(double from, double to, double t)
        {
            return from + t * (to - from);
        }
        public static Color Lerp(Color from, Color to, double t)
        {
            return new Color(
                Lerp(from.R, to.R, t),
                Lerp(from.G, to.G, t),
                Lerp(from.B, to.B, t),
                Lerp(from.A, to.A, t)
            );
        }
    }
}
