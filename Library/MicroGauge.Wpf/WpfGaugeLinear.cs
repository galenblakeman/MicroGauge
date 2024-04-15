using System.Windows;
using Brush = System.Windows.Media.Brush;

namespace MicroGauge.Wpf;

public class WpfGaugeLinear : WpfGaugeBase
{
    #region Constructor

    public WpfGaugeLinear()
    {
        Gauge = new GaugeLinear();
        PaintSurface += OnPaintCanvas;
    }

    #endregion

    #region Draw
    /// <summary>
    ///     OnRenderSizeChanged - calculate shaders (in case linear gradient) and redraw
    /// </summary>
    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        GaugeLinear linearGauge = (GaugeLinear)Gauge;
        linearGauge.ValueBarShader = GetSkShader(Gauge, ValueBarBrush);
        base.OnRenderSizeChanged(sizeInfo);
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

    public static readonly DependencyProperty IsVerticalProperty = Create(nameof(IsVertical), typeof(bool),
        (gaugeBase, newValue) => { GetLinear(gaugeBase).IsVertical = (bool)newValue; });

    /// <summary>
    ///     ValueWidthExtent
    /// </summary>
    public float ValueWidthExtent
    {
        get => (float)GetValue(ValueWidthExtentProperty);
        set => SetValue(ValueWidthExtentProperty, value);
    }

    public static readonly DependencyProperty ValueWidthExtentProperty = Create(nameof(ValueWidthExtent), typeof(float),
        (gaugeBase, newValue) => { GetLinear(gaugeBase).ValueWidthExtent = (float)newValue; });

    /// <summary>
    ///     TickWidthExtent
    /// </summary>
    public float TickWidthExtent
    {
        get => (float)GetValue(TickWidthExtentProperty);
        set => SetValue(TickWidthExtentProperty, value);
    }

    public static readonly DependencyProperty TickWidthExtentProperty = Create(nameof(TickWidthExtent), typeof(float),
        (gaugeBase, newValue) => { GetLinear(gaugeBase).TickWidthExtent = (float)newValue; });

    /// <summary>
    ///     MinorTickWidthExtent
    /// </summary>
    public float MinorTickWidthExtent
    {
        get => (float)GetValue(MinorTickWidthExtentProperty);
        set => SetValue(MinorTickWidthExtentProperty, value);
    }

    public static readonly DependencyProperty MinorTickWidthExtentProperty = Create(nameof(MinorTickWidthExtent),
        typeof(float),
        (gaugeBase, newValue) => { GetLinear(gaugeBase).MinorTickWidthExtent = (float)newValue; });

    /// <summary>
    ///     ValueBarBrush
    /// </summary>
    public Brush ValueBarBrush
    {
        get => (Brush)GetValue(ValueBarBrushProperty);
        set => SetValue(ValueBarBrushProperty, value);
    }

    public static readonly DependencyProperty ValueBarBrushProperty = Create(nameof(ValueBarBrush), typeof(Brush),
        (gaugeBase, newValue) =>
        {
            GetLinear(gaugeBase).ValueBarShader = GetSkShader(GetLinear(gaugeBase), (Brush)newValue);
        });

    #endregion
   

    #region Helper

    /// <summary>
    ///     GaugeLinear - Get Linear Gauge from gauge base
    /// </summary>
    private static GaugeLinear GetLinear(WpfGaugeBase gaugeBase)
    {
        return (GaugeLinear)gaugeBase.Gauge;
    }

    #endregion
}