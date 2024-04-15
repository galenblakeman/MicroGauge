using SkiaSharp;
using System;

namespace MicroGauge
{
    public static class GaugeHelper
    {

        /// <summary>
        ///     GetTicks - intervals fit between the minimum and maximum values, plus one for the starting tick
        /// </summary>
        /// <returns></returns>
        public static int GetTicks(float min, float max, float interval)
        {
            float rawNumberTicks = (max - min) / interval;
            return Convert.ToInt32(Math.Round(rawNumberTicks)) + 1;
        }

        /// <summary>
        ///     GetRadialPoint - Inverted Y
        /// </summary>
        public static SKPoint GetRadialPoint(SKPoint point, float innerRadius, double angle)
        {
            float calcX = Convert.ToSingle(point.X + innerRadius * Math.Cos(angle * Math.PI / 180));
            float calcY = Convert.ToSingle(point.Y - innerRadius * Math.Sin(angle * Math.PI / 180));
            return new SKPoint(calcX, calcY);
        }

        /// <summary>
        ///     GetTextBounds - Measure text to get bounds
        /// </summary>
        public static SKRect GetTextBounds(SKPaint paint, string text)
        {
            SKRect textBounds = new SKRect();
            paint.MeasureText(text, ref textBounds);
            return textBounds;
        }
    }
}
