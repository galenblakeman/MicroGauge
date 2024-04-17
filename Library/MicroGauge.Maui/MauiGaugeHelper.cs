using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace MicroGauge.Maui;

public static class MauiGaugeHelper
{
    /// <summary>
    ///     GetSkShader
    /// </summary>
    public static SKShader GetSkShader(Brush sourceBrush,
        float surfaceWidth, float surfaceHeight)
    {
        return sourceBrush switch
        {
            SolidColorBrush solidColorBrush => SKShader.CreateColor(solidColorBrush.Color.ToSKColor()),
            LinearGradientBrush linearGradientBrush => ConvertToSkiaLinearGradient(linearGradientBrush,
                surfaceWidth, surfaceHeight),
            _ => SKShader.CreateColor(SKColors.Transparent)
        };
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