using System;
using System.Globalization;
using MicroGauge.Constant;
using SkiaSharp;

// ReSharper disable PossibleLossOfFraction

namespace MicroGauge
{
    public abstract class GaugeBase
    {
        #region Properties

        /// <summary>
        /// 	Canvas - Skia Drawing Surface
        /// </summary>
        public SKCanvas Canvas { get; set; }

        /// <summary>
        /// 	SurfaceWidth - Measured width of the surface
        /// </summary>
        public int SurfaceWidth { get; set; } = 400;

        /// <summary>
        /// 	SurfaceHeight - Measured height of the surface
        /// </summary>
        public int SurfaceHeight { get; set; } = 400;

        /// <summary>
        /// 	BottomExtent - bottom padding extent of surface height
        /// </summary>
        public float BottomExtent { get; set; }

        /// <summary>
        /// 	TopExtent - top padding extent of surface height
        /// </summary>
        public float TopExtent { get; set; }

        /// <summary>
        /// 	LeftExtent - left padding extent of surface width
        /// </summary>
        public float LeftExtent { get; set; }

        /// <summary>
        /// 	RightExtent - right padding extent of surface width
        /// </summary>
        public float RightExtent { get; set; }

        /// <summary>
        /// 	BackingShader - Backing solid or linear gradient shader
        /// </summary>
        public SKShader BackingShader { get; set; } = SKShader.CreateColor(SKColors.White);

        /// <summary>
        /// 	BackingOutlineShader - Backing outline solid or linear gradient shader
        /// </summary>
        public SKShader BackingOutlineShader { get; set; } = SKShader.CreateColor(SKColors.Black);

        /// <summary>
        /// 	BackingStrokeWidth - Back stroke with for backing edge
        /// </summary>
        public float BackingStrokeWidth { get; set; } = 10f;

        /// <summary>
        /// 	TickShader - Tick solid or linear gradient shader
        /// </summary>
        public SKShader TickShader { get; set; } = SKShader.CreateColor(SKColors.Black);

        /// <summary>
        /// 	TickStrokeWidth - Tick Width
        /// </summary>
        public float TickStrokeWidth { get; set; } = 1f;

        /// <summary>
        /// 	MinorTickShader - Minor Tick solid or linear gradient shader
        /// </summary>
        public SKShader MinorTickShader { get; set; } = SKShader.CreateColor(SKColors.LightGray);

        /// <summary>
        /// 	MinorTickStrokeWidth - Minor Tick Width
        /// </summary>
        public float MinorTickStrokeWidth { get; set; } = 0.5f;

        /// <summary>
        /// 	TickInterval - Tick Interval
        /// </summary>
        public float TickInterval { get; set; } = 5f;

        /// <summary>
        /// 	MinorTickInterval - Minor Tick Interval
        /// </summary>
        public float MinorTickInterval { get; set; } = 1f;

        /// <summary>
        /// 	Value - Current Value for scale
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 	MinValue - Min value that corresponds to start
        /// </summary>
        public float MinValue { get; set; }

        /// <summary>
        /// 	MaxValue - Max value that corresponds to start
        /// </summary>
        public float MaxValue { get; set; } = 100f;

        /// <summary>
        /// 	LabelInterval - Label interval
        /// </summary>
        public float LabelInterval { get; set; } = 10f;

        /// <summary>
        /// 	LabelExtent - Label extent from center
        /// </summary>
        public float LabelExtent { get; set; } = 0.85f;

        /// <summary>
        /// 	LabelFormatString - Format string applied to label values, ex. "{0:N0}"
        /// </summary>
        public string LabelFormatString { get; set; } = "{0:N0}";

        /// <summary>
        /// 	LabelFontSize - Label Font Size
        /// </summary>
        public float LabelFontSize { get; set; } = 10f;

        /// <summary>
        /// 	LabelFontWeight - SKFontStyleWeight for Label (Light, Normal, Bold, etc.)
        /// </summary>
        public SKFontStyleWeight LabelFontWeight { get; set; } = SKFontStyleWeight.Normal;

        /// <summary>
        /// 	LabelFontShader - Label font solid or linear gradient shader
        /// </summary>
        public SKShader LabelFontShader { get; set; } = SKShader.CreateColor(SKColors.Black);

        /// <summary>
        /// 	LabelFontFamily - Label Font Family Name
        /// </summary>
        public string LabelFontFamily { get; set; } = "verdana";

        /// <summary>
        /// 	ValueLocation - Value location on gauge (TopCenter, BottomCenter, LeftCenter, RightCenter)
        /// </summary>
        public GaugeValueLocation ValueLocation { get; set; }

        /// <summary>
        /// 	ValueExtent - Value extent from center in direction of value location
        /// </summary>
        public float ValueExtent { get; set; } = 0.60f;

        /// <summary>
        /// 	ValueFormatString - Format string applied to Value values, ex. "{0:N0}"
        /// </summary>
        public string ValueFormatString { get; set; } = "{0:N0}";

        /// <summary>
        /// 	ValueFontSize - Value Font Size
        /// </summary>
        public float ValueFontSize { get; set; } = 20f;

        /// <summary>
        /// 	ValueFontWeight - SKFontStyleWeight for Value (Light, Normal, Bold, etc.)
        /// </summary>
        public SKFontStyleWeight ValueFontWeight { get; set; } = SKFontStyleWeight.Normal;

        /// <summary>
        /// 	ValueFontShader - Value font solid or linear gradient shader
        /// </summary>
        public SKShader ValueFontShader { get; set; } = SKShader.CreateColor(SKColors.Black);

        /// <summary>
        /// 	ValueFontFamily - Value Font Family Name
        /// </summary>
        public string ValueFontFamily { get; set; } = "verdana";

        /// <summary>
        /// 	NeedleShader - Needle solid or linear gradient shader
        /// </summary>
        public SKShader NeedleShader { get; set; } = SKShader.CreateColor(SKColors.Black);

        /// <summary>
        /// 	NeedleStartWidth - width of needle at start
        /// </summary>
        public float NeedleStartWidth { get; set; } = 6f;

        /// <summary>
        /// 	NeedleEndWidth - width of needle at end
        /// </summary>
        public float NeedleEndWidth { get; set; } = 3f;

        /// <summary>
        /// 	NeedleStartExtent - needle start as extent of radius or width
        /// </summary>
        public float NeedleStartExtent { get; set; }

        /// <summary>
        /// 	NeedleEndExtent - needle end as extent of radius or width
        /// </summary>
        public float NeedleEndExtent { get; set; } = 0.71f;

        /// <summary>
        /// 	SetNeedleValue - Set Needle Value
        /// </summary>
        public float SetNeedleValue { get; set; }

        /// <summary>
        /// 	SetNeedleShader - set needle solid or linear gradient shader
        /// </summary>
        public SKShader SetNeedleShader { get; set; } = SKShader.CreateColor(SKColors.Transparent);

        /// <summary>
        /// 	SetNeedleStartWidth - width of set needle at start
        /// </summary>
        public float SetNeedleStartWidth { get; set; } = 6f;

        /// <summary>
        /// 	SetNeedleEndWidth - width of set needle at end
        /// </summary>
        public float SetNeedleEndWidth { get; set; } = 3f;

        /// <summary>
        /// 	SetNeedleStartExtent - set needle start as extent of radius or width
        /// </summary>
        public float SetNeedleStartExtent { get; set; }

        /// <summary>
        /// 	SetNeedleEndExtent - needle at end as extent of radius or width
        /// </summary>
        public float SetNeedleEndExtent { get; set; } = 0.71f;

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
        ///     DebugFillCanvas
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        protected void DebugFillCanvas(SKColor color)
        {
            using (SKPaint paint = new SKPaint())
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
            using (SKPaint paint = new SKPaint())
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
            using (SKPath path = new SKPath())
            {
                SKPoint p1 = GaugeHelper.GetRadialPoint(start, startWidth, angle + 90);
                SKPoint p2 = GaugeHelper.GetRadialPoint(start, startWidth, angle - 90);
                SKPoint p3 = GaugeHelper.GetRadialPoint(end, endWidth, angle - 90);
                SKPoint p4 = GaugeHelper.GetRadialPoint(end, endWidth, angle + 90);
                DrawPoly(paint, path, p1, p2, p3, p4);
            }
        }

        /// <summary>
        ///     DrawPoly - Draw poly with 4 points
        /// </summary>
        protected void DrawPoly(SKPaint paint, SKPath path, SKPoint p1, SKPoint p2, SKPoint p3, SKPoint p4)
        {
            path.MoveTo(p1);
            path.LineTo(p2);
            path.LineTo(p3);
            path.LineTo(p4);
            path.Close();
            Canvas.DrawPath(path, paint);
        }


        /// <summary>
        ///     SetLabelPaint
        /// </summary>
        protected void SetLabelPaint(SKPaint paint)
        {
            paint.IsAntialias = true;
            paint.Shader = LabelFontShader;
            paint.IsStroke = false;
            paint.TextAlign = SKTextAlign.Center;
            paint.TextSize = LabelFontSize;
            paint.Typeface = SKTypeface.FromFamilyName(LabelFontFamily, LabelFontWeight,
                SKFontStyleWidth.Expanded, SKFontStyleSlant.Upright);
        }

        /// <summary>
        ///     SetValuePaint
        /// </summary>
        protected void SetValuePaint(SKPaint paint)
        {
            paint.IsAntialias = true;
            paint.Shader = ValueFontShader;
            paint.IsStroke = false;
            paint.TextAlign = SKTextAlign.Center;
            paint.TextSize = ValueFontSize;
            paint.Typeface = SKTypeface.FromFamilyName(ValueFontFamily, ValueFontWeight,
                SKFontStyleWidth.Expanded, SKFontStyleSlant.Upright);
        }

        /// <summary>
        ///     GetLabelFormattedValue
        /// </summary>
        protected string GetLabelFormattedValue(float value)
        {
            try
            {
                return LabelFormatString == null ?
                    Convert.ToString(value, CultureInfo.InvariantCulture) :
                    string.Format(LabelFormatString, value);
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }


        /// <summary>
        ///     GetValueFormattedValue
        /// </summary>
        protected string GetValueFormattedValue(float value)
        {
            try
            {
                return ValueFormatString == null ? 
                    Convert.ToString(value, CultureInfo.InvariantCulture) : 
                    string.Format(ValueFormatString, value);
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        #endregion
    }
}