using System.Diagnostics;
using MicroGauge.Constant;
using Microsoft.AspNetCore.Components;
using SkiaSharp;
using SkiaSharp.Views.Blazor;

// ReSharper disable SuggestBaseTypeForParameter


namespace MicroGauge.Blazor;

public abstract class BlazorGaugeBase : SKGLView
{
    public GaugeBase Gauge { get; protected init; } = null!;

    protected BlazorGaugeBase()
    {
        OnPaintSurface += DrawContent;
    }


    #region Draw

    /// <summary>
    ///     DrawContent - Call draw content
    /// </summary>
    private void DrawContent(SKPaintGLSurfaceEventArgs e)
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

    #endregion


    #region Base Properties

    /// <summary>
    ///     BottomExtent
    /// </summary>
    [Parameter]
    public float BottomExtent
    {
        get => Gauge.BottomExtent;
        set
        {
            Gauge.BottomExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     TopExtent
    /// </summary>
    [Parameter]
    public float TopExtent
    {
        get => Gauge.TopExtent;
        set
        {
            Gauge.TopExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     LeftExtent
    /// </summary>
    [Parameter]
    public float LeftExtent
    {
        get => Gauge.LeftExtent;
        set
        {
            Gauge.LeftExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     RightExtent
    /// </summary>
    [Parameter]
    public float RightExtent
    {
        get => Gauge.RightExtent;
        set
        {
            Gauge.RightExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     Value
    /// </summary>
    [Parameter]
    public double Value
    {
        get => Gauge.Value;
        set
        {
            Gauge.Value = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     BackingBrush
    /// </summary>
    private GaugeBrush _backingBrush = new(SKColors.White);

    [Parameter]
    public GaugeBrush BackingBrush
    {
        get => _backingBrush;
        set
        {
            _backingBrush = value;
            Gauge.BackingBrush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     BackingOutlineBrush
    /// </summary>
    private GaugeBrush _backingOutlineBrush = new(SKColors.Black);

    [Parameter]
    public GaugeBrush BackingOutlineBrush
    {
        get => _backingOutlineBrush;
        set
        {
            _backingOutlineBrush = value;
            Gauge.BackingOutlineBrush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     BackingStrokeWidth
    /// </summary>
    [Parameter]
    public float BackingStrokeWidth
    {
        get => Gauge.BackingStrokeWidth;
        set
        {
            Gauge.BackingStrokeWidth = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     TickBrush
    /// </summary>
    private GaugeBrush _tickBrush = new(SKColors.Black);

    [Parameter]
    public GaugeBrush TickBrush
    {
        get => _tickBrush;
        set
        {
            _tickBrush = value;
            Gauge.TickBrush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     TickStrokeWidth
    /// </summary>
    [Parameter]
    public float TickStrokeWidth
    {
        get => Gauge.TickStrokeWidth;
        set
        {
            Gauge.TickStrokeWidth = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     MinorTickBrush
    /// </summary>
    private GaugeBrush _minorTickBrush = new(SKColors.LightGray);

    [Parameter]
    public GaugeBrush MinorTickBrush
    {
        get => _minorTickBrush;
        set
        {
            _minorTickBrush = value;
            Gauge.MinorTickBrush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     MinorTickStrokeWidth
    /// </summary>
    [Parameter]
    public float MinorTickStrokeWidth
    {
        get => Gauge.MinorTickStrokeWidth;
        set
        {
            Gauge.MinorTickStrokeWidth = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     TickInterval
    /// </summary>
    [Parameter]
    public float TickInterval
    {
        get => Gauge.TickInterval;
        set
        {
            Gauge.TickInterval = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     MinorTickInterval
    /// </summary>
    [Parameter]
    public float MinorTickInterval
    {
        get => Gauge.MinorTickInterval;
        set
        {
            Gauge.MinorTickInterval = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     MinValue
    /// </summary>
    [Parameter]
    public float MinValue
    {
        get => Gauge.MinValue;
        set
        {
            Gauge.MinValue = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     MaxValue
    /// </summary>
    [Parameter]
    public float MaxValue
    {
        get => Gauge.MaxValue;
        set
        {
            Gauge.MaxValue = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     LabelInterval
    /// </summary>
    [Parameter]
    public float LabelInterval
    {
        get => Gauge.LabelInterval;
        set
        {
            Gauge.LabelInterval = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     LabelExtent
    /// </summary>
    [Parameter]
    public float LabelExtent
    {
        get => Gauge.LabelExtent;
        set
        {
            Gauge.LabelExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     LabelFormatString
    /// </summary>
    [Parameter]
    public string LabelFormatString
    {
        get => Gauge.LabelFormatString;
        set
        {
            Gauge.LabelFormatString = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     LabelFontSize
    /// </summary>
    [Parameter]
    public float LabelFontSize
    {
        get => Gauge.LabelFontSize;
        set
        {
            Gauge.LabelFontSize = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     LabelFontWeight
    /// </summary>
    [Parameter]
    public SKFontStyleWeight LabelFontWeight
    {
        get => Gauge.LabelFontWeight;
        set
        {
            Gauge.LabelFontWeight = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     FontBrush
    /// </summary>
    private GaugeBrush _labelFontBrush = new(SKColors.Black);

    [Parameter]
    public GaugeBrush LabelFontBrush
    {
        get => _labelFontBrush;
        set
        {
            _labelFontBrush = value;
            Gauge.LabelFontBrush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     LabelFontFamily
    /// </summary>
    [Parameter]
    public string LabelFontFamily
    {
        get => Gauge.LabelFontFamily;
        set
        {
            Gauge.LabelFontFamily = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     ValueLocation
    /// </summary>
    [Parameter]
    public GaugeValueLocation ValueLocation
    {
        get => Gauge.ValueLocation;
        set
        {
            Gauge.ValueLocation = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     ValueExtent
    /// </summary>
    [Parameter]
    public float ValueExtent
    {
        get => Gauge.ValueExtent;
        set
        {
            Gauge.ValueExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     ValueFormatString
    /// </summary>
    [Parameter]
    public string ValueFormatString
    {
        get => Gauge.ValueFormatString;
        set
        {
            Gauge.ValueFormatString = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     ValueFontSize
    /// </summary>
    [Parameter]
    public float ValueFontSize
    {
        get => Gauge.ValueFontSize;
        set
        {
            Gauge.ValueFontSize = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     ValueFontWeight
    /// </summary>
    [Parameter]
    public SKFontStyleWeight ValueFontWeight
    {
        get => Gauge.ValueFontWeight;
        set
        {
            Gauge.ValueFontWeight = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     ValueFontBrush
    /// </summary>
    private GaugeBrush _valueFontBrush = new(SKColors.Black);

    [Parameter]
    public GaugeBrush ValueFontBrush
    {
        get => _valueFontBrush;
        set
        {
            _valueFontBrush = value;
            Gauge.ValueFontBrush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     ValueFontFamily
    /// </summary>
    [Parameter]
    public string ValueFontFamily
    {
        get => Gauge.ValueFontFamily;
        set
        {
            Gauge.ValueFontFamily = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     NeedleBrush
    /// </summary>
    private GaugeBrush _needleBrush = new(SKColors.Black);

    [Parameter]
    public GaugeBrush NeedleBrush
    {
        get => _needleBrush;
        set
        {
            _needleBrush = value;
            Gauge.NeedleBrush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     NeedleStartWidth
    /// </summary>
    [Parameter]
    public float NeedleStartWidth
    {
        get => Gauge.NeedleStartWidth;
        set
        {
            Gauge.NeedleStartWidth = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     NeedleEndWidth
    /// </summary>
    [Parameter]
    public float NeedleEndWidth
    {
        get => Gauge.NeedleEndWidth;
        set
        {
            Gauge.NeedleEndWidth = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     NeedleStartExtent
    /// </summary>
    [Parameter]
    public float NeedleStartExtent
    {
        get => Gauge.NeedleStartExtent;
        set
        {
            Gauge.NeedleStartExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     NeedleEndExtent
    /// </summary>
    [Parameter]
    public float NeedleEndExtent
    {
        get => Gauge.NeedleEndExtent;
        set
        {
            Gauge.NeedleEndExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     NeedleOutlineWidth
    /// </summary>
    [Parameter]
    public float NeedleOutlineWidth
    {
        get => Gauge.NeedleOutlineWidth;
        set
        {
            Gauge.NeedleOutlineWidth = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     NeedleOutlineBrush
    /// </summary>
    private GaugeBrush _needleOutlineBrush = new(SKColors.Transparent);

    [Parameter]
    public GaugeBrush NeedleOutlineBrush
    {
        get => _needleOutlineBrush;
        set
        {
            _needleOutlineBrush = value;
            Gauge.NeedleOutlineBrush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     SetValue
    /// </summary>
    [Parameter]
    public float SetNeedleValue
    {
        get => Gauge.SetNeedleValue;
        set
        {
            Gauge.SetNeedleValue = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     SetNeedleBrush
    /// </summary>
    private GaugeBrush _setNeedleBrush = new(SKColors.Transparent);

    [Parameter]
    public GaugeBrush SetNeedleBrush
    {
        get => _setNeedleBrush;
        set
        {
            _setNeedleBrush = value;
            Gauge.SetNeedleBrush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     SetNeedleOutlineWidth
    /// </summary>
    [Parameter]
    public float SetNeedleOutlineWidth
    {
        get => Gauge.SetNeedleOutlineWidth;
        set
        {
            Gauge.SetNeedleOutlineWidth = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     SetNeedleOutlineBrush
    /// </summary>
    private GaugeBrush _setNeedleOutlineBrush = new(SKColors.Transparent);

    [Parameter]
    public GaugeBrush SetNeedleOutlineBrush
    {
        get => _setNeedleOutlineBrush;
        set
        {
            _setNeedleOutlineBrush = value;
            Gauge.SetNeedleOutlineBrush = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     SetNeedleStartWidth
    /// </summary>
    [Parameter]
    public float SetNeedleStartWidth
    {
        get => Gauge.SetNeedleStartWidth;
        set
        {
            Gauge.SetNeedleStartWidth = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     SetNeedleEndWidth
    /// </summary>
    [Parameter]
    public float SetNeedleEndWidth
    {
        get => Gauge.SetNeedleEndWidth;
        set
        {
            Gauge.SetNeedleEndWidth = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     SetNeedleStartExtent
    /// </summary>
    [Parameter]
    public float SetNeedleStartExtent
    {
        get => Gauge.SetNeedleStartExtent;
        set
        {
            Gauge.SetNeedleStartExtent = value;
            Invalidate();
        }
    }

    /// <summary>
    ///     SetNeedleEndExtent
    /// </summary>
    [Parameter]
    public float SetNeedleEndExtent
    {
        get => Gauge.SetNeedleEndExtent;
        set
        {
            Gauge.SetNeedleEndExtent = value;
            Invalidate();
        }
    }

    #endregion
   
}