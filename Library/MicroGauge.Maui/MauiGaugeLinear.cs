namespace MicroGauge.Maui;

/// <summary>
///     MauiGaugeLinear - Linear Gauge with tags and bindings for Maui
/// </summary>
public class MauiGaugeLinear : MauiGaugeBase
{
    #region Constructor

    /// <summary>
    ///     Constructor
    /// </summary>
    public MauiGaugeLinear()
    {
        Gauge = new GaugeLinear();
        PaintSurface += OnPaintCanvas;
    }

    #endregion


    #region Gauge Specific Properties

    /// <summary>
    ///     IsVertical
    /// </summary>
    public bool IsVertical
    {
        get => (bool)GetValue(IsVerticalProperty);
        set => SetValue(IsVerticalProperty, value);
    }

    public static readonly BindableProperty IsVerticalProperty = Create(nameof(IsVertical),
        typeof(bool), false,
        (gaugeBase, newValue) => { GetLinear(gaugeBase).IsVertical = (bool)newValue; });

    /// <summary>
    ///     ValueWidthExtent
    /// </summary>
    public float ValueWidthExtent
    {
        get => (float)GetValue(ValueWidthExtentProperty);
        set => SetValue(ValueWidthExtentProperty, value);
    }

    public static readonly BindableProperty ValueWidthExtentProperty = Create(nameof(ValueWidthExtent),
        typeof(float), 0.5f,
        (gaugeBase, newValue) => { GetLinear(gaugeBase).ValueWidthExtent = (float)newValue; });

    /// <summary>
    ///     TickWidthExtent
    /// </summary>
    public float TickWidthExtent
    {
        get => (float)GetValue(TickWidthExtentProperty);
        set => SetValue(TickWidthExtentProperty, value);
    }

    public static readonly BindableProperty TickWidthExtentProperty = Create(nameof(TickWidthExtent),
        typeof(float), 0.7f,
        (gaugeBase, newValue) => { GetLinear(gaugeBase).TickWidthExtent = (float)newValue; });

    /// <summary>
    ///     MinorTickWidthExtent
    /// </summary>
    public float MinorTickWidthExtent
    {
        get => (float)GetValue(MinorTickWidthExtentProperty);
        set => SetValue(MinorTickWidthExtentProperty, value);
    }

    public static readonly BindableProperty MinorTickWidthExtentProperty = Create(nameof(MinorTickWidthExtent),
        typeof(float), 0.5f,
        (gaugeBase, newValue) => { GetLinear(gaugeBase).MinorTickWidthExtent = (float)newValue; });

    /// <summary>
    ///     ValueBarBrush
    /// </summary>
    public Brush ValueBarBrush
    {
        get => (Brush)GetValue(ValueBarBrushProperty);
        set => SetValue(ValueBarBrushProperty, value);
    }

    public static readonly BindableProperty ValueBarBrushProperty = Create(nameof(ValueBarBrush),
        typeof(Brush), new SolidColorBrush(Colors.Black),
        (gaugeBase, newValue) =>
        {
            GetLinear(gaugeBase).ValueBarBrush = MauiGaugeHelper.GetGaugeBrush((Brush)newValue);
        });

    #endregion

    #region Helper

    /// <summary>
    ///     GaugeLinear - Get Linear Gauge from gauge base
    /// </summary>
    private static GaugeLinear GetLinear(MauiGaugeBase gaugeBase)
    {
        return (GaugeLinear)gaugeBase.Gauge;
    }

    #endregion
}