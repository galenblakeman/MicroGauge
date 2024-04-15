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

        public SKCanvas Canvas { get; set; }
        public int SurfaceWidth { get; set; } = 400;
        public int SurfaceHeight { get; set; } = 400;
        public float BottomExtent { get; set; }
        public float TopExtent { get; set; }
        public float LeftExtent { get; set; }
        public float RightExtent { get; set; }
        public double Value { get; set; }
        public SKShader BackingShader { get; set; } = SKShader.CreateColor(SKColors.Black);
        public SKShader BackingOutlineShader { get; set; } = SKShader.CreateColor(SKColors.Black);
        public float BackingStrokeWidth { get; set; } = 20f;
        public SKShader TickShader { get; set; } = SKShader.CreateColor(SKColors.White);
        public float TickStrokeWidth { get; set; } = 1f;
        public SKShader MinorTickShader { get; set; } = SKShader.CreateColor(SKColors.LightGray);
        public float MinorTickStrokeWidth { get; set; } = 0.5f;
        public float Interval { get; set; } = 5f;
        public float MinorInterval { get; set; } = 1f;
        public float MinValue { get; set; }
        public float MaxValue { get; set; } = 100f;
        public float LabelInterval { get; set; } = 10f;
        public float LabelExtent { get; set; } = 0.85f;
        public string LabelFormatString { get; set; } = "{0:N0}";
        public float LabelFontSize { get; set; } = 16f;
        public SKFontStyleWeight LabelFontWeight { get; set; } = SKFontStyleWeight.Normal;
        public SKShader LabelFontShader { get; set; } = SKShader.CreateColor(SKColors.White);
        public string LabelFontFamily { get; set; } = "verdana";
        public GaugeValueLocation ValueLocation { get; set; }
        public float ValueExtent { get; set; } = 0.60f;
        public string ValueFormatString { get; set; } = "{0:N0}";
        public float ValueFontSize { get; set; } = 20f;
        public SKFontStyleWeight ValueFontWeight { get; set; } = SKFontStyleWeight.Normal;
        public SKShader ValueFontShader { get; set; } = SKShader.CreateColor(SKColors.Transparent);
        public string ValueFontFamily { get; set; } = "verdana";
        public SKShader NeedleShader { get; set; } = SKShader.CreateColor(SKColors.LightBlue);
        public float NeedleStartWidth { get; set; } = 6f;
        public float NeedleEndWidth { get; set; } = 3f;
        public float NeedleStartExtent { get; set; } = 0f;
        public float NeedleEndExtent { get; set; } = 0.71f;
        public float SetNeedleValue { get; set; }
        public SKShader SetNeedleShader { get; set; }
        public float SetNeedleStartWidth { get; set; } = 6f;
        public float SetNeedleEndWidth { get; set; } = 3f;
        public float SetNeedleStartExtent { get; set; } = 0f;
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