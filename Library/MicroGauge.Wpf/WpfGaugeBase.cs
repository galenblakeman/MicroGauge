using System.Diagnostics;
using System.Windows;
using MicroGauge.Constant;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using Brush = System.Windows.Media.Brush;

namespace MicroGauge.Wpf;

public abstract class WpfGaugeBase : SKElement
{
    protected delegate void GaugePropertyChanged(WpfGaugeBase gaugeBase, object newValue);
    public GaugeBase Gauge { get; protected init; } = null!;

    #region Draw

    /// <summary>
    ///     OnPaintCanvas - Call draw content
    /// </summary>
    protected void OnPaintCanvas(object? sender, SKPaintSurfaceEventArgs e)
    {
        try
        {
            Gauge.Canvas = e.Surface.Canvas;
            Gauge.SurfaceWidth = e.Info.Width;
            Gauge.SurfaceHeight = e.Info.Height;
            Gauge.DrawContent();
        }
        catch (Exception ex)
        {
            Debug.Write(ex);
            e.Surface.Canvas.Clear(SKColors.Transparent);
        }
    }

    /// <summary>
    ///     OnRenderSizeChanged - calculate shaders (in case linear gradient) and redraw
    /// </summary>
    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        Gauge.BackingShader = GetSkShader(Gauge, BackingBrush);
        Gauge.BackingOutlineShader = GetSkShader(Gauge, BackingOutlineBrush);
        Gauge.TickShader = GetSkShader(Gauge, TickBrush);
        Gauge.MinorTickShader = GetSkShader(Gauge, MinorTickBrush);
        Gauge.LabelFontShader = GetSkShader(Gauge, LabelFontBrush);
        Gauge.ValueFontShader = GetSkShader(Gauge, ValueFontBrush);
        Gauge.NeedleShader = GetSkShader(Gauge, NeedleBrush);
        if (Gauge.SetNeedleShader != null && SetNeedleBrush != null)
            Gauge.SetNeedleShader = GetSkShader(Gauge, SetNeedleBrush);
        ReDraw(this);
    }

    /// <summary>
    ///     ReDraw - Invalidate surface and redraw
    /// </summary>
    private static void ReDraw(UIElement gaugeBase)
    {
        gaugeBase.InvalidateVisual();
    }

    #endregion


    #region Base Properties

    /// <summary>
    ///     BottomExtent
    /// </summary>
    public float BottomExtent
    {
        get => (float)GetValue(BottomExtentProperty);
        set => SetValue(BottomExtentProperty, value);
    }

    public static readonly DependencyProperty BottomExtentProperty = Create(nameof(BottomExtent), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.BottomExtent = (float)newValue; });

    /// <summary>
    ///     TopExtent
    /// </summary>
    public float TopExtent
    {
        get => (float)GetValue(TopExtentProperty);
        set => SetValue(TopExtentProperty, value);
    }

    public static readonly DependencyProperty TopExtentProperty = Create(nameof(TopExtent), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.TopExtent = (float)newValue; });

    /// <summary>
    ///     LeftExtent
    /// </summary>
    public float LeftExtent
    {
        get => (float)GetValue(LeftExtentProperty);
        set => SetValue(LeftExtentProperty, value);
    }

    public static readonly DependencyProperty LeftExtentProperty = Create(nameof(LeftExtent), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.LeftExtent = (float)newValue; });

    /// <summary>
    ///     RightExtent
    /// </summary>
    public float RightExtent
    {
        get => (float)GetValue(RightExtentProperty);
        set => SetValue(RightExtentProperty, value);
    }

    public static readonly DependencyProperty RightExtentProperty = Create(nameof(RightExtent), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.RightExtent = (float)newValue; });

    /// <summary>
    ///     Value
    /// </summary>
    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly DependencyProperty ValueProperty = Create(nameof(Value), typeof(double),
        (gaugeBase, newValue) => { gaugeBase.Gauge.Value = (double)newValue; });

    /// <summary>
    ///     BackingShader
    /// </summary>
    public Brush BackingBrush
    {
        get => (Brush)GetValue(BackingBrushProperty);
        set => SetValue(BackingBrushProperty, value);
    }

    public static readonly DependencyProperty BackingBrushProperty = Create(nameof(BackingBrush), typeof(Brush),
        (gaugeBase, newValue) =>
        {
            gaugeBase.Gauge.BackingShader = GetSkShader(gaugeBase.Gauge, (Brush)newValue);
        });

    /// <summary>
    ///     BackingOutlineBrush
    /// </summary>
    public Brush BackingOutlineBrush
    {
        get => (Brush)GetValue(BackingOutlineProperty);
        set => SetValue(BackingOutlineProperty, value);
    }

    public static readonly DependencyProperty BackingOutlineProperty = Create(nameof(BackingOutlineBrush), typeof(Brush),
        (gaugeBase, newValue) =>
        {
            gaugeBase.Gauge.BackingOutlineShader = GetSkShader(gaugeBase.Gauge, (Brush)newValue);
        });

    /// <summary>
    ///     BackingStrokeWidth
    /// </summary>
    public float BackingStrokeWidth
    {
        get => (float)GetValue(BackingStrokeWidthProperty);
        set => SetValue(BackingStrokeWidthProperty, value);
    }

    public static readonly DependencyProperty BackingStrokeWidthProperty = Create(nameof(BackingStrokeWidth),
        typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.BackingStrokeWidth = (float)newValue; });

    /// <summary>
    ///     TickShader
    /// </summary>
    public Brush TickBrush
    {
        get => (Brush)GetValue(TickBrushProperty);
        set => SetValue(TickBrushProperty, value);
    }

    public static readonly DependencyProperty TickBrushProperty = Create(nameof(TickBrush), typeof(Brush),
        (gaugeBase, newValue) => { gaugeBase.Gauge.TickShader = GetSkShader(gaugeBase.Gauge, (Brush)newValue); });

    /// <summary>
    ///     TickStrokeWidth
    /// </summary>
    public float TickStrokeWidth
    {
        get => (float)GetValue(TickStrokeWidthProperty);
        set => SetValue(TickStrokeWidthProperty, value);
    }

    public static readonly DependencyProperty TickStrokeWidthProperty = Create(nameof(TickStrokeWidth), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.TickStrokeWidth = (float)newValue; });

    /// <summary>
    ///     MinorTickBrush
    /// </summary>
    public Brush MinorTickBrush
    {
        get => (Brush)GetValue(MinorTickBrushProperty);
        set => SetValue(MinorTickBrushProperty, value);
    }

    public static readonly DependencyProperty MinorTickBrushProperty = Create(nameof(MinorTickBrush), typeof(Brush),
        (gaugeBase, newValue) =>
        {
            gaugeBase.Gauge.MinorTickShader = GetSkShader(gaugeBase.Gauge, (Brush)newValue);
        });

    /// <summary>
    ///     MinorTickStrokeWidth
    /// </summary>
    public float MinorTickStrokeWidth
    {
        get => (float)GetValue(MinorTickStrokeWidthProperty);
        set => SetValue(MinorTickStrokeWidthProperty, value);
    }

    public static readonly DependencyProperty MinorTickStrokeWidthProperty = Create(nameof(MinorTickStrokeWidth),
        typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.MinorTickStrokeWidth = (float)newValue; });

    /// <summary>
    ///     Interval
    /// </summary>
    public float Interval
    {
        get => (float)GetValue(IntervalProperty);
        set => SetValue(IntervalProperty, value);
    }

    public static readonly DependencyProperty IntervalProperty = Create(nameof(Interval), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.Interval = (float)newValue; });

    /// <summary>
    ///     MinorInterval
    /// </summary>
    public float MinorInterval
    {
        get => (float)GetValue(MinorIntervalProperty);
        set => SetValue(MinorIntervalProperty, value);
    }

    public static readonly DependencyProperty MinorIntervalProperty = Create(nameof(MinorInterval), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.MinorInterval = (float)newValue; });

    /// <summary>
    ///     MinValue
    /// </summary>
    public float MinValue
    {
        get => (float)GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }

    public static readonly DependencyProperty MinValueProperty = Create(nameof(MinValue), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.MinValue = (float)newValue; });

    /// <summary>
    ///     MaxValue
    /// </summary>
    public float MaxValue
    {
        get => (float)GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    public static readonly DependencyProperty MaxValueProperty = Create(nameof(MaxValue), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.MaxValue = (float)newValue; });

    /// <summary>
    ///     LabelInterval
    /// </summary>
    public float LabelInterval
    {
        get => (float)GetValue(LabelIntervalProperty);
        set => SetValue(LabelIntervalProperty, value);
    }

    public static readonly DependencyProperty LabelIntervalProperty = Create(nameof(LabelInterval), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelInterval = (float)newValue; });

    /// <summary>
    ///     LabelExtent
    /// </summary>
    public float LabelExtent
    {
        get => (float)GetValue(LabelExtentProperty);
        set => SetValue(LabelExtentProperty, value);
    }

    public static readonly DependencyProperty LabelExtentProperty = Create(nameof(LabelExtent), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelExtent = (float)newValue; });

    /// <summary>
    ///     LabelFormatString
    /// </summary>
    public string LabelFormatString
    {
        get => (string)GetValue(LabelFormatStringProperty);
        set => SetValue(LabelFormatStringProperty, value);
    }

    public static readonly DependencyProperty LabelFormatStringProperty = Create(nameof(LabelFormatString),
        typeof(string),
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelFormatString = (string)newValue; });

    /// <summary>
    ///     LabelFontSize
    /// </summary>
    public float LabelFontSize
    {
        get => (float)GetValue(LabelFontSizeProperty);
        set => SetValue(LabelFontSizeProperty, value);
    }

    public static readonly DependencyProperty LabelFontSizeProperty = Create(nameof(LabelFontSize), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelFontSize = (float)newValue; });

    /// <summary>
    ///     LabelFontWeight
    /// </summary>
    public SKFontStyleWeight LabelFontWeight
    {
        get => (SKFontStyleWeight)GetValue(LabelFontWeightProperty);
        set => SetValue(LabelFontWeightProperty, value);
    }

    public static readonly DependencyProperty LabelFontWeightProperty = Create(nameof(LabelFontWeight), typeof(SKFontStyleWeight),
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelFontWeight = (SKFontStyleWeight)newValue; });

    /// <summary>
    ///     FontBrush
    /// </summary>
    public Brush LabelFontBrush
    {
        get => (Brush)GetValue(LabelFontBrushProperty);
        set => SetValue(LabelFontBrushProperty, value);
    }

    public static readonly DependencyProperty LabelFontBrushProperty = Create(nameof(LabelFontBrush), typeof(Brush),
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelFontShader = GetSkShader(gaugeBase.Gauge, (Brush)newValue); });

    /// <summary>
    ///     LabelFontFamily
    /// </summary>
    public string LabelFontFamily
    {
        get => (string)GetValue(LabelFontFamilyProperty);
        set => SetValue(LabelFontFamilyProperty, value);
    }

    public static readonly DependencyProperty LabelFontFamilyProperty = Create(nameof(LabelFontFamily), typeof(string),
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelFontFamily = (string)newValue; });

    /// <summary>
    ///     ValueLocation
    /// </summary>
    public GaugeValueLocation ValueLocation
    {
        get => (GaugeValueLocation)GetValue(ValueLocationProperty);
        set => SetValue(ValueLocationProperty, value);
    }

    public static readonly DependencyProperty ValueLocationProperty = Create(nameof(ValueLocation), typeof(GaugeValueLocation),
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueLocation = (GaugeValueLocation)newValue; });

    /// <summary>
    ///     ValueExtent
    /// </summary>
    public float ValueExtent
    {
        get => (float)GetValue(ValueExtentProperty);
        set => SetValue(ValueExtentProperty, value);
    }

    public static readonly DependencyProperty ValueExtentProperty = Create(nameof(ValueExtent), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueExtent = (float)newValue; });

    /// <summary>
    ///     ValueFormatString
    /// </summary>
    public string ValueFormatString
    {
        get => (string)GetValue(ValueFormatStringProperty);
        set => SetValue(ValueFormatStringProperty, value);
    }

    public static readonly DependencyProperty ValueFormatStringProperty = Create(nameof(ValueFormatString),
        typeof(string),
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueFormatString = (string)newValue; });

    /// <summary>
    ///     ValueFontSize
    /// </summary>
    public float ValueFontSize
    {
        get => (float)GetValue(ValueFontSizeProperty);
        set => SetValue(ValueFontSizeProperty, value);
    }

    public static readonly DependencyProperty ValueFontSizeProperty = Create(nameof(ValueFontSize), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueFontSize = (float)newValue; });

    /// <summary>
    ///     ValueFontWeight
    /// </summary>
    public SKFontStyleWeight ValueFontWeight
    {
        get => (SKFontStyleWeight)GetValue(ValueFontWeightProperty);
        set => SetValue(ValueFontWeightProperty, value);
    }

    public static readonly DependencyProperty ValueFontWeightProperty = Create(nameof(ValueFontWeight), typeof(SKFontStyleWeight),
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueFontWeight = (SKFontStyleWeight)newValue; });

    /// <summary>
    ///     ValueFontBrush
    /// </summary>
    public Brush ValueFontBrush
    {
        get => (Brush)GetValue(ValueFontBrushProperty);
        set => SetValue(ValueFontBrushProperty, value);
    }

    public static readonly DependencyProperty ValueFontBrushProperty = Create(nameof(ValueFontBrush), typeof(Brush),
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueFontShader = GetSkShader(gaugeBase.Gauge, (Brush)newValue); });

    /// <summary>
    ///     ValueFontFamily
    /// </summary>
    public string ValueFontFamily
    {
        get => (string)GetValue(ValueFontFamilyProperty);
        set => SetValue(ValueFontFamilyProperty, value);
    }

    public static readonly DependencyProperty ValueFontFamilyProperty = Create(nameof(ValueFontFamily), typeof(string),
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueFontFamily = (string)newValue; });

    /// <summary>
    ///     NeedleBrush
    /// </summary>
    public Brush NeedleBrush
    {
        get => (Brush)GetValue(NeedleBrushProperty);
        set => SetValue(NeedleBrushProperty, value);
    }

    public static readonly DependencyProperty NeedleBrushProperty = Create(nameof(NeedleBrush), typeof(Brush),
        (gaugeBase, newValue) =>
        {
            gaugeBase.Gauge.NeedleShader = GetSkShader(gaugeBase.Gauge, (Brush)newValue);
        });

    /// <summary>
    ///     NeedleStartWidth
    /// </summary>
    public float NeedleStartWidth
    {
        get => (float)GetValue(NeedleStartWidthProperty);
        set => SetValue(NeedleStartWidthProperty, value);
    }

    public static readonly DependencyProperty NeedleStartWidthProperty = Create(nameof(NeedleStartWidth), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.NeedleStartWidth = (float)newValue; });

    /// <summary>
    ///     NeedleEndWidth
    /// </summary>
    public float NeedleEndWidth
    {
        get => (float)GetValue(NeedleEndWidthProperty);
        set => SetValue(NeedleEndWidthProperty, value);
    }

    public static readonly DependencyProperty NeedleEndWidthProperty = Create(nameof(NeedleEndWidth), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.NeedleEndWidth = (float)newValue; });

    /// <summary>
    ///     NeedleStartExtent
    /// </summary>
    public float NeedleStartExtent
    {
        get => (float)GetValue(NeedleStartExtentProperty);
        set => SetValue(NeedleStartExtentProperty, value);
    }

    public static readonly DependencyProperty NeedleStartExtentProperty = Create(nameof(NeedleStartExtent),
        typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.NeedleStartExtent = (float)newValue; });

    /// <summary>
    ///     NeedleEndExtent
    /// </summary>
    public float NeedleEndExtent
    {
        get => (float)GetValue(NeedleEndExtentProperty);
        set => SetValue(NeedleEndExtentProperty, value);
    }

    public static readonly DependencyProperty NeedleEndExtentProperty = Create(nameof(NeedleEndExtent), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.NeedleEndExtent = (float)newValue; });

    /// <summary>
    ///     SetValue
    /// </summary>
    public float SetNeedleValue
    {
        get => (float)GetValue(SetNeedleValueProperty);
        set => SetValue(SetNeedleValueProperty, value);
    }

    public static readonly DependencyProperty SetNeedleValueProperty = Create(nameof(SetNeedleValue), typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.SetNeedleValue = (float)newValue; });

    /// <summary>
    ///     SetNeedleBrush
    /// </summary>
    public Brush? SetNeedleBrush
    {
        get => (Brush)GetValue(SetNeedleBrushProperty);
        set => SetValue(SetNeedleBrushProperty, value);
    }

    public static readonly DependencyProperty SetNeedleBrushProperty = Create(nameof(SetNeedleBrush), typeof(Brush),
        (gaugeBase, newValue) =>
        {
            gaugeBase.Gauge.SetNeedleShader = GetSkShader(gaugeBase.Gauge, (Brush)newValue);
        });

    /// <summary>
    ///     SetNeedleStartWidth
    /// </summary>
    public float SetNeedleStartWidth
    {
        get => (float)GetValue(SetNeedleStartWidthProperty);
        set => SetValue(SetNeedleStartWidthProperty, value);
    }

    public static readonly DependencyProperty SetNeedleStartWidthProperty = Create(nameof(SetNeedleStartWidth),
        typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.SetNeedleStartWidth = (float)newValue; });

    /// <summary>
    ///     SetNeedleEndWidth
    /// </summary>
    public float SetNeedleEndWidth
    {
        get => (float)GetValue(SetNeedleEndWidthProperty);
        set => SetValue(SetNeedleEndWidthProperty, value);
    }

    public static readonly DependencyProperty SetNeedleEndWidthProperty = Create(nameof(SetNeedleEndWidth),
        typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.SetNeedleEndWidth = (float)newValue; });

    /// <summary>
    ///     SetNeedleStartExtent
    /// </summary>
    public float SetNeedleStartExtent
    {
        get => (float)GetValue(SetNeedleStartExtentProperty);
        set => SetValue(SetNeedleStartExtentProperty, value);
    }

    public static readonly DependencyProperty SetNeedleStartExtentProperty = Create(nameof(SetNeedleStartExtent),
        typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.SetNeedleStartExtent = (float)newValue; });

    /// <summary>
    ///     SetNeedleEndExtent
    /// </summary>
    public float SetNeedleEndExtent
    {
        get => (float)GetValue(SetNeedleEndExtentProperty);
        set => SetValue(SetNeedleEndExtentProperty, value);
    }

    public static readonly DependencyProperty SetNeedleEndExtentProperty = Create(nameof(SetNeedleEndExtent),
        typeof(float),
        (gaugeBase, newValue) => { gaugeBase.Gauge.SetNeedleEndExtent = (float)newValue; });

    #endregion

    #region Helper

    /// <summary>
    ///     GetSkShader - wrapper GetSkShader against this control
    /// </summary>
    protected static SKShader GetSkShader(GaugeBase gauge, Brush brush)
    {
        return WpfGaugeHelper.GetSkShader(brush, gauge.SurfaceWidth, gauge.SurfaceHeight);
    }

    /// <summary>
    ///     Create - wrapper Register against this control
    /// </summary>
    protected static DependencyProperty Create(string propertyName, Type propertyType,
        GaugePropertyChanged propertyChanged)
    {
        return DependencyProperty.Register(
            propertyName,
            propertyType,
            typeof(WpfGaugeBase),
            new FrameworkPropertyMetadata((bindObj, e) =>
            {
                WpfGaugeBase? gauge = (WpfGaugeBase)bindObj;
                propertyChanged(gauge, e.NewValue);
                ReDraw(gauge);
            }));
    }

    #endregion
}