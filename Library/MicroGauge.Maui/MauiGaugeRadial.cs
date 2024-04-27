using MicroGauge.Constant;

namespace MicroGauge.Maui;

public class MauiGaugeRadial : MauiGaugeBase
{
    #region Constructor

    public MauiGaugeRadial()
    {
        Gauge = new GaugeRadial();
        PaintSurface += OnPaintCanvas;
    }

    #endregion
   

    #region Gauge Specific Properties

    /// <summary>
    ///     RadialStyle
    /// </summary>
    public GaugeRadialStyle RadialStyle
    {
        get => (GaugeRadialStyle)GetValue(RadialStyleProperty);
        set => SetValue(RadialStyleProperty, value);
    }

    public static readonly BindableProperty RadialStyleProperty = Create(nameof(RadialStyle),
        typeof(GaugeRadialStyle), GaugeRadialStyle.Full,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).RadialStyle = (GaugeRadialStyle)newValue; });

    /// <summary>
    ///     ScaleStartAngle
    /// </summary>
    public float ScaleStartAngle
    {
        get => (float)GetValue(ScaleStartAngleProperty);
        set => SetValue(ScaleStartAngleProperty, value);
    }

    public static readonly BindableProperty ScaleStartAngleProperty = Create(nameof(ScaleStartAngle),
        typeof(float), 210f,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).ScaleStartAngle = (float)newValue; });

    /// <summary>
    ///     ScaleEndAngle
    /// </summary>
    public float ScaleEndAngle
    {
        get => (float)GetValue(ScaleEndAngleProperty);
        set => SetValue(ScaleEndAngleProperty, value);
    }

    public static readonly BindableProperty ScaleEndAngleProperty = Create(nameof(ScaleEndAngle),
        typeof(float), 330f,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).ScaleEndAngle = (float)newValue; });

    /// <summary>
    ///     TickStartExtent
    /// </summary>
    public float TickStartExtent
    {
        get => (float)GetValue(TickStartExtentProperty);
        set => SetValue(TickStartExtentProperty, value);
    }

    public static readonly BindableProperty TickStartExtentProperty = Create(nameof(TickStartExtent),
        typeof(float), 0.6f,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).TickStartExtent = (float)newValue; });

    /// <summary>
    ///     TickEndExtent
    /// </summary>
    public float TickEndExtent
    {
        get => (float)GetValue(TickEndExtentProperty);
        set => SetValue(TickEndExtentProperty, value);
    }

    public static readonly BindableProperty TickEndExtentProperty = Create(nameof(TickEndExtent),
        typeof(float), 0.7f,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).TickEndExtent = (float)newValue; });

    /// <summary>
    ///     MinorTickStartExtent
    /// </summary>
    public float MinorTickStartExtent
    {
        get => (float)GetValue(MinorTickStartExtentProperty);
        set => SetValue(MinorTickStartExtentProperty, value);
    }

    public static readonly BindableProperty MinorTickStartExtentProperty = Create(nameof(MinorTickStartExtent),
        typeof(float), 0.65f,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).MinorTickStartExtent = (float)newValue; });

    /// <summary>
    ///     MinorTickEndExtent
    /// </summary>
    public float MinorTickEndExtent
    {
        get => (float)GetValue(MinorTickEndExtentProperty);
        set => SetValue(MinorTickEndExtentProperty, value);
    }

    public static readonly BindableProperty MinorTickEndExtentProperty = Create(nameof(MinorTickEndExtent),
        typeof(float), 0.7f,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).MinorTickEndExtent = (float)newValue; });

    /// <summary>
    ///     NeedlePivotEndExtent
    /// </summary>
    public float NeedlePivotEndExtent
    {
        get => (float)GetValue(NeedlePivotEndExtentProperty);
        set => SetValue(NeedlePivotEndExtentProperty, value);
    }

    public static readonly BindableProperty NeedlePivotEndExtentProperty = Create(nameof(NeedlePivotEndExtent),
        typeof(float), 0.1f,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).NeedlePivotEndExtent = (float)newValue; });

    /// <summary>
    ///     NeedlePivotBrush
    /// </summary>
    public Brush NeedlePivotBrush
    {
        get => (Brush)GetValue(NeedlePivotBrushProperty);
        set => SetValue(NeedlePivotBrushProperty, value);
    }

    public static readonly BindableProperty NeedlePivotBrushProperty = Create(nameof(NeedlePivotBrush),
        typeof(Brush), new SolidColorBrush(Colors.LightGray),
        (gaugeBase, newValue) =>
        {
            GetRadial(gaugeBase).NeedlePivotBrush = MauiGaugeHelper.GetGaugeBrush( (Brush)newValue);
        });

    /// <summary>
    ///     NeedlePivotOutline
    /// </summary>
    public Brush NeedlePivotOutlineBrush
    {
        get => (Brush)GetValue(NeedlePivotOutlineBrushProperty);
        set => SetValue(NeedlePivotOutlineBrushProperty, value);
    }

    public static readonly BindableProperty NeedlePivotOutlineBrushProperty = Create(nameof(NeedlePivotOutlineBrush),
        typeof(Brush), new SolidColorBrush(Colors.Black),
        (gaugeBase, newValue) =>
        {
            GetRadial(gaugeBase).NeedlePivotOutlineBrush = MauiGaugeHelper.GetGaugeBrush( (Brush)newValue);
        });

    /// <summary>
    ///     NeedlePivotOutlineWidth
    /// </summary>
    public float NeedlePivotOutlineWidth
    {
        get => (float)GetValue(NeedlePivotOutlineWidthProperty);
        set => SetValue(NeedlePivotOutlineWidthProperty, value);
    }

    public static readonly BindableProperty NeedlePivotOutlineWidthProperty = Create(nameof(NeedlePivotOutlineWidth),
        typeof(float), 2f,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).NeedlePivotOutlineWidth = (float)newValue; });

    /// <summary>
    ///     RangeBrush
    /// </summary>
    public Brush RangeBrush
    {
        get => (Brush)GetValue(RangeBrushProperty);
        set => SetValue(RangeBrushProperty, value);
    }

    public static readonly BindableProperty RangeBrushProperty = Create(nameof(RangeBrush),
        typeof(Brush), new SolidColorBrush(Colors.Transparent),
        (gaugeBase, newValue) =>
        {
            GetRadial(gaugeBase).RangeBrush = MauiGaugeHelper.GetGaugeBrush((Brush)newValue);
        });

    /// <summary>
    ///     RangeInnerStartExtent
    /// </summary>
    public float RangeInnerStartExtent
    {
        get => (float)GetValue(RangeInnerStartExtentProperty);
        set => SetValue(RangeInnerStartExtentProperty, value);
    }

    public static readonly BindableProperty RangeInnerStartExtentProperty = Create(nameof(RangeInnerStartExtent),
        typeof(float), 0f,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).RangeInnerStartExtent = (float)newValue; });

    /// <summary>
    ///     RangeInnerEndExtent
    /// </summary>
    public float RangeInnerEndExtent
    {
        get => (float)GetValue(RangeInnerEndExtentProperty);
        set => SetValue(RangeInnerEndExtentProperty, value);
    }

    public static readonly BindableProperty RangeInnerEndExtentProperty = Create(nameof(RangeInnerEndExtent),
        typeof(float), 0f,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).RangeInnerEndExtent = (float)newValue; });

    /// <summary>
    ///     RangeOuterStartExtent
    /// </summary>
    public float RangeOuterStartExtent
    {
        get => (float)GetValue(RangeOuterStartExtentProperty);
        set => SetValue(RangeOuterStartExtentProperty, value);
    }

    public static readonly BindableProperty RangeOuterStartExtentProperty = Create(nameof(RangeOuterStartExtent),
        typeof(float), 0f,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).RangeOuterStartExtent = (float)newValue; });

    /// <summary>
    ///     RangeOuterEndExtent
    /// </summary>
    public float RangeOuterEndExtent
    {
        get => (float)GetValue(RangeOuterEndExtentProperty);
        set => SetValue(RangeOuterEndExtentProperty, value);
    }

    public static readonly BindableProperty RangeOuterEndExtentProperty = Create(nameof(RangeOuterEndExtent),
        typeof(float), 0f,
        (gaugeBase, newValue) => { GetRadial(gaugeBase).RangeOuterEndExtent = (float)newValue; });

    #endregion

    #region Helper

    /// <summary>
    ///     GetRadial - Get Radial Gauge from gauge base
    /// </summary>
    private static GaugeRadial GetRadial(MauiGaugeBase gaugeBase)
    {
        return (GaugeRadial)gaugeBase.Gauge;
    }

    #endregion
}