using MicroGauge.Constant;
using Microsoft.AspNetCore.Components;

namespace MicroGauge.Blazor;

/// <summary>
///     BlazorGaugeRadial - Radial Gauge with tags and bindings for Blazor
/// </summary>
public class BlazorGaugeRadial : BlazorGaugeBase
{
    #region Constructor

    /// <summary>
    ///     Constructor
    /// </summary>
    public BlazorGaugeRadial()
    {
        Gauge = new GaugeRadial();
    }

    #endregion


    #region Gauge Specific Properties

    /// <summary>
    ///     RadialStyle
    /// </summary>
    [Parameter]
    public GaugeRadialStyle RadialStyle
    {
        get => GetRadial(this).RadialStyle;
        set
        {
            GetRadial(this).RadialStyle = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     ScaleStartAngle
    /// </summary>
    [Parameter]
    public float ScaleStartAngle
    {
        get => GetRadial(this).ScaleStartAngle;
        set
        {
            GetRadial(this).ScaleStartAngle = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     ScaleEndAngle
    /// </summary>
    [Parameter]
    public float ScaleEndAngle
    {
        get => GetRadial(this).ScaleEndAngle;
        set
        {
            GetRadial(this).ScaleEndAngle = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     TickStartExtent
    /// </summary>
    [Parameter]
    public float TickStartExtent
    {
        get => GetRadial(this).TickStartExtent;
        set
        {
            GetRadial(this).TickStartExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     TickEndExtent
    /// </summary>
    [Parameter]
    public float TickEndExtent
    {
        get => GetRadial(this).TickEndExtent;
        set
        {
            GetRadial(this).TickEndExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     MinorTickStartExtent
    /// </summary>
    [Parameter]
    public float MinorTickStartExtent
    {
        get => GetRadial(this).MinorTickStartExtent;
        set
        {
            GetRadial(this).MinorTickStartExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     MinorTickEndExtent
    /// </summary>
    [Parameter]
    public float MinorTickEndExtent
    {
        get => GetRadial(this).MinorTickEndExtent;
        set
        {
            GetRadial(this).MinorTickEndExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     NeedlePivotEndExtent
    /// </summary>
    [Parameter]
    public float NeedlePivotEndExtent
    {
        get => GetRadial(this).NeedlePivotEndExtent;
        set
        {
            GetRadial(this).NeedlePivotEndExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     NeedlePivotBrush
    /// </summary>
    private GaugeBrush _needlePivotBrush = GaugeBrushes.LightGray;

    [Parameter]
    public GaugeBrush NeedlePivotBrush
    {
        get => _needlePivotBrush;
        set
        {
            _needlePivotBrush = value;
            GetRadial(this).NeedlePivotBrush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     NeedlePivotOutline
    /// </summary>
    private GaugeBrush _needlePivotOutlineBrush = GaugeBrushes.Black;

    [Parameter]
    public GaugeBrush NeedlePivotOutlineBrush
    {
        get => _needlePivotOutlineBrush;
        set
        {
            _needlePivotOutlineBrush = value;
            GetRadial(this).NeedlePivotOutlineBrush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     NeedlePivotOutlineWidth
    /// </summary>
    [Parameter]
    public float NeedlePivotOutlineWidth
    {
        get => GetRadial(this).NeedlePivotOutlineWidth;
        set
        {
            GetRadial(this).NeedlePivotOutlineWidth = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     RangeBrush
    /// </summary>
    private GaugeBrush _rangeBrush = GaugeBrushes.Transparent;

    [Parameter]
    public GaugeBrush RangeBrush
    {
        get => _rangeBrush;
        set
        {
            _rangeBrush = value;
            GetRadialRange(this).Brush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     RangeInnerStartExtent
    /// </summary>
    [Parameter]
    public float RangeInnerStartExtent
    {
        get => GetRadialRange(this).InnerStartExtent;
        set
        {
            GetRadialRange(this).InnerStartExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     RangeInnerEndExtent
    /// </summary>
    [Parameter]
    public float RangeInnerEndExtent
    {
        get => GetRadialRange(this).InnerEndExtent;
        set
        {
            GetRadialRange(this).InnerEndExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     RangeOuterStartExtent
    /// </summary>
    [Parameter]
    public float RangeOuterStartExtent
    {
        get => GetRadialRange(this).OuterStartExtent;
        set
        {
            GetRadialRange(this).OuterStartExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     RangeOuterEndExtent
    /// </summary>
    [Parameter]
    public float RangeOuterEndExtent
    {
        get => GetRadialRange(this).OuterEndExtent;
        set
        {
            GetRadialRange(this).OuterEndExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     Ranges
    /// </summary>
    [Parameter]
    public List<GaugeRadialRange> Ranges
    {
        get => GetRadial(this).Ranges;
        set
        {
            GetRadial(this).Ranges = value;
            Invalidate();
        }
    }

    #endregion

    #region Helper

    /// <summary>
    ///     GetRadial - Get Radial Gauge from gauge base
    /// </summary>
    private static GaugeRadial GetRadial(BlazorGaugeBase gaugeBase)
    {
        return (GaugeRadial)gaugeBase.Gauge;
    }

    /// <summary>
    ///     GetRadialRange - Get Radial Range from gauge base (first range)
    /// </summary>
    private static GaugeRadialRange GetRadialRange(BlazorGaugeBase gaugeBase)
    {
        GaugeRadial radialGauge = GetRadial(gaugeBase);
        if (radialGauge.Ranges.Count == 1) return radialGauge.Ranges[0];
        radialGauge.Ranges.Clear();
        radialGauge.Ranges.Add(new GaugeRadialRange());
        return radialGauge.Ranges[0];
    }
    #endregion
}