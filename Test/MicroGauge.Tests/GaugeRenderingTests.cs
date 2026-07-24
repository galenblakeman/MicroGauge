using MicroGauge.Constant;
using SkiaSharp;
using Xunit;

namespace MicroGauge.Tests;

/// <summary>
///     GaugeRenderingTests - golden-image regression tests for the drawing pipeline.
///     Goldens are generated on Windows; text rendering differs across OS font stacks,
///     so regenerate (UPDATE_GOLDENS=1) when moving these tests to another platform.
/// </summary>
public class GaugeRenderingTests
{
    [Fact]
    public void RadialFullWithMultipleRanges()
    {
        using var gauge = new GaugeRadial
        {
            Value = 42, SetNeedleValue = 70, SetNeedleBrush = GaugeBrushes.Red,
            Ranges =
            {
                MakeRange("#2E7D32", 0, 40), MakeRange("#F9A825", 40, 75), MakeRange("#C62828", 75, 100)
            }
        };
        using var image = GaugeRenderer.Render(gauge, 400, 400);
        GoldenImage.Assert(image, "radial-full-multirange");
    }

    [Fact]
    public void RadialHalf()
    {
        using var gauge = new GaugeRadial
        {
            RadialStyle = GaugeRadialStyle.Half, Value = 66,
            ScaleStartAngle = 180, ScaleEndAngle = 0,
            ValueLocation = GaugeValueLocation.BottomCenter, ValueExtent = 0.3f
        };
        using var image = GaugeRenderer.Render(gauge, 400, 220);
        GoldenImage.Assert(image, "radial-half");
    }

    [Fact]
    public void RadialNotch()
    {
        using var gauge = new GaugeRadial
        {
            RadialStyle = GaugeRadialStyle.Notch, Value = 25,
            ScaleStartAngle = 210, ScaleEndAngle = 330
        };
        using var image = GaugeRenderer.Render(gauge, 400, 400);
        GoldenImage.Assert(image, "radial-notch");
    }

    [Fact]
    public void LinearHorizontal()
    {
        using var gauge = new GaugeLinear
        {
            Value = 61, ValueBarBrush = GaugeBrushes.Blue,
            TopExtent = 0.2f, BottomExtent = 0.2f, LeftExtent = 0.08f, RightExtent = 0.08f
        };
        using var image = GaugeRenderer.Render(gauge, 500, 150);
        GoldenImage.Assert(image, "linear-horizontal");
    }

    [Fact]
    public void LinearVertical()
    {
        using var gauge = new GaugeLinear
        {
            IsVertical = true, Value = 30, ValueBarBrush = GaugeBrushes.Green,
            TopExtent = 0.08f, BottomExtent = 0.08f, LeftExtent = 0.25f, RightExtent = 0.25f
        };
        using var image = GaugeRenderer.Render(gauge, 150, 500);
        GoldenImage.Assert(image, "linear-vertical");
    }

    [Fact]
    public void RadialWithBackgroundImageAndRotation()
    {
        using var texture = GaugeRenderer.MakeTexture();
        using var gauge = new GaugeRadial
        {
            Value = 42, BackgroundImage = texture,
            BackgroundImageOpacity = 0.85f, BackgroundImageRotation = 45
        };
        using var image = GaugeRenderer.Render(gauge, 300, 300);
        GoldenImage.Assert(image, "radial-background-image");
    }

    [Fact]
    public void LinearWithRanges()
    {
        using var gauge = new GaugeLinear
        {
            Value = 61, ValueBarBrush = GaugeBrushes.Blue, ValueWidthExtent = 0.4f,
            TopExtent = 0.2f, BottomExtent = 0.2f, LeftExtent = 0.08f, RightExtent = 0.08f,
            Ranges =
            {
                new GaugeLinearRange
                {
                    BrushHex = "#2E7D32", StartValue = 0, EndValue = 70,
                    InnerStartExtent = 0.3f, InnerEndExtent = 0.3f,
                    OuterStartExtent = 0.5f, OuterEndExtent = 0.5f
                },
                new GaugeLinearRange
                {
                    BrushHex = "#C62828", StartValue = 70, EndValue = 100,
                    InnerStartExtent = 0.3f, InnerEndExtent = 0.3f,
                    OuterStartExtent = 0.5f, OuterEndExtent = 0.5f
                },
                new GaugeLinearRange
                {
                    BrushHex = "#F9A825", StartValue = 0, EndValue = 100,
                    InnerStartExtent = -0.5f, InnerEndExtent = -0.3f,
                    OuterStartExtent = -0.5f, OuterEndExtent = -0.5f
                }
            }
        };
        using var image = GaugeRenderer.Render(gauge, 500, 150);
        GoldenImage.Assert(image, "linear-ranges");
    }

    [Fact]
    public void DegenerateInputsDoNotThrow()
    {
        using var gauge = new GaugeRadial
        {
            TickInterval = 0, MinorTickInterval = 1e-30f, LabelInterval = 0,
            MinValue = 50, MaxValue = 50,
            Ranges = { MakeRange("#2E7D32", 10, 10) }
        };
        using var image = GaugeRenderer.Render(gauge, 200, 200);
        Assert.NotNull(image);
    }

    [Fact]
    public void BadFormatStringFallsBackToPlainNumber()
    {
        using var gauge = new GaugeRadial { Value = 42, LabelFormatString = "{5}", ValueFormatString = "{9}" };
        using var image = GaugeRenderer.Render(gauge, 300, 300);
        GoldenImage.Assert(image, "radial-bad-format-fallback");
    }

    [Fact]
    public void CachedReplayMatchesFirstRender()
    {
        using var gauge = new GaugeRadial
        {
            Value = 42,
            Ranges = { MakeRange("#2E7D32", 0, 40), MakeRange("#C62828", 75, 100) }
        };
        using var first = GaugeRenderer.Render(gauge, 300, 300);
        gauge.Value = 55;
        gauge.Value = 42;
        using var replayed = GaugeRenderer.Render(gauge, 300, 300);
        AssertIdentical(first, replayed);
    }

    [Fact]
    public void StaticCacheInvalidatesOnPropertyChange()
    {
        using var cached = new GaugeRadial { Value = 42, Ranges = { MakeRange("#2E7D32", 0, 40) } };
        using (GaugeRenderer.Render(cached, 300, 300))
        {
        }

        cached.TickBrush = GaugeBrushes.Red;
        cached.Ranges[0].BrushHex = "#C62828";
        cached.InvalidateStaticLayers(); // in-place range brush mutation is by-reference, needs explicit invalidate
        using var afterChange = GaugeRenderer.Render(cached, 300, 300);

        using var fresh = new GaugeRadial
        {
            Value = 42, TickBrush = GaugeBrushes.Red,
            Ranges = { MakeRange("#C62828", 0, 40) }
        };
        using var freshRender = GaugeRenderer.Render(fresh, 300, 300);
        AssertIdentical(afterChange, freshRender);
    }

    private static void AssertIdentical(SKImage expected, SKImage actual)
    {
        using var e = SKBitmap.FromImage(expected);
        using var a = SKBitmap.FromImage(actual);
        Assert.Equal(e.Width, a.Width);
        Assert.Equal(e.Height, a.Height);
        for (var y = 0; y < e.Height; y++)
        for (var x = 0; x < e.Width; x++)
            if (e.GetPixel(x, y) != a.GetPixel(x, y))
                Assert.Fail($"Pixel mismatch at ({x},{y})");
    }

    [Fact]
    public void DisposedGaugeRendersAgain()
    {
        var gauge = new GaugeRadial { Value = 42 };
        using (GaugeRenderer.Render(gauge, 200, 200))
        {
        }

        gauge.Dispose();
        using var image = GaugeRenderer.Render(gauge, 200, 200);
        Assert.NotNull(image);
        gauge.Dispose();
    }

    private static GaugeRadialRange MakeRange(string hex, float start, float end)
    {
        return new GaugeRadialRange
        {
            BrushHex = hex, StartValue = start, EndValue = end,
            InnerStartExtent = 0.6f, InnerEndExtent = 0.6f,
            OuterStartExtent = 0.71f, OuterEndExtent = 0.71f
        };
    }
}
