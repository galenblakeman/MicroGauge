using System.Collections.Specialized;
using Avalonia;
using Avalonia.Media;
using MicroGauge.Constant;

namespace MicroGauge.Avalonia;

public sealed class AvaloniaGaugeLinear : AvaloniaGaugeBase
{
    private GaugeLinear TypedGauge => (GaugeLinear)Gauge;

    public static readonly StyledProperty<bool> IsVerticalProperty =
        AvaloniaProperty.Register<AvaloniaGaugeLinear, bool>(nameof(IsVertical), false);
    public bool IsVertical { get => GetValue(IsVerticalProperty); set => SetValue(IsVerticalProperty, value); }

    public static readonly StyledProperty<float> ValueWidthExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeLinear, float>(nameof(ValueWidthExtent), 0.5f);
    public float ValueWidthExtent { get => GetValue(ValueWidthExtentProperty); set => SetValue(ValueWidthExtentProperty, value); }

    public static readonly StyledProperty<float> TickWidthExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeLinear, float>(nameof(TickWidthExtent), 0.7f);
    public float TickWidthExtent { get => GetValue(TickWidthExtentProperty); set => SetValue(TickWidthExtentProperty, value); }

    public static readonly StyledProperty<float> MinorTickWidthExtentProperty =
        AvaloniaProperty.Register<AvaloniaGaugeLinear, float>(nameof(MinorTickWidthExtent), 0.5f);
    public float MinorTickWidthExtent { get => GetValue(MinorTickWidthExtentProperty); set => SetValue(MinorTickWidthExtentProperty, value); }

    public static readonly StyledProperty<IBrush> ValueBarBrushProperty =
        AvaloniaProperty.Register<AvaloniaGaugeLinear, IBrush>(nameof(ValueBarBrush), Brushes.Black);
    public IBrush ValueBarBrush { get => GetValue(ValueBarBrushProperty); set => SetValue(ValueBarBrushProperty, value); }

    public static readonly StyledProperty<IList<GaugeLinearRange>?> RangesProperty =
        AvaloniaProperty.Register<AvaloniaGaugeLinear, IList<GaugeLinearRange>?>(nameof(Ranges));
    public IList<GaugeLinearRange> Ranges
    {
        get => GetValue(RangesProperty) ?? TypedGauge.Ranges;
        set => SetValue(RangesProperty, value);
    }

    private INotifyCollectionChanged? _observedRanges;

    public AvaloniaGaugeLinear()
    {
        Gauge = new GaugeLinear();
        InitializeGauge();
        UpdateGauge(() =>
        {
            TypedGauge.IsVertical = IsVertical;
            TypedGauge.ValueWidthExtent = ValueWidthExtent;
            TypedGauge.TickWidthExtent = TickWidthExtent;
            TypedGauge.MinorTickWidthExtent = MinorTickWidthExtent;
            TypedGauge.ValueBarBrush = AvaloniaGaugeHelper.GetGaugeBrush(ValueBarBrush);
        });
    }

    protected override void OnGaugePropertyChanged(AvaloniaProperty property)
    {
        if (property == IsVerticalProperty) TypedGauge.IsVertical = IsVertical;
        if (property == ValueWidthExtentProperty) TypedGauge.ValueWidthExtent = ValueWidthExtent;
        if (property == TickWidthExtentProperty) TypedGauge.TickWidthExtent = TickWidthExtent;
        if (property == MinorTickWidthExtentProperty) TypedGauge.MinorTickWidthExtent = MinorTickWidthExtent;
        if (property == ValueBarBrushProperty) TypedGauge.ValueBarBrush = AvaloniaGaugeHelper.GetGaugeBrush(ValueBarBrush);
        if (property == RangesProperty) UpdateRanges();
    }

    private void UpdateRanges()
    {
        if (_observedRanges is not null) _observedRanges.CollectionChanged -= OnRangesChanged;
        var source = GetValue(RangesProperty);
        _observedRanges = source as INotifyCollectionChanged;
        if (_observedRanges is not null) _observedRanges.CollectionChanged += OnRangesChanged;
        TypedGauge.Ranges = source is null ? new List<GaugeLinearRange>() : source.ToList();
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
        TypedGauge.ValueBarBrush = AvaloniaGaugeHelper.GetGaugeBrush(ValueBarBrush);
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
