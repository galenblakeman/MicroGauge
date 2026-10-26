using Avalonia.Media;
using Avalonia.Media.Imaging;
using SkiaSharp;

namespace MicroGauge.Avalonia;

public static class AvaloniaGaugeHelper
{
    public static GaugeBrush GetGaugeBrush(IBrush? source) => source switch
    {
        ISolidColorBrush solid => new GaugeBrush(ToSkColor(solid.Color)),
        ILinearGradientBrush gradient => ConvertGradient(gradient),
        _ => GaugeBrushes.Transparent
    };

    private static GaugeBrush ConvertGradient(ILinearGradientBrush source)
    {
        var start = source.StartPoint.Point;
        var end = source.EndPoint.Point;
        var result = new GaugeBrush(new SKPoint((float)start.X, (float)start.Y),
            new SKPoint((float)end.X, (float)end.Y));
        foreach (var stop in source.GradientStops)
            result.AddStop(new GaugeBrushStop(ToSkColor(stop.Color), (float)stop.Offset));
        return result;
    }

    private static SKColor ToSkColor(Color color) => new(color.R, color.G, color.B, color.A);

    public static SKImage? GetSkImage(Bitmap? source)
    {
        if (source is null) return null;
        try
        {
            using var stream = new MemoryStream();
            source.Save(stream, PngBitmapEncoderOptions.Default);
            stream.Position = 0;
            return SKImage.FromEncodedData(stream);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static SKImage? GetSkImageFromPath(string? path)
    {
        if (string.IsNullOrWhiteSpace(path)) return null;
        try
        {
            var resolved = Path.IsPathRooted(path) ? path : Path.Combine(AppContext.BaseDirectory, path);
            return File.Exists(resolved) ? SKImage.FromEncodedData(resolved) : null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
