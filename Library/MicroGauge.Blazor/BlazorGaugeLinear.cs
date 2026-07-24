using Microsoft.AspNetCore.Components;

namespace MicroGauge.Blazor;

/// <summary>
///     BlazorGaugeLinear - Linear Gauge with tags and bindings for Blazor
/// </summary>
public class BlazorGaugeLinear : BlazorGaugeBase
{
    #region Constructor

    /// <summary>
    ///     Constructor
    /// </summary>
    public BlazorGaugeLinear()
    {
        Gauge = new GaugeLinear();
    }

    #endregion


    #region Gauge Specific Properties

    /// <summary>
    ///     IsVertical
    /// </summary>
    [Parameter]
    public bool IsVertical
    {
        get => GetLinear(this).IsVertical;
        set
        {
            GetLinear(this).IsVertical = value;
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     ValueWidthExtent
    /// </summary>
    [Parameter]
    public float ValueWidthExtent
    {
        get => GetLinear(this).ValueWidthExtent;
        set
        {
            GetLinear(this).ValueWidthExtent = value;
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     TickWidthExtent
    /// </summary>
    [Parameter]
    public float TickWidthExtent
    {
        get => GetLinear(this).TickWidthExtent;
        set
        {
            GetLinear(this).TickWidthExtent = value;
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     MinorTickWidthExtent
    /// </summary>
    [Parameter]
    public float MinorTickWidthExtent
    {
        get => GetLinear(this).MinorTickWidthExtent;
        set
        {
            GetLinear(this).MinorTickWidthExtent = value;
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     ValueBarBrush
    /// </summary>
    private GaugeBrush _valueBarBrush = GaugeBrushes.LightGray;

    [Parameter]
    public GaugeBrush ValueBarBrush
    {
        get => _valueBarBrush;
        set
        {
            _valueBarBrush = value;
            GetLinear(this).ValueBarBrush = value;
            InvalidateGauge();
        }
    }

    #endregion

    #region Helper

    /// <summary>
    ///     GaugeLinear - Get Linear Gauge from gauge base
    /// </summary>
    private static GaugeLinear GetLinear(BlazorGaugeBase gaugeBase)
    {
        return (GaugeLinear)gaugeBase.Gauge;
    }

    #endregion
}