using System;
using Windows.UI;
using PrettyBars.UI;

namespace PrettyBars
{
    internal class ColorUtils
    {
        public static HsvColor RgbToHsv(Color c)
        {
            var max = Math.Max(Math.Max(c.R, c.G), c.B);
            var min = Math.Min(Math.Min(c.R, c.G), c.B);
            double h, s, v;
            if (max == min)
            {
                h = 0;
            }
            else if (max == c.R)
            {
                h = (60 * (c.G - c.B) / ((double)max - min) + 360) % 360;
            }
            else if (max == c.G)
            {
                h = (60 * (c.B - c.R) / ((double)max - min)) + 120;
            }
            else if (max == c.B)
            {
                h = (60 * (c.R - c.G) / ((double)max - min)) + 240;
            }
            else
            {
                throw new ArgumentException();
            }

            if (max == 0)
            {
                s = 0;
            }
            else
            {
                s = ((max - min) / (double)max);
            }
            v = max / 255.0;
            return HsvColor.FromHsv(h, s, v);
        }

        public static Color HsvToRgb(HsvColor c)
        {
            var hi = Math.Floor(c.H / 60.0) % 6;
            var f = (c.H / 60.0) - Math.Floor(c.H / 60.0);
            var p = Math.Round(c.V * (1.0 - (c.S)) * 255);
            var q = Math.Round(c.V * (1.0 - (c.S) * f) * 255);
            var t = Math.Round(c.V * (1.0 - (c.S) * (1.0 - f)) * 255);

            double r, g, b;
            switch ((byte)hi)
            {
                default:
                    throw new ArgumentException();
                case 0:
                    r = c.V * 255; g = t; b = p;
                    break;
                case 1:
                    r = q; g = c.V * 255; b = t;
                    break;
                case 2:
                    r = p; g = c.V * 255; b = t;
                    break;
                case 3:
                    r = p; g = q; b = c.V * 255;
                    break;
                case 4:
                    r = t; g = p; b = c.V * 255;
                    break;
                case 5:
                    r = c.V * 255; g = p; b = q;
                    break;
            }

            return Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
        }

        public static Color Darker(Color c)
        {
            var hsv = RgbToHsv(c);
            return HsvToRgb(HsvColor.FromHsv(hsv.H, hsv.S, hsv.V * 0.8));
        }

        public static Color Lighter(Color c)
        {
            var hsv = RgbToHsv(c);
            return HsvToRgb(HsvColor.FromHsv(hsv.H, hsv.S, Math.Min(1.0, hsv.V * 1.2)));
        }

        public static bool IsLight(Color c)
        {
            var hsv = RgbToHsv(c);
            return hsv.V >= 0.65;
        }

        public static bool IsDark(Color c)
        {
            return !IsLight(c);
        }
    }
}
