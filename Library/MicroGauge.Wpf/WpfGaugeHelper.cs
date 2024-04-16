using System.Windows.Media;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using Brush = System.Windows.Media.Brush;

namespace MicroGauge.Wpf;

public static class WpfGaugeHelper
{
    /// <summary>
    ///     GetSkShader
    /// </summary>
    public static SKShader GetSkShader(Brush sourceBrush,
        float surfaceWidth, float surfaceHeight)
    {
        return sourceBrush switch
        {
            SolidColorBrush solidColorBrush => SKShader.CreateColor(GetSkColor(solidColorBrush)),
            LinearGradientBrush linearGradientBrush => ConvertToSkiaLinearGradient(linearGradientBrush,
                surfaceWidth, surfaceHeight),
            _ => SKShader.CreateColor(SKColors.Transparent)
        };
    }

    /// <summary>
    ///     ConvertToSkiaLinearGradient
    /// </summary>
    private static SKShader ConvertToSkiaLinearGradient(LinearGradientBrush linearBrush,
        float surfaceWidth, float surfaceHeight)
    {
        double startX = linearBrush.StartPoint.X * surfaceWidth;
        double startY = linearBrush.StartPoint.Y * surfaceHeight;
        double endX = linearBrush.EndPoint.X * surfaceWidth;
        double endY = linearBrush.EndPoint.Y * surfaceHeight;

        SKColor[] colors = linearBrush.GradientStops.Select(stop => stop.Color.ToSKColor()).ToArray();
        double[] offsets = linearBrush.GradientStops.Select(stop => stop.Offset).ToArray();
        float[] positions = Array.ConvertAll(offsets, item => (float)item);

        return SKShader.CreateLinearGradient(
            new SKPoint(Convert.ToSingle(startX), Convert.ToSingle(startY)),
            new SKPoint(Convert.ToSingle(endX), Convert.ToSingle(endY)),
            colors,
            positions,
            SKShaderTileMode.Clamp);
    }

    /// <summary>
    ///     GetSkColor
    /// </summary>
    private static SKColor GetSkColor(SolidColorBrush brush)
    {
        return new SKColor(brush.Color.R, brush.Color.G, brush.Color.B, brush.Color.A);
    }
}