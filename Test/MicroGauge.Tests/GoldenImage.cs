using SkiaSharp;

namespace MicroGauge.Tests;

/// <summary>
///     GoldenImage - tolerance-based comparison against checked-in reference PNGs.
///     Set UPDATE_GOLDENS=1 to (re)write the reference images instead of asserting.
/// </summary>
public static class GoldenImage
{
    private const int ChannelTolerance = 8;
    private const double MaxMismatchedFraction = 0.01;

    private static string GoldenDir =>
        Path.Combine(AppContext.BaseDirectory, "Golden");

    private static string SourceGoldenDir
    {
        get
        {
            // Walk up from bin/... to the project directory so updates land in source control
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null && !File.Exists(Path.Combine(dir.FullName, "MicroGauge.Tests.csproj")))
                dir = dir.Parent;
            return Path.Combine(dir?.FullName ?? AppContext.BaseDirectory, "Golden");
        }
    }

    /// <summary>
    ///     Assert - compare a rendered image against the named golden PNG
    /// </summary>
    public static void Assert(SKImage actual, string name)
    {
        if (Environment.GetEnvironmentVariable("UPDATE_GOLDENS") == "1")
        {
            Directory.CreateDirectory(SourceGoldenDir);
            using var update = actual.Encode(SKEncodedImageFormat.Png, 100);
            using var updateStream = File.Create(Path.Combine(SourceGoldenDir, $"{name}.png"));
            update.SaveTo(updateStream);
            return;
        }

        var goldenPath = Path.Combine(GoldenDir, $"{name}.png");
        Xunit.Assert.True(File.Exists(goldenPath),
            $"Golden image missing: {goldenPath}. Run with UPDATE_GOLDENS=1 to create it.");

        using var golden = SKBitmap.Decode(goldenPath);
        using var actualBitmap = SKBitmap.FromImage(actual);
        Xunit.Assert.Equal(golden.Width, actualBitmap.Width);
        Xunit.Assert.Equal(golden.Height, actualBitmap.Height);

        long mismatched = 0;
        for (var y = 0; y < golden.Height; y++)
        for (var x = 0; x < golden.Width; x++)
        {
            var g = golden.GetPixel(x, y);
            var a = actualBitmap.GetPixel(x, y);
            var delta = Math.Max(
                Math.Max(Math.Abs(g.Red - a.Red), Math.Abs(g.Green - a.Green)),
                Math.Max(Math.Abs(g.Blue - a.Blue), Math.Abs(g.Alpha - a.Alpha)));
            if (delta > ChannelTolerance) mismatched++;
        }

        var fraction = mismatched / (double)(golden.Width * golden.Height);
        Xunit.Assert.True(fraction <= MaxMismatchedFraction,
            $"{name}: {fraction:P2} of pixels differ by more than {ChannelTolerance} " +
            $"(allowed {MaxMismatchedFraction:P0}). Run with UPDATE_GOLDENS=1 to accept the new rendering.");
    }
}
