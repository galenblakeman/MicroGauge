using System;
using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace MicroGauge.Forms
{
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
                    return new GaugeBrush(SKColors.Transparent);
            }
        }

        /// <summary>
        ///     ConvertToGaugeBrush
        /// </summary>
        private static GaugeBrush ConvertToGaugeBrush(LinearGradientBrush platformBrush)
        {
            var startPoint = new SKPoint(Convert.ToSingle(platformBrush.StartPoint.X), Convert.ToSingle(platformBrush.StartPoint.Y));
            var endPoint = new SKPoint(Convert.ToSingle(platformBrush.EndPoint.X), Convert.ToSingle(platformBrush.EndPoint.Y));
            GaugeBrush brush = new GaugeBrush(startPoint, endPoint);
            foreach (var stop in platformBrush.GradientStops)
            {
                brush.AddStop(new GaugeBrushStop(stop.Color.ToSKColor(), stop.Offset));
            }
            return brush;
        }


        /// <summary>
        ///     GetSkShader
        /// </summary>
        public static SKShader GetSkShader(Brush sourceBrush,
            SKPoint offset, float width, float height)
        {
            switch (sourceBrush)
            {
                case SolidColorBrush solidColorBrush:
                    return SKShader.CreateColor(solidColorBrush.Color.ToSKColor());
                case LinearGradientBrush linearGradientBrush:
                    return ConvertToSkiaLinearGradient(linearGradientBrush, offset, width, height);
                default:
                    return SKShader.CreateColor(SKColors.Transparent);
            }
        }

        /// <summary>
        ///     ConvertToSkiaLinearGradient
        /// </summary>
        private static SKShader ConvertToSkiaLinearGradient(LinearGradientBrush platformBrush,
            SKPoint offset, float width, float height)
        {
            var startX = offset.X + platformBrush.StartPoint.X * width;
            var startY = offset.Y + platformBrush.StartPoint.Y * height;
            var endX = offset.X + platformBrush.EndPoint.X * width;
            var endY = offset.Y + platformBrush.EndPoint.Y * height;

            var colors = platformBrush.GradientStops.Select(stop => stop.Color.ToSKColor()).ToArray();
            var positions = platformBrush.GradientStops.Select(stop => stop.Offset).ToArray();

            return SKShader.CreateLinearGradient(
                new SKPoint(Convert.ToSingle(startX), Convert.ToSingle(startY)),
                new SKPoint(Convert.ToSingle(endX), Convert.ToSingle(endY)),
                colors,
                positions,
                SKShaderTileMode.Clamp);
        }
    }
}