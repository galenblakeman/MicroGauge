using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MicroGauge.Avalonia;

namespace MicroGauge.Example.Avalonia;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        AvaloniaXamlLoader.Load(this);

        var windSpeed = this.FindControl<AvaloniaGaugeRadial>("WindSpeedGauge")!;
        AddRadialRange(windSpeed, 0, 15, "#2E7D32", 0.6f, 0.71f);
        AddRadialRange(windSpeed, 15, 22, "#F9A825", 0.6f, 0.71f);
        AddRadialRange(windSpeed, 22, 30, "#C62828", 0.6f, 0.71f);
        windSpeed.InvalidateGauge();

        var valve = this.FindControl<AvaloniaGaugeLinear>("ValveGauge")!;
        AddLinearRange(valve, 80, 100, "#C62828");
        AddLinearRange(valve, 60, 80, "#F9A825");
        valve.InvalidateGauge();

        var progress = this.FindControl<AvaloniaGaugeRadial>("ProgressGauge")!;
        AddRadialRange(progress, 0, 25, "#EE2400", 0.5f, 0.74f);
        AddRadialRange(progress, 25, 50, "#F3A600", 0.5f, 0.74f);
        AddRadialRange(progress, 50, 75, "#FCFF00", 0.5f, 0.74f);
        AddRadialRange(progress, 75, 100, "#2E7F00", 0.5f, 0.74f);
        progress.InvalidateGauge();
    }

    private static void AddRadialRange(AvaloniaGaugeRadial gauge, float start, float end,
        string color, float inner, float outer) => gauge.Ranges.Add(new GaugeRadialRange
    {
        StartValue = start, EndValue = end, BrushHex = color,
        InnerStartExtent = inner, InnerEndExtent = inner,
        OuterStartExtent = outer, OuterEndExtent = outer
    });

    private static void AddLinearRange(AvaloniaGaugeLinear gauge, float start, float end,
        string color) => gauge.Ranges.Add(new GaugeLinearRange
    {
        StartValue = start, EndValue = end, BrushHex = color,
        InnerStartExtent = 0.3f, InnerEndExtent = 0.3f,
        OuterStartExtent = 0.5f, OuterEndExtent = 0.5f
    });
}
