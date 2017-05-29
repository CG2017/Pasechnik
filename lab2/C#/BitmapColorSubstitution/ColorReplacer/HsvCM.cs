using System;
using System.Drawing;

namespace ColorReplacer
{
    internal class HsvCM
    {
        public static void ColorToHsv(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = max == 0 ? 0 : 1d - 1d*min/max;
            value = max/255d;
        }

        public static Color ColorFromHsv(double hue, double saturation, double value)
        {
            var hi = Convert.ToInt32(Math.Floor(hue/60))%6;
            var f = hue/60 - Math.Floor(hue/60);

            value = value*255;
            var v = Convert.ToInt32(value);
            var p = Convert.ToInt32(value*(1 - saturation));
            var q = Convert.ToInt32(value*(1 - f*saturation));
            var t = Convert.ToInt32(value*(1 - (1 - f)*saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            return Color.FromArgb(255, v, p, q);
        }
    }
}