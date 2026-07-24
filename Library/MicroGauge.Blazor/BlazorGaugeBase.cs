using System.Diagnostics;
using MicroGauge.Constant;
using Microsoft.AspNetCore.Components;
using SkiaSharp;
using SkiaSharp.Views.Blazor;

// ReSharper disable SuggestBaseTypeForParameter


namespace MicroGauge.Blazor;

/// <summary>
///     BlazorGaugeBase - abstract class with shared common properties and methods
/// </summary>
public abstract class BlazorGaugeBase : SKGLView, IDisposable
{
    /// <summary>
    ///     Gauge - Wrapped MicroGauge
    /// </summary>
    public GaugeBase Gauge { get; protected init; } = null!;

    /// <summary>
    ///     Constructor
    /// </summary>
    protected BlazorGaugeBase()
    {
        OnPaintSurface += DrawContent;
    }

    private bool _suppressInvalidate;

    /// <summary>
    ///     SetParametersAsync - batch parameter updates into a single invalidation
    /// </summary>
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        _suppressInvalidate = true;
        try
        {
            await base.SetParametersAsync(parameters);
        }
        finally
        {
            _suppressInvalidate = false;
        }

        Invalidate();
    }

    /// <summary>
    ///     InvalidateGauge - request a repaint unless batched by SetParametersAsync
    /// </summary>
    protected internal void InvalidateGauge()
    {
        if (!_suppressInvalidate) Invalidate();
    }

    /// <summary>
    ///     Dispose - release cached gauge resources, then run SKGLView cleanup
    /// </summary>
    public new void Dispose()
    {
        _animationCts?.Cancel();
        Gauge?.Dispose();
        base.Dispose();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            AnimateValue(value);
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     NeedleAnimationDuration - milliseconds for the needle to ease to a new value (0 = instant)
    /// </summary>
    [Parameter]
    public double NeedleAnimationDuration { get; set; }

    private CancellationTokenSource? _animationCts;

    /// <summary>
    ///     AnimateValue - ease the gauge value to a target with cubic ease-out,
    ///     or jump directly when NeedleAnimationDuration is 0
    /// </summary>
    private void AnimateValue(double target)
    {
        _animationCts?.Cancel();
        if (NeedleAnimationDuration <= 0)
        {
            Gauge.Value = target;
            return;
        }

        _animationCts = new CancellationTokenSource();
        _ = RunValueAnimation(Gauge.Value, target, NeedleAnimationDuration, _animationCts.Token);
    }

    private async Task RunValueAnimation(double from, double target, double duration, CancellationToken token)
    {
        var start = DateTime.UtcNow;
        while (!token.IsCancellationRequested)
        {
            var t = (DateTime.UtcNow - start).TotalMilliseconds / duration;
            if (t >= 1) break;
            var eased = 1 - Math.Pow(1 - t, 3);
            Gauge.Value = from + (target - from) * eased;
            Invalidate();
            try
            {
                await Task.Delay(16, token);
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }

        if (token.IsCancellationRequested) return;
        Gauge.Value = target;
        Invalidate();
    }

    /// <summary>
    ///     BackingBrush
    /// </summary>
    private GaugeBrush _backingBrush = GaugeBrushes.White;

    [Parameter]
    public GaugeBrush BackingBrush
    {
        get => _backingBrush;
        set
        {
            _backingBrush = value;
            Gauge.BackingBrush = value;
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     BackingOutlineBrush
    /// </summary>
    private GaugeBrush _backingOutlineBrush = GaugeBrushes.Black;

    [Parameter]
    public GaugeBrush BackingOutlineBrush
    {
        get => _backingOutlineBrush;
        set
        {
            _backingOutlineBrush = value;
            Gauge.BackingOutlineBrush = value;
            InvalidateGauge();
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
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     TickBrush
    /// </summary>
    private GaugeBrush _tickBrush = GaugeBrushes.Black;

    [Parameter]
    public GaugeBrush TickBrush
    {
        get => _tickBrush;
        set
        {
            _tickBrush = value;
            Gauge.TickBrush = value;
            InvalidateGauge();
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
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     MinorTickBrush
    /// </summary>
    private GaugeBrush _minorTickBrush = GaugeBrushes.LightGray;

    [Parameter]
    public GaugeBrush MinorTickBrush
    {
        get => _minorTickBrush;
        set
        {
            _minorTickBrush = value;
            Gauge.MinorTickBrush = value;
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     FontBrush
    /// </summary>
    private GaugeBrush _labelFontBrush = GaugeBrushes.Black;

    [Parameter]
    public GaugeBrush LabelFontBrush
    {
        get => _labelFontBrush;
        set
        {
            _labelFontBrush = value;
            Gauge.LabelFontBrush = value;
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     ValueFontBrush
    /// </summary>
    private GaugeBrush _valueFontBrush = GaugeBrushes.Black;

    [Parameter]
    public GaugeBrush ValueFontBrush
    {
        get => _valueFontBrush;
        set
        {
            _valueFontBrush = value;
            Gauge.ValueFontBrush = value;
            InvalidateGauge();
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
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     NeedleBrush
    /// </summary>
    private GaugeBrush _needleBrush = GaugeBrushes.Black;

    [Parameter]
    public GaugeBrush NeedleBrush
    {
        get => _needleBrush;
        set
        {
            _needleBrush = value;
            Gauge.NeedleBrush = value;
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     NeedleOutlineBrush
    /// </summary>
    private GaugeBrush _needleOutlineBrush = GaugeBrushes.Transparent;

    [Parameter]
    public GaugeBrush NeedleOutlineBrush
    {
        get => _needleOutlineBrush;
        set
        {
            _needleOutlineBrush = value;
            Gauge.NeedleOutlineBrush = value;
            InvalidateGauge();
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
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     SetNeedleBrush
    /// </summary>
    private GaugeBrush _setNeedleBrush = GaugeBrushes.Transparent;

    [Parameter]
    public GaugeBrush SetNeedleBrush
    {
        get => _setNeedleBrush;
        set
        {
            _setNeedleBrush = value;
            Gauge.SetNeedleBrush = value;
            InvalidateGauge();
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
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     SetNeedleOutlineBrush
    /// </summary>
    private GaugeBrush _setNeedleOutlineBrush = GaugeBrushes.Transparent;

    [Parameter]
    public GaugeBrush SetNeedleOutlineBrush
    {
        get => _setNeedleOutlineBrush;
        set
        {
            _setNeedleOutlineBrush = value;
            Gauge.SetNeedleOutlineBrush = value;
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
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
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     BackgroundImage - image drawn over the backing, clipped to the gauge shape
    ///     (load via SKImage.FromEncodedData, e.g. from HttpClient bytes)
    /// </summary>
    [Parameter]
    public SKImage BackgroundImage
    {
        get => Gauge.BackgroundImage;
        set
        {
            Gauge.BackgroundImage = value;
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     BackgroundImageOpacity - opacity of background image (0 to 1)
    /// </summary>
    [Parameter]
    public float BackgroundImageOpacity
    {
        get => Gauge.BackgroundImageOpacity;
        set
        {
            Gauge.BackgroundImageOpacity = value;
            InvalidateGauge();
        }
    }

    /// <summary>
    ///     BackgroundImageRotation - rotation of background image in degrees about the gauge center
    /// </summary>
    [Parameter]
    public double BackgroundImageRotation
    {
        get => Gauge.BackgroundImageRotation;
        set
        {
            Gauge.BackgroundImageRotation = value;
            InvalidateGauge();
        }
    }

    #endregion
}