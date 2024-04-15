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
            _ => SKShader.CreateColor(SKColors.Red)
        };
    }

    /// <summary>
    ///     ConvertToSkiaLinearGradient
    /// </summary>
    private static SKShader ConvertToSkiaLinearGradient(LinearGradientBrush xamarinBrush,
        float surfaceWidth, float surfaceHeight)
    {
        double startX = xamarinBrush.StartPoint.X * surfaceWidth;
        double startY = xamarinBrush.StartPoint.Y * surfaceHeight;
        double endX = xamarinBrush.EndPoint.X * surfaceWidth;
        double endY = xamarinBrush.EndPoint.Y * surfaceHeight;

        SKColor[] colors = xamarinBrush.GradientStops.Select(stop => stop.Color.ToSKColor()).ToArray();
        float[] positions = xamarinBrush.GradientStops.Select(stop => stop.Offset).ToArray();

        return SKShader.CreateLinearGradient(
            new SKPoint(Convert.ToSingle(startX), Convert.ToSingle(startY)),
            new SKPoint(Convert.ToSingle(endX), Convert.ToSingle(endY)),
            colors,
            positions,
            SKShaderTileMode.Clamp);
    }
}