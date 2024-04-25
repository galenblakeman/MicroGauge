using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace MicroGauge.Maui;

public static class MauiGaugeHelper
{
    /// <summary>
    ///     GetSkShader
    /// </summary>
    public static SKShader GetSkShader(Brush sourceBrush,
        SKPoint offset, float width, float height)
    {
        return sourceBrush switch
        {
            SolidColorBrush solidColorBrush => SKShader.CreateColor(solidColorBrush.Color.ToSKColor()),
            LinearGradientBrush linearGradientBrush => ConvertToSkiaLinearGradient(linearGradientBrush,
                offset, width, height),
            _ => SKShader.CreateColor(SKColors.Transparent)
        };
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