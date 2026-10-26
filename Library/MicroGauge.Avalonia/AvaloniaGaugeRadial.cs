using System.Collections.Specialized;
using Avalonia;
using Avalonia.Media;
using MicroGauge.Constant;

namespace MicroGauge.Avalonia;

public sealed class AvaloniaGaugeRadial : AvaloniaGaugeBase
{
    private GaugeRadial TypedGauge => (GaugeRadial)Gauge;

    private GaugeRadialRange FirstRange
    {
        get
        {
            if (TypedGauge.Ranges.Count == 0) TypedGauge.Ranges.Add(new GaugeRadialRange());
            return TypedGauge.Ranges[0];
        }
    }

    public static readonly StyledProperty<GaugeRadialStyle> RadialStyleProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, GaugeRadialStyle>(nameof(RadialStyle), GaugeRadialStyle.Full);
    public GaugeRadialStyle RadialStyle { get => GetValue(RadialStyleProperty); set => SetValue(RadialStyleProperty, value); }

    public static readonly StyledProperty<float> ScaleStartAngleProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, float>(nameof(ScaleStartAngle), 210f);
    public float ScaleStartAngle { get => GetValue(ScaleStartAngleProperty); set => SetValue(ScaleStartAngleProperty, value); }

    public static readonly StyledProperty<float> ScaleEndAngleProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, float>(nameof(ScaleEndAngle), 330f);
    public float ScaleEndAngle { get => GetValue(ScaleEndAngleProperty); set => SetValue(ScaleEndAngleProperty, value); }

    public static readonly StyledProperty<float> TickStartExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, float>(nameof(TickStartExtent), 0.6f);
    public float TickStartExtent { get => GetValue(TickStartExtentProperty); set => SetValue(TickStartExtentProperty, value); }

    public static readonly StyledProperty<float> TickEndExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, float>(nameof(TickEndExtent), 0.7f);
    public float TickEndExtent { get => GetValue(TickEndExtentProperty); set => SetValue(TickEndExtentProperty, value); }

    public static readonly StyledProperty<float> MinorTickStartExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, float>(nameof(MinorTickStartExtent), 0.65f);
    public float MinorTickStartExtent { get => GetValue(MinorTickStartExtentProperty); set => SetValue(MinorTickStartExtentProperty, value); }

    public static readonly StyledProperty<float> MinorTickEndExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, float>(nameof(MinorTickEndExtent), 0.7f);
    public float MinorTickEndExtent { get => GetValue(MinorTickEndExtentProperty); set => SetValue(MinorTickEndExtentProperty, value); }

    public static readonly StyledProperty<float> NeedlePivotEndExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, float>(nameof(NeedlePivotEndExtent), 0.1f);
    public float NeedlePivotEndExtent { get => GetValue(NeedlePivotEndExtentProperty); set => SetValue(NeedlePivotEndExtentProperty, value); }

    public static readonly StyledProperty<IBrush> NeedlePivotBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, IBrush>(nameof(NeedlePivotBrush), Brushes.LightGray);
    public IBrush NeedlePivotBrush { get => GetValue(NeedlePivotBrushProperty); set => SetValue(NeedlePivotBrushProperty, value); }

    public static readonly StyledProperty<IBrush> NeedlePivotOutlineBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, IBrush>(nameof(NeedlePivotOutlineBrush), Brushes.Black);
    public IBrush NeedlePivotOutlineBrush { get => GetValue(NeedlePivotOutlineBrushProperty); set => SetValue(NeedlePivotOutlineBrushProperty, value); }

    public static readonly StyledProperty<float> NeedlePivotOutlineWidthProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, float>(nameof(NeedlePivotOutlineWidth), 2f);
    public float NeedlePivotOutlineWidth { get => GetValue(NeedlePivotOutlineWidthProperty); set => SetValue(NeedlePivotOutlineWidthProperty, value); }

    public static readonly StyledProperty<IBrush> RangeBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, IBrush>(nameof(RangeBrush), Brushes.Transparent);
    public IBrush RangeBrush { get => GetValue(RangeBrushProperty); set => SetValue(RangeBrushProperty, value); }

    public static readonly StyledProperty<float> RangeInnerStartExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, float>(nameof(RangeInnerStartExtent), 0f);
    public float RangeInnerStartExtent { get => GetValue(RangeInnerStartExtentProperty); set => SetValue(RangeInnerStartExtentProperty, value); }

    public static readonly StyledProperty<float> RangeInnerEndExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, float>(nameof(RangeInnerEndExtent), 0f);
    public float RangeInnerEndExtent { get => GetValue(RangeInnerEndExtentProperty); set => SetValue(RangeInnerEndExtentProperty, value); }

    public static readonly StyledProperty<float> RangeOuterStartExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, float>(nameof(RangeOuterStartExtent), 0f);
    public float RangeOuterStartExtent { get => GetValue(RangeOuterStartExtentProperty); set => SetValue(RangeOuterStartExtentProperty, value); }

    public static readonly StyledProperty<float> RangeOuterEndExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, float>(nameof(RangeOuterEndExtent), 0f);
    public float RangeOuterEndExtent { get => GetValue(RangeOuterEndExtentProperty); set => SetValue(RangeOuterEndExtentProperty, value); }

    public static readonly StyledProperty<IList<GaugeRadialRange>?> RangesProperty =
        AvaloniaProperty.Register<AvaloniaGaugeRadial, IList<GaugeRadialRange>?>(nameof(Ranges));
    public IList<GaugeRadialRange> Ranges
    {
        get => GetValue(RangesProperty) ?? TypedGauge.Ranges;
        set => SetValue(RangesProperty, value);
    }

    private INotifyCollectionChanged? _observedRanges;

    public AvaloniaGaugeRadial()
    {
        Gauge = new GaugeRadial();
        InitializeGauge();
        UpdateGauge(() =>
        {
            TypedGauge.RadialStyle = RadialStyle;
            TypedGauge.ScaleStartAngle = ScaleStartAngle;
            TypedGauge.ScaleEndAngle = ScaleEndAngle;
            TypedGauge.TickStartExtent = TickStartExtent;
            TypedGauge.TickEndExtent = TickEndExtent;
            TypedGauge.MinorTickStartExtent = MinorTickStartExtent;
            TypedGauge.MinorTickEndExtent = MinorTickEndExtent;
            TypedGauge.NeedlePivotEndExtent = NeedlePivotEndExtent;
            TypedGauge.NeedlePivotBrush = AvaloniaGaugeHelper.GetGaugeBrush(NeedlePivotBrush);
            TypedGauge.NeedlePivotOutlineBrush = AvaloniaGaugeHelper.GetGaugeBrush(NeedlePivotOutlineBrush);
            TypedGauge.NeedlePivotOutlineWidth = NeedlePivotOutlineWidth;
        });
    }

    protected override void OnGaugePropertyChanged(AvaloniaProperty property)
    {
        if (property == RadialStyleProperty) TypedGauge.RadialStyle = RadialStyle;
        if (property == ScaleStartAngleProperty) TypedGauge.ScaleStartAngle = ScaleStartAngle;
        if (property == ScaleEndAngleProperty) TypedGauge.ScaleEndAngle = ScaleEndAngle;
        if (property == TickStartExtentProperty) TypedGauge.TickStartExtent = TickStartExtent;
        if (property == TickEndExtentProperty) TypedGauge.TickEndExtent = TickEndExtent;
        if (property == MinorTickStartExtentProperty) TypedGauge.MinorTickStartExtent = MinorTickStartExtent;
        if (property == MinorTickEndExtentProperty) TypedGauge.MinorTickEndExtent = MinorTickEndExtent;
        if (property == NeedlePivotEndExtentProperty) TypedGauge.NeedlePivotEndExtent = NeedlePivotEndExtent;
        if (property == NeedlePivotBrushProperty) TypedGauge.NeedlePivotBrush = AvaloniaGaugeHelper.GetGaugeBrush(NeedlePivotBrush);
        if (property == NeedlePivotOutlineBrushProperty) TypedGauge.NeedlePivotOutlineBrush = AvaloniaGaugeHelper.GetGaugeBrush(NeedlePivotOutlineBrush);
        if (property == NeedlePivotOutlineWidthProperty) TypedGauge.NeedlePivotOutlineWidth = NeedlePivotOutlineWidth;
        if (property == RangeBrushProperty) FirstRange.Brush = AvaloniaGaugeHelper.GetGaugeBrush(RangeBrush);
        if (property == RangeInnerStartExtentProperty) FirstRange.InnerStartExtent = RangeInnerStartExtent;
        if (property == RangeInnerEndExtentProperty) FirstRange.InnerEndExtent = RangeInnerEndExtent;
        if (property == RangeOuterStartExtentProperty) FirstRange.OuterStartExtent = RangeOuterStartExtent;
        if (property == RangeOuterEndExtentProperty) FirstRange.OuterEndExtent = RangeOuterEndExtent;
        if (property == RangesProperty) UpdateRanges();
    }

    private void UpdateRanges()
    {
        if (_observedRanges is not null) _observedRanges.CollectionChanged -= OnRangesChanged;
        var source = GetValue(RangesProperty);
        _observedRanges = source as INotifyCollectionChanged;
        if (_observedRanges is not null) _observedRanges.CollectionChanged += OnRangesChanged;
        TypedGauge.Ranges = source is null ? new List<GaugeRadialRange>() : source.ToList();
        TypedGauge.InvalidateStaticLayers();
    }

    private void OnRangesChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        UpdateGauge(() =>
        {
            TypedGauge.Ranges = Ranges.ToList();
            TypedGauge.InvalidateStaticLayers();
        });
    }

    protected override void RefreshMutableState()
    {
        base.RefreshMutableState();
        TypedGauge.NeedlePivotBrush = AvaloniaGaugeHelper.GetGaugeBrush(NeedlePivotBrush);
        TypedGauge.NeedlePivotOutlineBrush = AvaloniaGaugeHelper.GetGaugeBrush(NeedlePivotOutlineBrush);
        var source = GetValue(RangesProperty);
        if (source is not null) TypedGauge.Ranges = source.ToList();
    }

    protected override void OnDetachedFromVisualTree(global::Avalonia.VisualTreeAttachmentEventArgs e)
    {
        if (_observedRanges is not null) _observedRanges.CollectionChanged -= OnRangesChanged;
        _observedRanges = null;
        base.OnDetachedFromVisualTree(e);
    }

    protected override void OnAttachedToVisualTree(global::Avalonia.VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (GetValue(RangesProperty) is not null) UpdateGauge(UpdateRanges);
    }
}
