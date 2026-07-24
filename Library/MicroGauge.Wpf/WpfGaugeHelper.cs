using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using Brush = System.Windows.Media.Brush;

namespace MicroGauge.Wpf;

/// <summary>
///     WpfGaugeHelper - static helper methods for WPF
/// </summary>
public static class WpfGaugeHelper
{
    /// <summary>
    ///     GetSkImage - convert a WPF BitmapSource to an SKImage
    /// </summary>
    public static SKImage? GetSkImage(BitmapSource? source)
    {
        if (source == null) return null;
        try
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(source));
            using var stream = new MemoryStream();
            encoder.Save(stream);
            stream.Position = 0;
            return SKImage.FromEncodedData(stream);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    ///     GetSkImageFromPath - load an SKImage from a file path
    ///     (relative paths resolve to the application base directory)
    /// </summary>
    public static SKImage? GetSkImageFromPath(string? imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath)) return null;
        try
        {
            var path = Path.IsPathRooted(imagePath)
                ? imagePath
                : Path.Combine(AppContext.BaseDirectory, imagePath);
            return File.Exists(path) ? SKImage.FromEncodedData(path) : null;
        }
        catch (Exception)
        {
            return null;
        }
    }

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
            brush.AddStop(new GaugeBrushStop(stop.Color.ToSKColor(), Convert.ToSingle(stop.Offset)));
        return brush;
    }
}