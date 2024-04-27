using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace MicroGauge.Forms
{
    /// <summary>
    ///     XfGaugeHelper - static helper methods for Xamarin
    /// </summary>
    public static class XfGaugeHelper
    {
        /// <summary>
        ///     GetGaugeBrush
        /// </summary>
        public static GaugeBrush GetGaugeBrush(Brush sourceBrush)
        {
            switch (sourceBrush)
            {
                case SolidColorBrush solidColorBrush:
                    return new GaugeBrush(solidColorBrush.Color.ToSKColor());
                case LinearGradientBrush linearGradientBrush:
                    return ConvertToGaugeBrush(linearGradientBrush);
                default:
                    return GaugeBrushes.Transparent;
            }
        }

        /// <summary>
        ///     ConvertToGaugeBrush
        /// </summary>
        private static GaugeBrush ConvertToGaugeBrush(LinearGradientBrush platformBrush)
        {
            var startPoint = new SKPoint(Convert.ToSingle(platformBrush.StartPoint.X),
                Convert.ToSingle(platformBrush.StartPoint.Y));
            var endPoint = new SKPoint(Convert.ToSingle(platformBrush.EndPoint.X),
                Convert.ToSingle(platformBrush.EndPoint.Y));
            var brush = new GaugeBrush(startPoint, endPoint);
            foreach (var stop in platformBrush.GradientStops)
                brush.AddStop(new GaugeBrushStop(stop.Color.ToSKColor(), stop.Offset));
            return brush;
        }
    }
}