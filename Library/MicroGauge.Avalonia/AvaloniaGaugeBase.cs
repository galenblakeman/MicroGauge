using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Threading;
using MicroGauge.Constant;
using SkiaSharp;

namespace MicroGauge.Avalonia;

public abstract class AvaloniaGaugeBase : Control
{
    private readonly object _renderGate = new();
    private DispatcherTimer? _animationTimer;
    private DateTime _animationStart;
    private double _animationFrom;
    private double _animationTarget;
    private SKImage? _ownedImage;
    private bool _initialized;

    public GaugeBase Gauge { get; protected init; } = null!;

    public static readonly StyledProperty<float> BottomExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(BottomExtent), 0.05f);
    public float BottomExtent { get => GetValue(BottomExtentProperty); set => SetValue(BottomExtentProperty, value); }

    public static readonly StyledProperty<float> TopExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(TopExtent), 0.05f);
    public float TopExtent { get => GetValue(TopExtentProperty); set => SetValue(TopExtentProperty, value); }

    public static readonly StyledProperty<float> LeftExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(LeftExtent), 0.05f);
    public float LeftExtent { get => GetValue(LeftExtentProperty); set => SetValue(LeftExtentProperty, value); }

    public static readonly StyledProperty<float> RightExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(RightExtent), 0.05f);
    public float RightExtent { get => GetValue(RightExtentProperty); set => SetValue(RightExtentProperty, value); }

    public static readonly StyledProperty<double> ValueProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, double>(nameof(Value), 0d);
    public double Value { get => GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

    public static readonly StyledProperty<double> NeedleAnimationDurationProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, double>(nameof(NeedleAnimationDuration), 0d);
    public double NeedleAnimationDuration { get => GetValue(NeedleAnimationDurationProperty); set => SetValue(NeedleAnimationDurationProperty, value); }

    public static readonly StyledProperty<IBrush> BackingBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, IBrush>(nameof(BackingBrush), Brushes.White);
    public IBrush BackingBrush { get => GetValue(BackingBrushProperty); set => SetValue(BackingBrushProperty, value); }

    public static readonly StyledProperty<IBrush> BackingOutlineBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, IBrush>(nameof(BackingOutlineBrush), Brushes.Black);
    public IBrush BackingOutlineBrush { get => GetValue(BackingOutlineBrushProperty); set => SetValue(BackingOutlineBrushProperty, value); }

    public static readonly StyledProperty<float> BackingStrokeWidthProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(BackingStrokeWidth), 10f);
    public float BackingStrokeWidth { get => GetValue(BackingStrokeWidthProperty); set => SetValue(BackingStrokeWidthProperty, value); }

    public static readonly StyledProperty<IBrush> TickBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, IBrush>(nameof(TickBrush), Brushes.Black);
    public IBrush TickBrush { get => GetValue(TickBrushProperty); set => SetValue(TickBrushProperty, value); }

    public static readonly StyledProperty<float> TickStrokeWidthProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(TickStrokeWidth), 1f);
    public float TickStrokeWidth { get => GetValue(TickStrokeWidthProperty); set => SetValue(TickStrokeWidthProperty, value); }

    public static readonly StyledProperty<IBrush> MinorTickBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, IBrush>(nameof(MinorTickBrush), Brushes.LightGray);
    public IBrush MinorTickBrush { get => GetValue(MinorTickBrushProperty); set => SetValue(MinorTickBrushProperty, value); }

    public static readonly StyledProperty<float> MinorTickStrokeWidthProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(MinorTickStrokeWidth), 0.5f);
    public float MinorTickStrokeWidth { get => GetValue(MinorTickStrokeWidthProperty); set => SetValue(MinorTickStrokeWidthProperty, value); }

    public static readonly StyledProperty<float> TickIntervalProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(TickInterval), 5f);
    public float TickInterval { get => GetValue(TickIntervalProperty); set => SetValue(TickIntervalProperty, value); }

    public static readonly StyledProperty<float> MinorTickIntervalProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(MinorTickInterval), 1f);
    public float MinorTickInterval { get => GetValue(MinorTickIntervalProperty); set => SetValue(MinorTickIntervalProperty, value); }

    public static readonly StyledProperty<float> MinValueProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(MinValue), 0f);
    public float MinValue { get => GetValue(MinValueProperty); set => SetValue(MinValueProperty, value); }

    public static readonly StyledProperty<float> MaxValueProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(MaxValue), 100f);
    public float MaxValue { get => GetValue(MaxValueProperty); set => SetValue(MaxValueProperty, value); }

    public static readonly StyledProperty<float> LabelIntervalProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(LabelInterval), 10f);
    public float LabelInterval { get => GetValue(LabelIntervalProperty); set => SetValue(LabelIntervalProperty, value); }

    public static readonly StyledProperty<float> LabelExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(LabelExtent), 0.85f);
    public float LabelExtent { get => GetValue(LabelExtentProperty); set => SetValue(LabelExtentProperty, value); }

    public static readonly StyledProperty<string> LabelFormatStringProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, string>(nameof(LabelFormatString), "{0:N0}");
    public string LabelFormatString { get => GetValue(LabelFormatStringProperty); set => SetValue(LabelFormatStringProperty, value); }

    public static readonly StyledProperty<float> LabelFontSizeProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(LabelFontSize), 10f);
    public float LabelFontSize { get => GetValue(LabelFontSizeProperty); set => SetValue(LabelFontSizeProperty, value); }

    public static readonly StyledProperty<SKFontStyleWeight> LabelFontWeightProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, SKFontStyleWeight>(nameof(LabelFontWeight), SKFontStyleWeight.Normal);
    public SKFontStyleWeight LabelFontWeight { get => GetValue(LabelFontWeightProperty); set => SetValue(LabelFontWeightProperty, value); }

    public static readonly StyledProperty<IBrush> LabelFontBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, IBrush>(nameof(LabelFontBrush), Brushes.Black);
    public IBrush LabelFontBrush { get => GetValue(LabelFontBrushProperty); set => SetValue(LabelFontBrushProperty, value); }

    public static readonly StyledProperty<string> LabelFontFamilyProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, string>(nameof(LabelFontFamily), "verdana");
    public string LabelFontFamily { get => GetValue(LabelFontFamilyProperty); set => SetValue(LabelFontFamilyProperty, value); }

    public static readonly StyledProperty<GaugeValueLocation> ValueLocationProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, GaugeValueLocation>(nameof(ValueLocation), GaugeValueLocation.BottomCenter);
    public GaugeValueLocation ValueLocation { get => GetValue(ValueLocationProperty); set => SetValue(ValueLocationProperty, value); }

    public static readonly StyledProperty<float> ValueExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(ValueExtent), 0.60f);
    public float ValueExtent { get => GetValue(ValueExtentProperty); set => SetValue(ValueExtentProperty, value); }

    public static readonly StyledProperty<string> ValueFormatStringProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, string>(nameof(ValueFormatString), "{0:N0}");
    public string ValueFormatString { get => GetValue(ValueFormatStringProperty); set => SetValue(ValueFormatStringProperty, value); }

    public static readonly StyledProperty<float> ValueFontSizeProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(ValueFontSize), 20f);
    public float ValueFontSize { get => GetValue(ValueFontSizeProperty); set => SetValue(ValueFontSizeProperty, value); }

    public static readonly StyledProperty<SKFontStyleWeight> ValueFontWeightProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, SKFontStyleWeight>(nameof(ValueFontWeight), SKFontStyleWeight.Normal);
    public SKFontStyleWeight ValueFontWeight { get => GetValue(ValueFontWeightProperty); set => SetValue(ValueFontWeightProperty, value); }

    public static readonly StyledProperty<IBrush> ValueFontBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, IBrush>(nameof(ValueFontBrush), Brushes.Black);
    public IBrush ValueFontBrush { get => GetValue(ValueFontBrushProperty); set => SetValue(ValueFontBrushProperty, value); }

    public static readonly StyledProperty<string> ValueFontFamilyProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, string>(nameof(ValueFontFamily), "verdana");
    public string ValueFontFamily { get => GetValue(ValueFontFamilyProperty); set => SetValue(ValueFontFamilyProperty, value); }

    public static readonly StyledProperty<IBrush> NeedleBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, IBrush>(nameof(NeedleBrush), Brushes.Black);
    public IBrush NeedleBrush { get => GetValue(NeedleBrushProperty); set => SetValue(NeedleBrushProperty, value); }

    public static readonly StyledProperty<float> NeedleStartWidthProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(NeedleStartWidth), 6f);
    public float NeedleStartWidth { get => GetValue(NeedleStartWidthProperty); set => SetValue(NeedleStartWidthProperty, value); }

    public static readonly StyledProperty<float> NeedleEndWidthProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(NeedleEndWidth), 3f);
    public float NeedleEndWidth { get => GetValue(NeedleEndWidthProperty); set => SetValue(NeedleEndWidthProperty, value); }

    public static readonly StyledProperty<float> NeedleStartExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(NeedleStartExtent), 0f);
    public float NeedleStartExtent { get => GetValue(NeedleStartExtentProperty); set => SetValue(NeedleStartExtentProperty, value); }

    public static readonly StyledProperty<float> NeedleEndExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(NeedleEndExtent), 0.71f);
    public float NeedleEndExtent { get => GetValue(NeedleEndExtentProperty); set => SetValue(NeedleEndExtentProperty, value); }

    public static readonly StyledProperty<float> NeedleOutlineWidthProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(NeedleOutlineWidth), 0.71f);
    public float NeedleOutlineWidth { get => GetValue(NeedleOutlineWidthProperty); set => SetValue(NeedleOutlineWidthProperty, value); }

    public static readonly StyledProperty<IBrush> NeedleOutlineBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, IBrush>(nameof(NeedleOutlineBrush), Brushes.Transparent);
    public IBrush NeedleOutlineBrush { get => GetValue(NeedleOutlineBrushProperty); set => SetValue(NeedleOutlineBrushProperty, value); }

    public static readonly StyledProperty<float> SetNeedleValueProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(SetNeedleValue), 0f);
    public float SetNeedleValue { get => GetValue(SetNeedleValueProperty); set => SetValue(SetNeedleValueProperty, value); }

    public static readonly StyledProperty<float> SetNeedleOutlineWidthProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(SetNeedleOutlineWidth), 0.71f);
    public float SetNeedleOutlineWidth { get => GetValue(SetNeedleOutlineWidthProperty); set => SetValue(SetNeedleOutlineWidthProperty, value); }

    public static readonly StyledProperty<IBrush> SetNeedleBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, IBrush>(nameof(SetNeedleBrush), Brushes.Transparent);
    public IBrush SetNeedleBrush { get => GetValue(SetNeedleBrushProperty); set => SetValue(SetNeedleBrushProperty, value); }

    public static readonly StyledProperty<IBrush> SetNeedleOutlineBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, IBrush>(nameof(SetNeedleOutlineBrush), Brushes.Transparent);
    public IBrush SetNeedleOutlineBrush { get => GetValue(SetNeedleOutlineBrushProperty); set => SetValue(SetNeedleOutlineBrushProperty, value); }

    public static readonly StyledProperty<float> SetNeedleStartWidthProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(SetNeedleStartWidth), 6f);
    public float SetNeedleStartWidth { get => GetValue(SetNeedleStartWidthProperty); set => SetValue(SetNeedleStartWidthProperty, value); }

    public static readonly StyledProperty<float> SetNeedleEndWidthProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(SetNeedleEndWidth), 3f);
    public float SetNeedleEndWidth { get => GetValue(SetNeedleEndWidthProperty); set => SetValue(SetNeedleEndWidthProperty, value); }

    public static readonly StyledProperty<float> SetNeedleStartExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(SetNeedleStartExtent), 0f);
    public float SetNeedleStartExtent { get => GetValue(SetNeedleStartExtentProperty); set => SetValue(SetNeedleStartExtentProperty, value); }

    public static readonly StyledProperty<float> SetNeedleEndExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(SetNeedleEndExtent), 0.71f);
    public float SetNeedleEndExtent { get => GetValue(SetNeedleEndExtentProperty); set => SetValue(SetNeedleEndExtentProperty, value); }

    public static readonly StyledProperty<float> BackgroundImageOpacityProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, float>(nameof(BackgroundImageOpacity), 1f);
    public float BackgroundImageOpacity { get => GetValue(BackgroundImageOpacityProperty); set => SetValue(BackgroundImageOpacityProperty, value); }

    public static readonly StyledProperty<double> BackgroundImageRotationProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, double>(nameof(BackgroundImageRotation), 0d);
    public double BackgroundImageRotation { get => GetValue(BackgroundImageRotationProperty); set => SetValue(BackgroundImageRotationProperty, value); }

    public static readonly StyledProperty<bool> EnableStaticCachingProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, bool>(nameof(EnableStaticCaching), true);
    public bool EnableStaticCaching { get => GetValue(EnableStaticCachingProperty); set => SetValue(EnableStaticCachingProperty, value); }

    public static readonly StyledProperty<Bitmap?> BackgroundImageSourceProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, Bitmap?>(nameof(BackgroundImageSource));
    public Bitmap? BackgroundImageSource { get => GetValue(BackgroundImageSourceProperty); set => SetValue(BackgroundImageSourceProperty, value); }

    public static readonly StyledProperty<string?> BackgroundImagePathProperty =
        AvaloniaProperty.Register<AvaloniaGaugeBase, string?>(nameof(BackgroundImagePath));
    public string? BackgroundImagePath { get => GetValue(BackgroundImagePathProperty); set => SetValue(BackgroundImagePathProperty, value); }

    /// <summary>Caller retains ownership of a programmatically supplied image.</summary>
    public SKImage? BackgroundImage
    {
        get => Gauge.BackgroundImage;
        set
        {
            lock (_renderGate)
            {
                _ownedImage?.Dispose();
                _ownedImage = null;
                Gauge.BackgroundImage = value!;
                Gauge.InvalidateStaticLayers();
            }
            InvalidateVisual();
        }
    }

    protected AvaloniaGaugeBase()
    {
        ClipToBounds = true;
    }

    /// <summary>Call after a derived constructor assigns Gauge.</summary>
    protected void InitializeGauge()
    {
        lock (_renderGate)
        {
        Gauge.BottomExtent = BottomExtent;
        Gauge.TopExtent = TopExtent;
        Gauge.LeftExtent = LeftExtent;
        Gauge.RightExtent = RightExtent;
        Gauge.BackingBrush = AvaloniaGaugeHelper.GetGaugeBrush(BackingBrush);
        Gauge.BackingOutlineBrush = AvaloniaGaugeHelper.GetGaugeBrush(BackingOutlineBrush);
        Gauge.BackingStrokeWidth = BackingStrokeWidth;
        Gauge.TickBrush = AvaloniaGaugeHelper.GetGaugeBrush(TickBrush);
        Gauge.TickStrokeWidth = TickStrokeWidth;
        Gauge.MinorTickBrush = AvaloniaGaugeHelper.GetGaugeBrush(MinorTickBrush);
        Gauge.MinorTickStrokeWidth = MinorTickStrokeWidth;
        Gauge.TickInterval = TickInterval;
        Gauge.MinorTickInterval = MinorTickInterval;
        Gauge.MinValue = MinValue;
        Gauge.MaxValue = MaxValue;
        Gauge.LabelInterval = LabelInterval;
        Gauge.LabelExtent = LabelExtent;
        Gauge.LabelFormatString = LabelFormatString;
        Gauge.LabelFontSize = LabelFontSize;
        Gauge.LabelFontWeight = LabelFontWeight;
        Gauge.LabelFontBrush = AvaloniaGaugeHelper.GetGaugeBrush(LabelFontBrush);
        Gauge.LabelFontFamily = LabelFontFamily;
        Gauge.ValueLocation = ValueLocation;
        Gauge.ValueExtent = ValueExtent;
        Gauge.ValueFormatString = ValueFormatString;
        Gauge.ValueFontSize = ValueFontSize;
        Gauge.ValueFontWeight = ValueFontWeight;
        Gauge.ValueFontBrush = AvaloniaGaugeHelper.GetGaugeBrush(ValueFontBrush);
        Gauge.ValueFontFamily = ValueFontFamily;
        Gauge.NeedleBrush = AvaloniaGaugeHelper.GetGaugeBrush(NeedleBrush);
        Gauge.NeedleStartWidth = NeedleStartWidth;
        Gauge.NeedleEndWidth = NeedleEndWidth;
        Gauge.NeedleStartExtent = NeedleStartExtent;
        Gauge.NeedleEndExtent = NeedleEndExtent;
        Gauge.NeedleOutlineWidth = NeedleOutlineWidth;
        Gauge.NeedleOutlineBrush = AvaloniaGaugeHelper.GetGaugeBrush(NeedleOutlineBrush);
        Gauge.SetNeedleValue = SetNeedleValue;
        Gauge.SetNeedleOutlineWidth = SetNeedleOutlineWidth;
        Gauge.SetNeedleBrush = AvaloniaGaugeHelper.GetGaugeBrush(SetNeedleBrush);
        Gauge.SetNeedleOutlineBrush = AvaloniaGaugeHelper.GetGaugeBrush(SetNeedleOutlineBrush);
        Gauge.SetNeedleStartWidth = SetNeedleStartWidth;
        Gauge.SetNeedleEndWidth = SetNeedleEndWidth;
        Gauge.SetNeedleStartExtent = SetNeedleStartExtent;
        Gauge.SetNeedleEndExtent = SetNeedleEndExtent;
        Gauge.BackgroundImageOpacity = BackgroundImageOpacity;
        Gauge.BackgroundImageRotation = BackgroundImageRotation;
        Gauge.EnableStaticCaching = EnableStaticCaching;
            Gauge.Value = Value;
            _initialized = true;
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (!_initialized) return;
        if (change.Property == ValueProperty)
        {
            AnimateValue(Value);
            return;
        }
        if (change.Property == NeedleAnimationDurationProperty) return;
        if (change.Property == BackgroundImageSourceProperty || change.Property == BackgroundImagePathProperty)
        {
            LoadBackgroundImage();
            return;
        }
        lock (_renderGate)
        {
            if (change.Property == BottomExtentProperty) Gauge.BottomExtent = BottomExtent;
            if (change.Property == TopExtentProperty) Gauge.TopExtent = TopExtent;
            if (change.Property == LeftExtentProperty) Gauge.LeftExtent = LeftExtent;
            if (change.Property == RightExtentProperty) Gauge.RightExtent = RightExtent;
            if (change.Property == BackingBrushProperty) Gauge.BackingBrush = AvaloniaGaugeHelper.GetGaugeBrush(BackingBrush);
            if (change.Property == BackingOutlineBrushProperty) Gauge.BackingOutlineBrush = AvaloniaGaugeHelper.GetGaugeBrush(BackingOutlineBrush);
            if (change.Property == BackingStrokeWidthProperty) Gauge.BackingStrokeWidth = BackingStrokeWidth;
            if (change.Property == TickBrushProperty) Gauge.TickBrush = AvaloniaGaugeHelper.GetGaugeBrush(TickBrush);
            if (change.Property == TickStrokeWidthProperty) Gauge.TickStrokeWidth = TickStrokeWidth;
            if (change.Property == MinorTickBrushProperty) Gauge.MinorTickBrush = AvaloniaGaugeHelper.GetGaugeBrush(MinorTickBrush);
            if (change.Property == MinorTickStrokeWidthProperty) Gauge.MinorTickStrokeWidth = MinorTickStrokeWidth;
            if (change.Property == TickIntervalProperty) Gauge.TickInterval = TickInterval;
            if (change.Property == MinorTickIntervalProperty) Gauge.MinorTickInterval = MinorTickInterval;
            if (change.Property == MinValueProperty) Gauge.MinValue = MinValue;
            if (change.Property == MaxValueProperty) Gauge.MaxValue = MaxValue;
            if (change.Property == LabelIntervalProperty) Gauge.LabelInterval = LabelInterval;
            if (change.Property == LabelExtentProperty) Gauge.LabelExtent = LabelExtent;
            if (change.Property == LabelFormatStringProperty) Gauge.LabelFormatString = LabelFormatString;
            if (change.Property == LabelFontSizeProperty) Gauge.LabelFontSize = LabelFontSize;
            if (change.Property == LabelFontWeightProperty) Gauge.LabelFontWeight = LabelFontWeight;
            if (change.Property == LabelFontBrushProperty) Gauge.LabelFontBrush = AvaloniaGaugeHelper.GetGaugeBrush(LabelFontBrush);
            if (change.Property == LabelFontFamilyProperty) Gauge.LabelFontFamily = LabelFontFamily;
            if (change.Property == ValueLocationProperty) Gauge.ValueLocation = ValueLocation;
            if (change.Property == ValueExtentProperty) Gauge.ValueExtent = ValueExtent;
            if (change.Property == ValueFormatStringProperty) Gauge.ValueFormatString = ValueFormatString;
            if (change.Property == ValueFontSizeProperty) Gauge.ValueFontSize = ValueFontSize;
            if (change.Property == ValueFontWeightProperty) Gauge.ValueFontWeight = ValueFontWeight;
            if (change.Property == ValueFontBrushProperty) Gauge.ValueFontBrush = AvaloniaGaugeHelper.GetGaugeBrush(ValueFontBrush);
            if (change.Property == ValueFontFamilyProperty) Gauge.ValueFontFamily = ValueFontFamily;
            if (change.Property == NeedleBrushProperty) Gauge.NeedleBrush = AvaloniaGaugeHelper.GetGaugeBrush(NeedleBrush);
            if (change.Property == NeedleStartWidthProperty) Gauge.NeedleStartWidth = NeedleStartWidth;
            if (change.Property == NeedleEndWidthProperty) Gauge.NeedleEndWidth = NeedleEndWidth;
            if (change.Property == NeedleStartExtentProperty) Gauge.NeedleStartExtent = NeedleStartExtent;
            if (change.Property == NeedleEndExtentProperty) Gauge.NeedleEndExtent = NeedleEndExtent;
            if (change.Property == NeedleOutlineWidthProperty) Gauge.NeedleOutlineWidth = NeedleOutlineWidth;
            if (change.Property == NeedleOutlineBrushProperty) Gauge.NeedleOutlineBrush = AvaloniaGaugeHelper.GetGaugeBrush(NeedleOutlineBrush);
            if (change.Property == SetNeedleValueProperty) Gauge.SetNeedleValue = SetNeedleValue;
            if (change.Property == SetNeedleOutlineWidthProperty) Gauge.SetNeedleOutlineWidth = SetNeedleOutlineWidth;
            if (change.Property == SetNeedleBrushProperty) Gauge.SetNeedleBrush = AvaloniaGaugeHelper.GetGaugeBrush(SetNeedleBrush);
            if (change.Property == SetNeedleOutlineBrushProperty) Gauge.SetNeedleOutlineBrush = AvaloniaGaugeHelper.GetGaugeBrush(SetNeedleOutlineBrush);
            if (change.Property == SetNeedleStartWidthProperty) Gauge.SetNeedleStartWidth = SetNeedleStartWidth;
            if (change.Property == SetNeedleEndWidthProperty) Gauge.SetNeedleEndWidth = SetNeedleEndWidth;
            if (change.Property == SetNeedleStartExtentProperty) Gauge.SetNeedleStartExtent = SetNeedleStartExtent;
            if (change.Property == SetNeedleEndExtentProperty) Gauge.SetNeedleEndExtent = SetNeedleEndExtent;
            if (change.Property == BackgroundImageOpacityProperty) Gauge.BackgroundImageOpacity = BackgroundImageOpacity;
            if (change.Property == BackgroundImageRotationProperty) Gauge.BackgroundImageRotation = BackgroundImageRotation;
            if (change.Property == EnableStaticCachingProperty) Gauge.EnableStaticCaching = EnableStaticCaching;
            OnGaugePropertyChanged(change.Property);
        }
        InvalidateVisual();
    }

    /// <summary>Derived controls apply their own styled properties here.</summary>
    protected virtual void OnGaugePropertyChanged(AvaloniaProperty property) { }

    /// <summary>Apply all derived property defaults after Gauge has been assigned.</summary>
    protected void UpdateGauge(Action update)
    {
        lock (_renderGate) update();
        InvalidateVisual();
    }

    /// <summary>Refresh after editing a mutable range or brush in place.</summary>
    public void InvalidateGauge()
    {
        lock (_renderGate)
        {
            RefreshMutableState();
            Gauge.InvalidateStaticLayers();
        }
        InvalidateVisual();
    }

    protected virtual void RefreshMutableState()
    {
        Gauge.BackingBrush = AvaloniaGaugeHelper.GetGaugeBrush(BackingBrush);
        Gauge.BackingOutlineBrush = AvaloniaGaugeHelper.GetGaugeBrush(BackingOutlineBrush);
        Gauge.TickBrush = AvaloniaGaugeHelper.GetGaugeBrush(TickBrush);
        Gauge.MinorTickBrush = AvaloniaGaugeHelper.GetGaugeBrush(MinorTickBrush);
        Gauge.LabelFontBrush = AvaloniaGaugeHelper.GetGaugeBrush(LabelFontBrush);
        Gauge.ValueFontBrush = AvaloniaGaugeHelper.GetGaugeBrush(ValueFontBrush);
        Gauge.NeedleBrush = AvaloniaGaugeHelper.GetGaugeBrush(NeedleBrush);
        Gauge.NeedleOutlineBrush = AvaloniaGaugeHelper.GetGaugeBrush(NeedleOutlineBrush);
        Gauge.SetNeedleBrush = AvaloniaGaugeHelper.GetGaugeBrush(SetNeedleBrush);
        Gauge.SetNeedleOutlineBrush = AvaloniaGaugeHelper.GetGaugeBrush(SetNeedleOutlineBrush);
    }

    private void AnimateValue(double target)
    {
        _animationTimer?.Stop();
        var duration = NeedleAnimationDuration;
        if (duration <= 0 || VisualRoot is null)
        {
            UpdateGauge(() => Gauge.Value = target);
            return;
        }

        lock (_renderGate) _animationFrom = Gauge.Value;
        _animationTarget = target;
        _animationStart = DateTime.UtcNow;
        _animationTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16) };
        _animationTimer.Tick += (_, _) =>
        {
            var t = Math.Min(1d, (DateTime.UtcNow - _animationStart).TotalMilliseconds / duration);
            var eased = 1 - Math.Pow(1 - t, 3);
            UpdateGauge(() => Gauge.Value = _animationFrom + (_animationTarget - _animationFrom) * eased);
            if (t >= 1) _animationTimer?.Stop();
        };
        _animationTimer.Start();
    }

    private void LoadBackgroundImage()
    {
        var next = BackgroundImageSource is { } source
            ? AvaloniaGaugeHelper.GetSkImage(source)
            : AvaloniaGaugeHelper.GetSkImageFromPath(BackgroundImagePath);
        lock (_renderGate)
        {
            var previous = _ownedImage;
            _ownedImage = next;
            Gauge.BackgroundImage = next!;
            Gauge.InvalidateStaticLayers();
            previous?.Dispose();
        }
        InvalidateVisual();
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);
        if (Bounds.Width <= 0 || Bounds.Height <= 0) return;
        var scale = TopLevel.GetTopLevel(this)?.RenderScaling ?? 1d;
        context.Custom(new GaugeDrawOperation(this, new Rect(Bounds.Size), scale));
    }

    private sealed class GaugeDrawOperation(AvaloniaGaugeBase owner, Rect bounds, double scale) : ICustomDrawOperation
    {
        public Rect Bounds => bounds;
        public bool HitTest(Point p) => Bounds.Contains(p);
        public bool Equals(ICustomDrawOperation? other) => false;
        public void Dispose() { }

        public void Render(ImmediateDrawingContext context)
        {
            var feature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (feature is null) return;
            var width = (int)Math.Ceiling(bounds.Width * scale);
            var height = (int)Math.Ceiling(bounds.Height * scale);
            if (width <= 0 || height <= 0) return;
            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            if (surface is null) return;
            lock (owner._renderGate)
            {
                try
                {
                    owner.Gauge.Canvas = surface.Canvas;
                    owner.Gauge.SurfaceWidth = width;
                    owner.Gauge.SurfaceHeight = height;
                    owner.Gauge.DrawContent();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    surface.Canvas.Clear(SKColors.Transparent);
                }
                finally
                {
                    owner.Gauge.Canvas = null!;
                }
            }
            using var image = surface.Snapshot();
            using var lease = feature.Lease();
            lease.SkCanvas.DrawImage(image, new SKRect(0, 0, (float)bounds.Width, (float)bounds.Height),
                new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.None));
        }
    }

    protected override void OnDetachedFromVisualTree(global::Avalonia.VisualTreeAttachmentEventArgs e)
    {
        _animationTimer?.Stop();
        lock (_renderGate)
        {
            Gauge.Dispose();
            if (_ownedImage is not null)
            {
                Gauge.BackgroundImage = null!;
                _ownedImage.Dispose();
                _ownedImage = null;
            }
        }
        base.OnDetachedFromVisualTree(e);
    }

    protected override void OnAttachedToVisualTree(global::Avalonia.VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (BackgroundImageSource is not null || !string.IsNullOrWhiteSpace(BackgroundImagePath))
            LoadBackgroundImage();
        UpdateGauge(() => Gauge.Value = Value);
    }
}
