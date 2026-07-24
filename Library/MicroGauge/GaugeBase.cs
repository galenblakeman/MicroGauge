using System;
using System.Globalization;
using MicroGauge.Constant;
using SkiaSharp;

// ReSharper disable PossibleLossOfFraction

namespace MicroGauge
{
    /// <summary>
    ///     GaugeBase - abstract class with shared common properties and methods.
    ///     Dispose releases cached fonts; BackgroundImage is owned by the caller.
    /// </summary>
    public abstract class GaugeBase : IDisposable
    {
        #region Properties

        /// <summary>
        ///     Canvas - Skia Drawing Surface
        /// </summary>
        public SKCanvas Canvas { get; set; }

        /// <summary>
        ///     SurfaceWidth - Measured width of the surface
        /// </summary>
        public int SurfaceWidth { get; set; } = 400;

        /// <summary>
        ///     SurfaceHeight - Measured height of the surface
        /// </summary>
        public int SurfaceHeight { get; set; } = 400;


        /// <summary>
        ///     GradientOffset -  Offset for linear gradient
        /// </summary>
        public SKPoint GradientOffset { get; set; }

        /// <summary>
        ///     GradientWidth -  Width for linear gradient
        /// </summary>
        public float GradientWidth { get; set; } = 200;

        /// <summary>
        ///     GradientHeight -  Height for linear gradient
        /// </summary>
        public float GradientHeight { get; set; } = 200;

        /// <summary>
        ///     BottomExtent - bottom padding extent of surface height
        /// </summary>
        public float BottomExtent { get; set; }

        /// <summary>
        ///     TopExtent - top padding extent of surface height
        /// </summary>
        public float TopExtent { get; set; }

        /// <summary>
        ///     LeftExtent - left padding extent of surface width
        /// </summary>
        public float LeftExtent { get; set; }

        /// <summary>
        ///     RightExtent - right padding extent of surface width
        /// </summary>
        public float RightExtent { get; set; }

        /// <summary>
        ///     BackingBrush - Backing solid or linear gradient shader
        /// </summary>
        public GaugeBrush BackingBrush { get; set; } = GaugeBrushes.White;

        /// <summary>
        ///     BackingOutlineBrush - Backing outline solid or linear gradient shader
        /// </summary>
        public GaugeBrush BackingOutlineBrush { get; set; } = GaugeBrushes.Black;

        /// <summary>
        ///     BackingStrokeWidth - Back stroke with for backing edge
        /// </summary>
        public float BackingStrokeWidth { get; set; } = 1f;

        /// <summary>
        ///     TickBrush - Tick solid or linear gradient shader
        /// </summary>
        public GaugeBrush TickBrush { get; set; } = GaugeBrushes.Black;

        /// <summary>
        ///     TickStrokeWidth - Tick Width
        /// </summary>
        public float TickStrokeWidth { get; set; } = 1f;

        /// <summary>
        ///     MinorTickBrush - Minor Tick solid or linear gradient shader
        /// </summary>
        public GaugeBrush MinorTickBrush { get; set; } = GaugeBrushes.Black;

        /// <summary>
        ///     MinorTickStrokeWidth - Minor Tick Width
        /// </summary>
        public float MinorTickStrokeWidth { get; set; } = 0.5f;

        /// <summary>
        ///     TickInterval - Tick Interval
        /// </summary>
        public float TickInterval { get; set; } = 5f;

        /// <summary>
        ///     MinorTickInterval - Minor Tick Interval
        /// </summary>
        public float MinorTickInterval { get; set; } = 1f;

        /// <summary>
        ///     Value - Current Value for scale
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        ///     MinValue - Min value that corresponds to start
        /// </summary>
        public float MinValue { get; set; }

        /// <summary>
        ///     MaxValue - Max value that corresponds to start
        /// </summary>
        public float MaxValue { get; set; } = 100f;

        /// <summary>
        ///     LabelInterval - Label interval
        /// </summary>
        public float LabelInterval { get; set; } = 10f;

        /// <summary>
        ///     LabelExtent - Label extent from center
        /// </summary>
        public float LabelExtent { get; set; } = 0.85f;

        /// <summary>
        ///     LabelFormatString - Format string applied to label values, ex. "{0:N0}"
        /// </summary>
        public string LabelFormatString { get; set; } = "{0:N0}";

        /// <summary>
        ///     LabelFontSize - Label Font Size
        /// </summary>
        public float LabelFontSize { get; set; } = 10f;

        /// <summary>
        ///     LabelFontWeight - SKFontStyleWeight for Label (Light, Normal, Bold, etc.)
        /// </summary>
        public SKFontStyleWeight LabelFontWeight { get; set; } = SKFontStyleWeight.Normal;

        /// <summary>
        ///     LabelFontBrush - Label font solid or linear gradient shader
        /// </summary>
        public GaugeBrush LabelFontBrush { get; set; } = GaugeBrushes.Black;

        /// <summary>
        ///     LabelFontFamily - Label Font Family Name
        /// </summary>
        public string LabelFontFamily { get; set; } = "verdana";

        /// <summary>
        ///     ValueLocation - Value location on gauge (TopCenter, BottomCenter, LeftCenter, RightCenter)
        /// </summary>
        public GaugeValueLocation ValueLocation { get; set; }

        /// <summary>
        ///     ValueExtent - Value extent from center in direction of value location
        /// </summary>
        public float ValueExtent { get; set; } = 0.60f;

        /// <summary>
        ///     ValueFormatString - Format string applied to Value values, ex. "{0:N0}"
        /// </summary>
        public string ValueFormatString { get; set; } = "{0:N0}";

        /// <summary>
        ///     ValueFontSize - Value Font Size
        /// </summary>
        public float ValueFontSize { get; set; } = 20f;

        /// <summary>
        ///     ValueFontWeight - SKFontStyleWeight for Value (Light, Normal, Bold, etc.)
        /// </summary>
        public SKFontStyleWeight ValueFontWeight { get; set; } = SKFontStyleWeight.Normal;

        /// <summary>
        ///     ValueFontBrush - Value font solid or linear gradient shader
        /// </summary>
        public GaugeBrush ValueFontBrush { get; set; } = GaugeBrushes.Black;

        /// <summary>
        ///     ValueFontFamily - Value Font Family Name
        /// </summary>
        public string ValueFontFamily { get; set; } = "verdana";

        /// <summary>
        ///     NeedleBrush - Needle solid or linear gradient shader
        /// </summary>
        public GaugeBrush NeedleBrush { get; set; } = GaugeBrushes.Black;

        /// <summary>
        ///     NeedleStartWidth - width of needle at start
        /// </summary>
        public float NeedleStartWidth { get; set; } = 6f;

        /// <summary>
        ///     NeedleEndWidth - width of needle at end
        /// </summary>
        public float NeedleEndWidth { get; set; } = 3f;

        /// <summary>
        ///     NeedleStartExtent - needle start as extent of radius or width
        /// </summary>
        public float NeedleStartExtent { get; set; }

        /// <summary>
        ///     NeedleEndExtent - needle end as extent of radius or width
        /// </summary>
        public float NeedleEndExtent { get; set; } = 0.71f;

        /// <summary>
        ///     NeedleOutlineWidth - width of needle outline
        /// </summary>
        public float NeedleOutlineWidth { get; set; } = 1f;

        /// <summary>
        ///     NeedleOutlineBrush - Needle outline solid or linear gradient shader
        /// </summary>
        public GaugeBrush NeedleOutlineBrush { get; set; } = GaugeBrushes.Transparent;

        /// <summary>
        ///     SetNeedleValue - Set Needle Value
        /// </summary>
        public float SetNeedleValue { get; set; }

        /// <summary>
        ///     SetNeedleOutlineWidth - width of set needle outline
        /// </summary>
        public float SetNeedleOutlineWidth { get; set; } = 1f;

        /// <summary>
        ///     SetNeedleBrush - set needle solid or linear gradient shader
        /// </summary>
        public GaugeBrush SetNeedleBrush { get; set; } = GaugeBrushes.Transparent;

        /// <summary>
        ///     SetNeedleOutlineBrush - set needle outline solid or linear gradient shader
        /// </summary>
        public GaugeBrush SetNeedleOutlineBrush { get; set; } = GaugeBrushes.Transparent;

        /// <summary>
        ///     SetNeedleStartWidth - width of set needle at start
        /// </summary>
        public float SetNeedleStartWidth { get; set; } = 6f;

        /// <summary>
        ///     SetNeedleEndWidth - width of set needle at end
        /// </summary>
        public float SetNeedleEndWidth { get; set; } = 3f;

        /// <summary>
        ///     SetNeedleStartExtent - set needle start as extent of radius or width
        /// </summary>
        public float SetNeedleStartExtent { get; set; }

        /// <summary>
        ///     SetNeedleEndExtent - needle at end as extent of radius or width
        /// </summary>
        public float SetNeedleEndExtent { get; set; } = 0.71f;

        /// <summary>
        ///     BackgroundImage - image drawn over the backing, clipped to the gauge shape
        /// </summary>
        public SKImage BackgroundImage { get; set; }

        /// <summary>
        ///     BackgroundImageOpacity - opacity of background image (0 to 1)
        /// </summary>
        public float BackgroundImageOpacity { get; set; } = 1f;

        /// <summary>
        ///     BackgroundImageRotation - rotation of background image in degrees about the gauge center
        /// </summary>
        public double BackgroundImageRotation { get; set; }

        /// <summary>
        ///     EnableStaticCaching - cache static layers (backing, image, ranges, ticks, labels)
        ///     as pictures replayed on each draw, so value updates only redraw the needle.
        ///     Static state changes are detected automatically; brushes are tracked by
        ///     reference, so after mutating a brush in place call InvalidateStaticLayers.
        /// </summary>
        public bool EnableStaticCaching { get; set; } = true;

        #endregion

        #region Static layer cache

        private SKPicture _underLayer;
        private SKPicture _overLayer;
        private long _cacheSignature;
        private int _cacheWidth;
        private int _cacheHeight;

        /// <summary>
        ///     InvalidateStaticLayers - drop cached static layers so the next draw re-records them
        /// </summary>
        public void InvalidateStaticLayers()
        {
            _underLayer?.Dispose();
            _underLayer = null;
            _overLayer?.Dispose();
            _overLayer = null;
        }

        /// <summary>
        ///     DrawWithCache - replay cached under/over layers around the dynamic draw,
        ///     re-recording them when the signature or surface size changes
        /// </summary>
        protected void DrawWithCache(long signature, Action drawUnder, Action drawDynamic, Action drawOver)
        {
            if (!EnableStaticCaching)
            {
                drawUnder();
                drawDynamic();
                drawOver();
                return;
            }

            if (_underLayer == null || signature != _cacheSignature ||
                _cacheWidth != SurfaceWidth || _cacheHeight != SurfaceHeight)
            {
                InvalidateStaticLayers();
                _underLayer = RecordLayer(drawUnder);
                _overLayer = RecordLayer(drawOver);
                _cacheSignature = signature;
                _cacheWidth = SurfaceWidth;
                _cacheHeight = SurfaceHeight;
            }

            Canvas.DrawPicture(_underLayer);
            drawDynamic();
            Canvas.DrawPicture(_overLayer);
        }

        /// <summary>
        ///     RecordLayer - record a draw action into a picture by temporarily
        ///     redirecting Canvas to a recording canvas
        /// </summary>
        private SKPicture RecordLayer(Action draw)
        {
            using (var recorder = new SKPictureRecorder())
            {
                var realCanvas = Canvas;
                Canvas = recorder.BeginRecording(SKRect.Create(SurfaceWidth, SurfaceHeight));
                try
                {
                    draw();
                }
                finally
                {
                    Canvas = realCanvas;
                }

                return recorder.EndRecording();
            }
        }

        /// <summary>
        ///     HashCombine - fold a value's hash into a running signature
        /// </summary>
        protected static long HashCombine(long hash, object value)
        {
            return hash * 31 + (value?.GetHashCode() ?? 0);
        }

        /// <summary>
        ///     ComputeBaseStaticSignature - signature of shared state that affects the static layers
        /// </summary>
        protected long ComputeBaseStaticSignature()
        {
            long hash = 17;
            hash = HashCombine(hash, TopExtent);
            hash = HashCombine(hash, BottomExtent);
            hash = HashCombine(hash, LeftExtent);
            hash = HashCombine(hash, RightExtent);
            hash = HashCombine(hash, BackingBrush);
            hash = HashCombine(hash, BackingOutlineBrush);
            hash = HashCombine(hash, BackingStrokeWidth);
            hash = HashCombine(hash, BackgroundImage);
            hash = HashCombine(hash, BackgroundImageOpacity);
            hash = HashCombine(hash, BackgroundImageRotation);
            hash = HashCombine(hash, TickBrush);
            hash = HashCombine(hash, TickStrokeWidth);
            hash = HashCombine(hash, MinorTickBrush);
            hash = HashCombine(hash, MinorTickStrokeWidth);
            hash = HashCombine(hash, TickInterval);
            hash = HashCombine(hash, MinorTickInterval);
            hash = HashCombine(hash, MinValue);
            hash = HashCombine(hash, MaxValue);
            hash = HashCombine(hash, LabelInterval);
            hash = HashCombine(hash, LabelExtent);
            hash = HashCombine(hash, LabelFormatString);
            hash = HashCombine(hash, LabelFontSize);
            hash = HashCombine(hash, LabelFontWeight);
            hash = HashCombine(hash, LabelFontBrush);
            hash = HashCombine(hash, LabelFontFamily);
            return hash;
        }

        #endregion

        #region Draw

        /// <summary>
        ///     DrawContent
        /// </summary>
        public virtual void DrawContent()
        {
        }

        #endregion

        #region Helpers

        /// <summary>
        ///     GetSkShader - wrapper GetSkShader against this control
        /// </summary>
        protected SKShader GetSkShader(GaugeBrush brush)
        {
            return GaugeHelper.ConvertToSkShader(brush, GradientOffset, GradientWidth, GradientHeight);
        }

        /// <summary>
        ///     DebugFillCanvas
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        protected void DebugFillCanvas(SKColor color)
        {
            using (var paint = new SKPaint())
            {
                paint.Color = color;
                Canvas.DrawPaint(paint);
            }
        }

        /// <summary>
        ///     DebugCircleAtPoint
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        protected void DebugCircleAtPoint(SKPoint point, SKColor color)
        {
            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = 2;
                paint.StrokeCap = SKStrokeCap.Round;
                paint.Color = color;
                Canvas.DrawCircle(point, 10f, paint);
            }
        }

        /// <summary>
        ///     DrawNeedlePoly - Draw poly for needle
        /// </summary>
        protected void DrawNeedlePoly(SKPaint paint, SKPoint start, SKPoint end, float startWidth, float endWidth,
            double angle)
        {
            if (startWidth < 0.25) startWidth = 0.25f;
            if (endWidth < 0.25) endWidth = 0.25f;
            var p1 = GaugeHelper.GetRadialPoint(start, startWidth, angle + 90);
            var p2 = GaugeHelper.GetRadialPoint(start, startWidth, angle - 90);
            var p3 = GaugeHelper.GetRadialPoint(end, endWidth, angle - 90);
            var p4 = GaugeHelper.GetRadialPoint(end, endWidth, angle + 90);
            DrawPoly(paint, p1, p2, p3, p4);
        }

        /// <summary>
        ///     DrawPoly - Draw poly with 4 points
        /// </summary>
        protected void DrawPoly(SKPaint paint, SKPoint p1, SKPoint p2, SKPoint p3, SKPoint p4)
        {
            using (var builder = new SKPathBuilder())
            {
                builder.MoveTo(p1);
                builder.LineTo(p2);
                builder.LineTo(p3);
                builder.LineTo(p4);
                builder.Close();
                using (var path = builder.Detach())
                {
                    Canvas.DrawPath(path, paint);
                }
            }
        }


        /// <summary>
        ///     SetBackgroundImagePaint - shader that cover-scales BackgroundImage onto the target rect,
        ///     modulated by BackgroundImageOpacity
        /// </summary>
        protected void SetBackgroundImagePaint(SKPaint paint, float targetX, float targetY,
            float targetWidth, float targetHeight)
        {
            var scale = Math.Max(targetWidth / BackgroundImage.Width, targetHeight / BackgroundImage.Height);
            var matrix = SKMatrix.CreateScale(scale, scale);
            matrix.TransX = targetX + (targetWidth - BackgroundImage.Width * scale) / 2;
            matrix.TransY = targetY + (targetHeight - BackgroundImage.Height * scale) / 2;
            if (Math.Abs(BackgroundImageRotation) > 0.001)
            {
                var rotation = SKMatrix.CreateRotationDegrees(Convert.ToSingle(BackgroundImageRotation),
                    targetX + targetWidth / 2, targetY + targetHeight / 2);
                matrix = SKMatrix.Concat(rotation, matrix);
            }

            var sampling = new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.Linear);
            paint.IsAntialias = true;
            paint.Style = SKPaintStyle.Fill;
            paint.Shader = BackgroundImage.ToShader(SKShaderTileMode.Clamp, SKShaderTileMode.Clamp,
                sampling, matrix);
            var opacity = BackgroundImageOpacity < 0f ? 0f :
                BackgroundImageOpacity > 1f ? 1f : BackgroundImageOpacity;
            paint.Color = SKColors.White.WithAlpha((byte)(opacity * 255));
        }

        /// <summary>
        ///     SetLabelPaint
        /// </summary>
        protected void SetLabelPaint(SKPaint paint)
        {
            paint.IsAntialias = true;
            paint.Shader = GetSkShader(LabelFontBrush);
            paint.IsStroke = false;
        }

        /// <summary>
        ///     SetValuePaint
        /// </summary>
        protected void SetValuePaint(SKPaint paint)
        {
            paint.IsAntialias = true;
            paint.Shader = GetSkShader(ValueFontBrush);
            paint.IsStroke = false;
        }

        private SKFont _labelFont;
        private SKTypeface _labelTypeface;
        private string _labelFontKey;

        /// <summary>
        ///     GetLabelFont - cached font for label text
        /// </summary>
        protected SKFont GetLabelFont()
        {
            var key = $"{LabelFontFamily}|{(int)LabelFontWeight}|{LabelFontSize}";
            if (_labelFont == null || _labelFontKey != key)
            {
                _labelFont?.Dispose();
                _labelTypeface?.Dispose();
                _labelTypeface = SKTypeface.FromFamilyName(LabelFontFamily, LabelFontWeight,
                    SKFontStyleWidth.Expanded, SKFontStyleSlant.Upright);
                _labelFont = new SKFont(_labelTypeface, LabelFontSize);
                _labelFontKey = key;
            }

            return _labelFont;
        }

        private SKFont _valueFont;
        private SKTypeface _valueTypeface;
        private string _valueFontKey;

        /// <summary>
        ///     GetValueFont - cached font for value text
        /// </summary>
        protected SKFont GetValueFont()
        {
            var key = $"{ValueFontFamily}|{(int)ValueFontWeight}|{ValueFontSize}";
            if (_valueFont == null || _valueFontKey != key)
            {
                _valueFont?.Dispose();
                _valueTypeface?.Dispose();
                _valueTypeface = SKTypeface.FromFamilyName(ValueFontFamily, ValueFontWeight,
                    SKFontStyleWidth.Expanded, SKFontStyleSlant.Upright);
                _valueFont = new SKFont(_valueTypeface, ValueFontSize);
                _valueFontKey = key;
            }

            return _valueFont;
        }

        /// <summary>
        ///     Dispose - release cached fonts and typefaces; safe to reuse the gauge
        ///     afterwards (caches rebuild on next draw). BackgroundImage is not disposed
        ///     because it is supplied and owned by the caller.
        /// </summary>
        public void Dispose()
        {
            _labelFont?.Dispose();
            _labelFont = null;
            _labelTypeface?.Dispose();
            _labelTypeface = null;
            _labelFontKey = null;
            _valueFont?.Dispose();
            _valueFont = null;
            _valueTypeface?.Dispose();
            _valueTypeface = null;
            _valueFontKey = null;
            InvalidateStaticLayers();
        }

        /// <summary>
        ///     GetLabelFormattedValue
        /// </summary>
        protected string GetLabelFormattedValue(float value)
        {
            try
            {
                return LabelFormatString == null
                    ? Convert.ToString(value, CultureInfo.InvariantCulture)
                    : string.Format(LabelFormatString, value);
            }
            catch (FormatException)
            {
                return Convert.ToString(value, CultureInfo.InvariantCulture);
            }
        }


        /// <summary>
        ///     GetValueFormattedValue
        /// </summary>
        protected string GetValueFormattedValue(float value)
        {
            try
            {
                return ValueFormatString == null
                    ? Convert.ToString(value, CultureInfo.InvariantCulture)
                    : string.Format(ValueFormatString, value);
            }
            catch (FormatException)
            {
                return Convert.ToString(value, CultureInfo.InvariantCulture);
            }
        }

        #endregion
    }
}