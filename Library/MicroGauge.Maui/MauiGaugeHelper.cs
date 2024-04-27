using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace MicroGauge.Maui;

public static class MauiGaugeHelper
{
    /// <summary>
    ///     GetGaugeBrush
    /// </summary>
    public static GaugeBrush GetGaugeBrush(Brush sourceBrush)
    {
        return sourceBrush switch
        {
            SolidColorBrush solidColorBrush => new GaugeBrush(solidColorBrush.Color.ToSKColor()),
            LinearGradientBrush linearGradientBrush => ConvertToGaugeBrush(linearGradientBrush),
            _ => GaugeBrushes.Transparent
        };
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
        GaugeBrush brush = new(startPoint, endPoint);
        foreach (var stop in platformBrush.GradientStops)
            brush.AddStop(new GaugeBrushStop(stop.Color.ToSKColor(), stop.Offset));
        return brush;
    }
}