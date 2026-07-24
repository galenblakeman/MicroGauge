using SkiaSharp;

namespace MicroGauge.Tests;

/// <summary>
///     GaugeRenderer - renders gauges to off-screen surfaces for golden-image comparison
/// </summary>
public static class GaugeRenderer
{
    /// <summary>
    ///     Render - draw the gauge onto a raster surface and return the snapshot
    /// </summary>
    public static SKImage Render(GaugeBase gauge, int width, int height)
    {
        using var surface = SKSurface.Create(new SKImageInfo(width, height));
        gauge.Canvas = surface.Canvas;
        gauge.SurfaceWidth = width;
        gauge.SurfaceHeight = height;
        gauge.DrawContent();
        return surface.Snapshot();
    }

    /// <summary>
    ///     MakeTexture - deterministic brushed-metal style texture (seeded random)
    /// </summary>
    public static SKImage MakeTexture()
    {
        using var surface = SKSurface.Create(new SKImageInfo(256, 256));
        var canvas = surface.Canvas;
        canvas.Clear(new SKColor(180, 185, 195));
        using var paint = new SKPaint { StrokeWidth = 1 };
        var rnd = new Random(7);
        for (var i = 0; i < 400; i++)
        {
            var y = (float)(rnd.NextDouble() * 256);
            paint.Color = new SKColor(255, 255, 255, (byte)rnd.Next(10, 60));
            canvas.DrawLine(0, y, 256, y + (float)(rnd.NextDouble() * 4 - 2), paint);
        }

        return surface.Snapshot();
    }
}
