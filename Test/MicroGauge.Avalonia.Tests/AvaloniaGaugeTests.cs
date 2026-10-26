using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Headless;
using Avalonia.Headless.NUnit;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Themes.Fluent;
using MicroGauge.Constant;
using NUnit.Framework;
using SkiaSharp;

[assembly: AvaloniaTestApplication(typeof(MicroGauge.Avalonia.Tests.TestAppBuilder))]

namespace MicroGauge.Avalonia.Tests;

public sealed class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<TestApp>()
        .UseSkia()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions { UseHeadlessDrawing = false });
}

public sealed class TestApp : Application
{
    public override void Initialize() => Styles.Add(new FluentTheme());
}

public class AvaloniaGaugeTests
{
    [AvaloniaTest]
    public void StyledPropertiesUpdateTheCoreGauges()
    {
        var radial = new AvaloniaGaugeRadial
        {
            Value = 42,
            RadialStyle = GaugeRadialStyle.Half,
            BackingBrush = Brushes.Red,
            RangeBrush = Brushes.Green
        };
        var linear = new AvaloniaGaugeLinear { Value = 65, IsVertical = true, ValueBarBrush = Brushes.Blue };

        Assert.That(radial.Gauge.Value, Is.EqualTo(42));
        Assert.That(radial.BackingStrokeWidth, Is.EqualTo(10f));
        Assert.That(radial.ValueLocation, Is.EqualTo(GaugeValueLocation.BottomCenter));
        Assert.That(((GaugeRadial)radial.Gauge).RadialStyle, Is.EqualTo(GaugeRadialStyle.Half));
        Assert.That(((GaugeRadial)radial.Gauge).Ranges, Has.Count.EqualTo(1));
        Assert.That(((GaugeLinear)linear.Gauge).IsVertical, Is.True);
        Assert.That(((GaugeLinear)linear.Gauge).ValueBarBrush.GradientStops, Has.Count.EqualTo(2));
    }

    [AvaloniaTest]
    public void ObservableRangesPropagateCollectionChanges()
    {
        var ranges = new ObservableCollection<GaugeLinearRange>();
        var gauge = new AvaloniaGaugeLinear { Ranges = ranges };
        ranges.Add(new GaugeLinearRange { StartValue = 10, EndValue = 30, BrushHex = "#FF008000" });

        Assert.That(((GaugeLinear)gauge.Gauge).Ranges, Has.Count.EqualTo(1));
        Assert.That(((GaugeLinear)gauge.Gauge).Ranges[0].EndValue, Is.EqualTo(30));
    }

    [AvaloniaTest]
    public void PlainListCanBeRefreshedAfterMutation()
    {
        var ranges = new List<GaugeRadialRange>();
        var gauge = new AvaloniaGaugeRadial { Ranges = ranges };
        ranges.Add(new GaugeRadialRange { BrushHex = "#FF008000", StartValue = 0, EndValue = 50 });
        gauge.InvalidateGauge();

        Assert.That(((GaugeRadial)gauge.Gauge).Ranges, Has.Count.EqualTo(1));
    }

    [AvaloniaTest]
    public void ObservableRangesReconnectAfterDetach()
    {
        var ranges = new ObservableCollection<GaugeLinearRange>();
        var gauge = new AvaloniaGaugeLinear { Ranges = ranges };
        var host = new ContentControl { Content = gauge };
        var window = new Window { Content = host };
        window.Show();
        host.Content = null;
        ranges.Add(new GaugeLinearRange { EndValue = 40 });
        host.Content = gauge;
        ranges.Add(new GaugeLinearRange { StartValue = 40 });

        Assert.That(((GaugeLinear)gauge.Gauge).Ranges, Has.Count.EqualTo(2));
        window.Close();
    }

    [AvaloniaTest]
    public void BackgroundImagePathLoadsAndClears()
    {
        var path = Path.Combine(TestContext.CurrentContext.WorkDirectory, $"gauge-{Guid.NewGuid():N}.png");
        try
        {
            using (var surface = SKSurface.Create(new SKImageInfo(8, 8)))
            {
                surface.Canvas.Clear(SKColors.Green);
                using var image = surface.Snapshot();
                using var data = image.Encode(SKEncodedImageFormat.Png, 100);
                File.WriteAllBytes(path, data.ToArray());
            }
            var gauge = new AvaloniaGaugeRadial { BackgroundImagePath = path };
            Assert.That(gauge.Gauge.BackgroundImage, Is.Not.Null);
            gauge.BackgroundImagePath = null;
            Assert.That(gauge.Gauge.BackgroundImage, Is.Null);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [AvaloniaTest]
    public void LinearGradientBrushMapsItsStops()
    {
        var brush = new LinearGradientBrush
        {
            StartPoint = new RelativePoint(0, 0.5, RelativeUnit.Relative),
            EndPoint = new RelativePoint(1, 0.5, RelativeUnit.Relative),
            GradientStops =
            {
                new GradientStop(Colors.Red, 0),
                new GradientStop(Colors.Blue, 1)
            }
        };
        var gauge = new AvaloniaGaugeLinear { ValueBarBrush = brush };
        var converted = ((GaugeLinear)gauge.Gauge).ValueBarBrush;

        Assert.That(converted.GradientStops, Has.Count.EqualTo(2));
        Assert.That(converted.GradientStops[0].Color.Red, Is.EqualTo(255));
        Assert.That(converted.GradientStops[1].Color.Blue, Is.EqualTo(255));
    }

    [AvaloniaTest]
    public void BoundValueUpdatesTheRenderedGauge()
    {
        var slider = new Slider { Minimum = 0, Maximum = 100, Value = 15 };
        var gauge = new AvaloniaGaugeRadial();
        gauge.Bind(AvaloniaGaugeBase.ValueProperty, new Binding(nameof(Slider.Value)) { Source = slider });
        var window = new Window { Content = gauge };
        window.Show();

        Assert.That(gauge.Gauge.Value, Is.EqualTo(15));
        slider.Value = 72;
        Assert.That(gauge.Gauge.Value, Is.EqualTo(72));
        window.Close();
    }

    [AvaloniaTest]
    public void TwoControlsRenderWithoutClearingEachOther()
    {
        var panel = new StackPanel { Orientation = Orientation.Horizontal };
        panel.Children.Add(new AvaloniaGaugeRadial { Width = 150, Height = 150, Value = 40,
            BackingBrush = Brushes.Red, LabelFontBrush = Brushes.Black });
        panel.Children.Add(new AvaloniaGaugeLinear { Width = 150, Height = 150, Value = 65,
            BackingBrush = Brushes.Blue });
        var window = new Window { Width = 300, Height = 150, Content = panel };
        window.Show();

        using var frame = window.CaptureRenderedFrame() ?? throw new AssertionException("No frame was captured");
        using var stream = new MemoryStream();
        frame.Save(stream, PngBitmapEncoderOptions.Default);
        stream.Position = 0;
        using var bitmap = SKBitmap.Decode(stream) ?? throw new AssertionException("Frame could not be decoded");
        Assert.That(bitmap.Width, Is.GreaterThanOrEqualTo(300));
        Assert.That(bitmap.GetPixel(75, 75).Red, Is.GreaterThan(100));
        var bluePixels = 0;
        for (var x = 150; x < 300; x++)
        for (var y = 0; y < 150; y++)
        {
            var color = bitmap.GetPixel(x, y);
            if (color.Blue > color.Red + 30 && color.Blue > color.Green + 30) bluePixels++;
        }
        Assert.That(bluePixels, Is.GreaterThan(100));
        window.Close();
    }
}
