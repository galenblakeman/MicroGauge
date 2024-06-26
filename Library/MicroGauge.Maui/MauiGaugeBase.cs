﻿using System.Diagnostics;
using MicroGauge.Constant;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

// ReSharper disable SuggestBaseTypeForParameter


namespace MicroGauge.Maui;

/// <summary>
///     MauiGaugeBase - abstract class with shared common properties and methods
/// </summary>
public abstract class MauiGaugeBase : SKCanvasView
{
    /// <summary>
    ///     GaugePropertyChanged - delegate used to propagate changes
    /// </summary>
    protected delegate void GaugePropertyChanged(MauiGaugeBase gaugeBase, object newValue);

    /// <summary>
    ///     Gauge - Wrapped MicroGauge
    /// </summary>
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
    ///     ReDraw - Invalidate surface and redraw
    /// </summary>
    private static void ReDraw(MauiGaugeBase gaugeBase)
    {
        gaugeBase.InvalidateSurface();
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

    public static readonly BindableProperty BottomExtentProperty = Create(nameof(BottomExtent),
        typeof(float), 0.05f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.BottomExtent = (float)newValue; });

    /// <summary>
    ///     TopExtent
    /// </summary>
    public float TopExtent
    {
        get => (float)GetValue(TopExtentProperty);
        set => SetValue(TopExtentProperty, value);
    }

    public static readonly BindableProperty TopExtentProperty = Create(nameof(TopExtent),
        typeof(float), 0.05f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.TopExtent = (float)newValue; });

    /// <summary>
    ///     LeftExtent
    /// </summary>
    public float LeftExtent
    {
        get => (float)GetValue(LeftExtentProperty);
        set => SetValue(LeftExtentProperty, value);
    }

    public static readonly BindableProperty LeftExtentProperty = Create(nameof(LeftExtent),
        typeof(float), 0.05f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.LeftExtent = (float)newValue; });

    /// <summary>
    ///     RightExtent
    /// </summary>
    public float RightExtent
    {
        get => (float)GetValue(RightExtentProperty);
        set => SetValue(RightExtentProperty, value);
    }

    public static readonly BindableProperty RightExtentProperty = Create(nameof(RightExtent),
        typeof(float), 0.05f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.RightExtent = (float)newValue; });

    /// <summary>
    ///     Value
    /// </summary>
    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly BindableProperty ValueProperty = Create(nameof(Value),
        typeof(double), 0.0,
        (gaugeBase, newValue) => { gaugeBase.Gauge.Value = (double)newValue; });

    /// <summary>
    ///     BackingBrush
    /// </summary>
    public Brush BackingBrush
    {
        get => (Brush)GetValue(BackingBrushProperty);
        set => SetValue(BackingBrushProperty, value);
    }

    public static readonly BindableProperty BackingBrushProperty = Create(nameof(BackingBrush),
        typeof(Brush), new SolidColorBrush(Colors.White),
        (gaugeBase, newValue) => { gaugeBase.Gauge.BackingBrush = MauiGaugeHelper.GetGaugeBrush((Brush)newValue); });

    /// <summary>
    ///     BackingOutlineBrush
    /// </summary>
    public Brush BackingOutlineBrush
    {
        get => (Brush)GetValue(BackingOutlineBrushProperty);
        set => SetValue(BackingOutlineBrushProperty, value);
    }

    public static readonly BindableProperty BackingOutlineBrushProperty = Create(nameof(BackingOutlineBrush),
        typeof(Brush), new SolidColorBrush(Colors.Black),
        (gaugeBase, newValue) =>
        {
            gaugeBase.Gauge.BackingOutlineBrush = MauiGaugeHelper.GetGaugeBrush((Brush)newValue);
        });

    /// <summary>
    ///     BackingStrokeWidth
    /// </summary>
    public float BackingStrokeWidth
    {
        get => (float)GetValue(BackingStrokeWidthProperty);
        set => SetValue(BackingStrokeWidthProperty, value);
    }

    public static readonly BindableProperty BackingStrokeWidthProperty = Create(nameof(BackingStrokeWidth),
        typeof(float), 10f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.BackingStrokeWidth = (float)newValue; });

    /// <summary>
    ///     TickBrush
    /// </summary>
    public Brush TickBrush
    {
        get => (Brush)GetValue(TickBrushProperty);
        set => SetValue(TickBrushProperty, value);
    }

    public static readonly BindableProperty TickBrushProperty = Create(nameof(TickBrush),
        typeof(Brush), new SolidColorBrush(Colors.Black),
        (gaugeBase, newValue) => { gaugeBase.Gauge.TickBrush = MauiGaugeHelper.GetGaugeBrush((Brush)newValue); });

    /// <summary>
    ///     TickStrokeWidth
    /// </summary>
    public float TickStrokeWidth
    {
        get => (float)GetValue(TickStrokeWidthProperty);
        set => SetValue(TickStrokeWidthProperty, value);
    }

    public static readonly BindableProperty TickStrokeWidthProperty = Create(nameof(TickStrokeWidth),
        typeof(float), 1f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.TickStrokeWidth = (float)newValue; });

    /// <summary>
    ///     MinorTickBrush
    /// </summary>
    public Brush MinorTickBrush
    {
        get => (Brush)GetValue(MinorTickBrushProperty);
        set => SetValue(MinorTickBrushProperty, value);
    }

    public static readonly BindableProperty MinorTickBrushProperty = Create(nameof(MinorTickBrush),
        typeof(Brush), new SolidColorBrush(Colors.LightGray),
        (gaugeBase, newValue) => { gaugeBase.Gauge.MinorTickBrush = MauiGaugeHelper.GetGaugeBrush((Brush)newValue); });

    /// <summary>
    ///     MinorTickStrokeWidth
    /// </summary>
    public float MinorTickStrokeWidth
    {
        get => (float)GetValue(MinorTickStrokeWidthProperty);
        set => SetValue(MinorTickStrokeWidthProperty, value);
    }

    public static readonly BindableProperty MinorTickStrokeWidthProperty = Create(nameof(MinorTickStrokeWidth),
        typeof(float), 0.5f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.MinorTickStrokeWidth = (float)newValue; });

    /// <summary>
    ///     TickInterval
    /// </summary>
    public float TickInterval
    {
        get => (float)GetValue(TickIntervalProperty);
        set => SetValue(TickIntervalProperty, value);
    }

    public static readonly BindableProperty TickIntervalProperty = Create(nameof(TickInterval),
        typeof(float), 5f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.TickInterval = (float)newValue; });

    /// <summary>
    ///     MinorTickInterval
    /// </summary>
    public float MinorTickInterval
    {
        get => (float)GetValue(MinorTickIntervalProperty);
        set => SetValue(MinorTickIntervalProperty, value);
    }

    public static readonly BindableProperty MinorTickIntervalProperty = Create(nameof(MinorTickInterval),
        typeof(float), 1f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.MinorTickInterval = (float)newValue; });

    /// <summary>
    ///     MinValue
    /// </summary>
    public float MinValue
    {
        get => (float)GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }

    public static readonly BindableProperty MinValueProperty = Create(nameof(MinValue),
        typeof(float), 0f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.MinValue = (float)newValue; });

    /// <summary>
    ///     MaxValue
    /// </summary>
    public float MaxValue
    {
        get => (float)GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    public static readonly BindableProperty MaxValueProperty = Create(nameof(MaxValue),
        typeof(float), 100f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.MaxValue = (float)newValue; });

    /// <summary>
    ///     LabelInterval
    /// </summary>
    public float LabelInterval
    {
        get => (float)GetValue(LabelIntervalProperty);
        set => SetValue(LabelIntervalProperty, value);
    }

    public static readonly BindableProperty LabelIntervalProperty = Create(nameof(LabelInterval),
        typeof(float), 10f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelInterval = (float)newValue; });

    /// <summary>
    ///     LabelExtent
    /// </summary>
    public float LabelExtent
    {
        get => (float)GetValue(LabelExtentProperty);
        set => SetValue(LabelExtentProperty, value);
    }

    public static readonly BindableProperty LabelExtentProperty = Create(nameof(LabelExtent),
        typeof(float), 0.85f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelExtent = (float)newValue; });

    /// <summary>
    ///     LabelFormatString
    /// </summary>
    public string LabelFormatString
    {
        get => (string)GetValue(LabelFormatStringProperty);
        set => SetValue(LabelFormatStringProperty, value);
    }

    public static readonly BindableProperty LabelFormatStringProperty = Create(nameof(LabelFormatString),
        typeof(string), "{0:N0}",
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelFormatString = (string)newValue; });

    /// <summary>
    ///     LabelFontSize
    /// </summary>
    public float LabelFontSize
    {
        get => (float)GetValue(LabelFontSizeProperty);
        set => SetValue(LabelFontSizeProperty, value);
    }

    public static readonly BindableProperty LabelFontSizeProperty = Create(nameof(LabelFontSize),
        typeof(float), 10f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelFontSize = (float)newValue; });

    /// <summary>
    ///     LabelFontWeight
    /// </summary>
    public SKFontStyleWeight LabelFontWeight
    {
        get => (SKFontStyleWeight)GetValue(LabelFontWeightProperty);
        set => SetValue(LabelFontWeightProperty, value);
    }

    public static readonly BindableProperty LabelFontWeightProperty = Create(nameof(LabelFontWeight),
        typeof(SKFontStyleWeight), SKFontStyleWeight.Normal,
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelFontWeight = (SKFontStyleWeight)newValue; });

    /// <summary>
    ///     FontBrush
    /// </summary>
    public Brush LabelFontBrush
    {
        get => (Brush)GetValue(LabelFontBrushProperty);
        set => SetValue(LabelFontBrushProperty, value);
    }

    public static readonly BindableProperty LabelFontBrushProperty = Create(nameof(LabelFontBrush),
        typeof(Brush), new SolidColorBrush(Colors.Black),
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelFontBrush = MauiGaugeHelper.GetGaugeBrush((Brush)newValue); });

    /// <summary>
    ///     LabelFontFamily
    /// </summary>
    public string LabelFontFamily
    {
        get => (string)GetValue(LabelFontFamilyProperty);
        set => SetValue(LabelFontFamilyProperty, value);
    }

    public static readonly BindableProperty LabelFontFamilyProperty = Create(nameof(LabelFontFamily),
        typeof(string), "verdana",
        (gaugeBase, newValue) => { gaugeBase.Gauge.LabelFontFamily = (string)newValue; });

    /// <summary>
    ///     ValueLocation
    /// </summary>
    public GaugeValueLocation ValueLocation
    {
        get => (GaugeValueLocation)GetValue(ValueLocationProperty);
        set => SetValue(ValueLocationProperty, value);
    }

    public static readonly BindableProperty ValueLocationProperty = Create(nameof(ValueLocation),
        typeof(GaugeValueLocation), GaugeValueLocation.BottomCenter,
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueLocation = (GaugeValueLocation)newValue; });

    /// <summary>
    ///     ValueExtent
    /// </summary>
    public float ValueExtent
    {
        get => (float)GetValue(ValueExtentProperty);
        set => SetValue(ValueExtentProperty, value);
    }

    public static readonly BindableProperty ValueExtentProperty = Create(nameof(ValueExtent),
        typeof(float), 0.60f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueExtent = (float)newValue; });

    /// <summary>
    ///     ValueFormatString
    /// </summary>
    public string ValueFormatString
    {
        get => (string)GetValue(ValueFormatStringProperty);
        set => SetValue(ValueFormatStringProperty, value);
    }

    public static readonly BindableProperty ValueFormatStringProperty = Create(nameof(ValueFormatString),
        typeof(string), "{0:N0}",
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueFormatString = (string)newValue; });

    /// <summary>
    ///     ValueFontSize
    /// </summary>
    public float ValueFontSize
    {
        get => (float)GetValue(ValueFontSizeProperty);
        set => SetValue(ValueFontSizeProperty, value);
    }

    public static readonly BindableProperty ValueFontSizeProperty = Create(nameof(ValueFontSize),
        typeof(float), 20f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueFontSize = (float)newValue; });

    /// <summary>
    ///     ValueFontWeight
    /// </summary>
    public SKFontStyleWeight ValueFontWeight
    {
        get => (SKFontStyleWeight)GetValue(ValueFontWeightProperty);
        set => SetValue(ValueFontWeightProperty, value);
    }

    public static readonly BindableProperty ValueFontWeightProperty = Create(nameof(ValueFontWeight),
        typeof(SKFontStyleWeight), SKFontStyleWeight.Normal,
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueFontWeight = (SKFontStyleWeight)newValue; });

    /// <summary>
    ///     ValueFontBrush
    /// </summary>
    public Brush ValueFontBrush
    {
        get => (Brush)GetValue(ValueFontBrushProperty);
        set => SetValue(ValueFontBrushProperty, value);
    }

    public static readonly BindableProperty ValueFontBrushProperty = Create(nameof(ValueFontBrush),
        typeof(Brush), new SolidColorBrush(Colors.Black),
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueFontBrush = MauiGaugeHelper.GetGaugeBrush((Brush)newValue); });

    /// <summary>
    ///     ValueFontFamily
    /// </summary>
    public string ValueFontFamily
    {
        get => (string)GetValue(ValueFontFamilyProperty);
        set => SetValue(ValueFontFamilyProperty, value);
    }

    public static readonly BindableProperty ValueFontFamilyProperty = Create(nameof(ValueFontFamily),
        typeof(string), "verdana",
        (gaugeBase, newValue) => { gaugeBase.Gauge.ValueFontFamily = (string)newValue; });

    /// <summary>
    ///     NeedleBrush
    /// </summary>
    public Brush NeedleBrush
    {
        get => (Brush)GetValue(NeedleBrushProperty);
        set => SetValue(NeedleBrushProperty, value);
    }

    public static readonly BindableProperty NeedleBrushProperty = Create(nameof(NeedleBrush),
        typeof(Brush), new SolidColorBrush(Colors.Black),
        (gaugeBase, newValue) => { gaugeBase.Gauge.NeedleBrush = MauiGaugeHelper.GetGaugeBrush((Brush)newValue); });

    /// <summary>
    ///     NeedleStartWidth
    /// </summary>
    public float NeedleStartWidth
    {
        get => (float)GetValue(NeedleStartWidthProperty);
        set => SetValue(NeedleStartWidthProperty, value);
    }

    public static readonly BindableProperty NeedleStartWidthProperty = Create(nameof(NeedleStartWidth),
        typeof(float), 6f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.NeedleStartWidth = (float)newValue; });

    /// <summary>
    ///     NeedleEndWidth
    /// </summary>
    public float NeedleEndWidth
    {
        get => (float)GetValue(NeedleEndWidthProperty);
        set => SetValue(NeedleEndWidthProperty, value);
    }

    public static readonly BindableProperty NeedleEndWidthProperty = Create(nameof(NeedleEndWidth),
        typeof(float), 3f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.NeedleEndWidth = (float)newValue; });

    /// <summary>
    ///     NeedleStartExtent
    /// </summary>
    public float NeedleStartExtent
    {
        get => (float)GetValue(NeedleStartExtentProperty);
        set => SetValue(NeedleStartExtentProperty, value);
    }

    public static readonly BindableProperty NeedleStartExtentProperty = Create(nameof(NeedleStartExtent),
        typeof(float), 0f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.NeedleStartExtent = (float)newValue; });

    /// <summary>
    ///     NeedleEndExtent
    /// </summary>
    public float NeedleEndExtent
    {
        get => (float)GetValue(NeedleEndExtentProperty);
        set => SetValue(NeedleEndExtentProperty, value);
    }

    public static readonly BindableProperty NeedleEndExtentProperty = Create(nameof(NeedleEndExtent),
        typeof(float), 0.71f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.NeedleEndExtent = (float)newValue; });

    /// <summary>
    ///     NeedleOutlineWidth
    /// </summary>
    public float NeedleOutlineWidth
    {
        get => (float)GetValue(NeedleOutlineWidthProperty);
        set => SetValue(NeedleOutlineWidthProperty, value);
    }

    public static readonly BindableProperty NeedleOutlineWidthProperty = Create(nameof(NeedleOutlineWidth),
        typeof(float), 0.71f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.NeedleOutlineWidth = (float)newValue; });

    /// <summary>
    ///     NeedleOutlineBrush
    /// </summary>
    public Brush NeedleOutlineBrush
    {
        get => (Brush)GetValue(NeedleOutlineBrushProperty);
        set => SetValue(NeedleOutlineBrushProperty, value);
    }

    public static readonly BindableProperty NeedleOutlineBrushProperty = Create(nameof(NeedleOutlineBrush),
        typeof(Brush), new SolidColorBrush(Colors.Transparent),
        (gaugeBase, newValue) =>
        {
            gaugeBase.Gauge.NeedleOutlineBrush = MauiGaugeHelper.GetGaugeBrush((Brush)newValue);
        });

    /// <summary>
    ///     SetValue
    /// </summary>
    public float SetNeedleValue
    {
        get => (float)GetValue(SetNeedleValueProperty);
        set => SetValue(SetNeedleValueProperty, value);
    }

    public static readonly BindableProperty SetNeedleValueProperty = Create(nameof(SetNeedleValue),
        typeof(float), 0f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.SetNeedleValue = (float)newValue; });

    /// <summary>
    ///     SetNeedleBrush
    /// </summary>
    public Brush SetNeedleBrush
    {
        get => (Brush)GetValue(SetNeedleBrushProperty);
        set => SetValue(SetNeedleBrushProperty, value);
    }

    public static readonly BindableProperty SetNeedleBrushProperty = Create(nameof(SetNeedleBrush),
        typeof(Brush), new SolidColorBrush(Colors.Transparent),
        (gaugeBase, newValue) => { gaugeBase.Gauge.SetNeedleBrush = MauiGaugeHelper.GetGaugeBrush((Brush)newValue); });

    /// <summary>
    ///     SetNeedleOutlineWidth
    /// </summary>
    public float SetNeedleOutlineWidth
    {
        get => (float)GetValue(SetNeedleOutlineWidthProperty);
        set => SetValue(SetNeedleOutlineWidthProperty, value);
    }

    public static readonly BindableProperty SetNeedleOutlineWidthProperty = Create(nameof(SetNeedleOutlineWidth),
        typeof(float), 0.71f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.SetNeedleOutlineWidth = (float)newValue; });

    /// <summary>
    ///     SetNeedleOutlineBrush
    /// </summary>
    public Brush SetNeedleOutlineBrush
    {
        get => (Brush)GetValue(SetNeedleOutlineBrushProperty);
        set => SetValue(SetNeedleOutlineBrushProperty, value);
    }

    public static readonly BindableProperty SetNeedleOutlineBrushProperty = Create(nameof(SetNeedleOutlineBrush),
        typeof(Brush), new SolidColorBrush(Colors.Transparent),
        (gaugeBase, newValue) =>
        {
            gaugeBase.Gauge.SetNeedleOutlineBrush = MauiGaugeHelper.GetGaugeBrush((Brush)newValue);
        });

    /// <summary>
    ///     SetNeedleStartWidth
    /// </summary>
    public float SetNeedleStartWidth
    {
        get => (float)GetValue(SetNeedleStartWidthProperty);
        set => SetValue(SetNeedleStartWidthProperty, value);
    }

    public static readonly BindableProperty SetNeedleStartWidthProperty = Create(nameof(SetNeedleStartWidth),
        typeof(float), 6f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.SetNeedleStartWidth = (float)newValue; });

    /// <summary>
    ///     SetNeedleEndWidth
    /// </summary>
    public float SetNeedleEndWidth
    {
        get => (float)GetValue(SetNeedleEndWidthProperty);
        set => SetValue(SetNeedleEndWidthProperty, value);
    }

    public static readonly BindableProperty SetNeedleEndWidthProperty = Create(nameof(SetNeedleEndWidth),
        typeof(float), 3f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.SetNeedleEndWidth = (float)newValue; });

    /// <summary>
    ///     SetNeedleStartExtent
    /// </summary>
    public float SetNeedleStartExtent
    {
        get => (float)GetValue(SetNeedleStartExtentProperty);
        set => SetValue(SetNeedleStartExtentProperty, value);
    }

    public static readonly BindableProperty SetNeedleStartExtentProperty = Create(nameof(SetNeedleStartExtent),
        typeof(float), 0f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.SetNeedleStartExtent = (float)newValue; });

    /// <summary>
    ///     SetNeedleEndExtent
    /// </summary>
    public float SetNeedleEndExtent
    {
        get => (float)GetValue(SetNeedleEndExtentProperty);
        set => SetValue(SetNeedleEndExtentProperty, value);
    }

    public static readonly BindableProperty SetNeedleEndExtentProperty = Create(nameof(SetNeedleEndExtent),
        typeof(float), 0.71f,
        (gaugeBase, newValue) => { gaugeBase.Gauge.SetNeedleEndExtent = (float)newValue; });

    #endregion

    #region Helper

    /// <summary>
    ///     Create - wrapper Register against this control
    /// </summary>
    protected static BindableProperty Create(string propertyName, Type propertyType,
        object defaultValue, GaugePropertyChanged propertyChanged)
    {
        return BindableProperty.Create(propertyName, propertyType, typeof(GaugeBase), defaultValue,
            propertyChanged: (bindObj, _, newValue) =>
            {
                var canvasGauge = (MauiGaugeBase)bindObj;
                propertyChanged(canvasGauge, newValue);
                ReDraw(canvasGauge);
            });
    }

    #endregion
}