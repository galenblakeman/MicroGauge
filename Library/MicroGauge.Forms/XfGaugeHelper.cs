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
        ///     GetSkShader
        /// </summary>
        public static SKShader GetSkShader(Brush sourceBrush,
            float surfaceWidth, float surfaceHeight)
        {
            switch (sourceBrush)
            {
                case SolidColorBrush solidColorBrush:
                    return SKShader.CreateColor(solidColorBrush.Color.ToSKColor());
                case LinearGradientBrush linearGradientBrush:
                    return ConvertToSkiaLinearGradient(linearGradientBrush, surfaceWidth, surfaceHeight);
                default:
                    return SKShader.CreateColor(SKColors.Transparent);
            }
        }

        /// <summary>
        ///     ConvertToSkiaLinearGradient
        /// </summary>
        private static SKShader ConvertToSkiaLinearGradient(LinearGradientBrush xamarinBrush,
            float surfaceWidth, float surfaceHeight)
        {
            var startX = xamarinBrush.StartPoint.X * surfaceWidth;
            var startY = xamarinBrush.StartPoint.Y * surfaceHeight;
            var endX = xamarinBrush.EndPoint.X * surfaceWidth;
            var endY = xamarinBrush.EndPoint.Y * surfaceHeight;

            var colors = xamarinBrush.GradientStops.Select(stop => stop.Color.ToSKColor()).ToArray();
            var positions = xamarinBrush.GradientStops.Select(stop => stop.Offset).ToArray();

            return SKShader.CreateLinearGradient(
                new SKPoint(Convert.ToSingle(startX), Convert.ToSingle(startY)),
                new SKPoint(Convert.ToSingle(endX), Convert.ToSingle(endY)),
                colors,
                positions,
                SKShaderTileMode.Clamp);
        }
    }
}