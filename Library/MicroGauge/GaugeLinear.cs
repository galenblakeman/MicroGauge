using System;
using MicroGauge.Constant;
using SkiaSharp;

// ReSharper disable PossibleLossOfFraction

namespace MicroGauge
{
    public class GaugeLinear : GaugeBase
    {
        #region Properties

        /// <summary>
        ///     IsVertical - True draw bar vertical, else horizontal
        /// </summary>
        public bool IsVertical { get; set; }

        /// <summary>
        ///     ValueWidthExtent - percent of surface width for value bar
        /// </summary>
        public float ValueWidthExtent { get; set; } = 0.5f;

        /// <summary>
        ///     TickWidthExtent - percent of bar width for major ticks
        /// </summary>
        public float TickWidthExtent { get; set; } = 0.7f;

        /// <summary>
        ///     MinorTickWidthExtent - percent of bar width for minor ticks
        /// </summary>
        public float MinorTickWidthExtent { get; set; } = 0.5f;

        /// <summary>
        ///     ValueBarShader - shader for value bar
        /// </summary>
        public SKShader ValueBarShader { get; set; } = SKShader.CreateColor(SKColors.Black);


        /// <summary>
        ///     _start - If vertical middle height, else middle width
        /// </summary>
        private SKPoint _start;

        /// <summary>
        ///     _barStart - If vertical bottom center, else left center
        /// </summary>
        private SKPoint _barStart;

        /// <summary>
        ///     _horizontal - surface width as float
        /// </summary>
        private float _horizontal;

        /// <summary>
        ///     _vertical - surface height as float
        /// </summary>
        private float _vertical;

        /// <summary>
        ///     _barWidth - If vertical _horizontal, else _vertical minus extents
        /// </summary>
        private float _barWidth;

        /// <summary>
        ///     _barLength - If vertical _vertical, else _horizontal minus extents
        /// </summary>
        private float _barLength;

        #endregion

        #region Draw

        /// <summary>
        ///     DrawContent
        /// </summary>
        public override void DrawContent()
        {
            Canvas.Clear();
            //DebugFillCanvas(SKColors.Green);
            CalcDimensions();
            DrawGaugeArea();
            DrawValueBar();
            var minorTicks = GaugeHelper.GetTicks(MinValue, MaxValue, MinorTickInterval);
            DrawTicks(minorTicks, false);
            var majorTicks = GaugeHelper.GetTicks(MinValue, MaxValue, TickInterval);
            DrawTicks(majorTicks, true);
            DrawNeedle();
            if (SetNeedleShader != null)
                DrawSetNeedle();
            DrawLabelNumbers();
            DrawValueLabel();
        }

        /// <summary>
        ///     DrawGaugeArea - draw background area
        /// </summary>
        private void DrawGaugeArea()
        {
            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = BackingStrokeWidth;
                paint.Shader = BackingOutlineShader;
                paint.IsAntialias = true;
                var width = IsVertical ? Convert.ToSingle(SurfaceWidth) : Convert.ToSingle(SurfaceHeight);
                var length = IsVertical ? Convert.ToSingle(SurfaceHeight) : Convert.ToSingle(SurfaceWidth);
                var start = new SKPoint(_start.X, _start.Y);
                DrawBar(paint, start, width, length);
                if (IsVertical)
                    start.Y -= BackingStrokeWidth / 2;
                else
                    start.X += BackingStrokeWidth / 2;
                width -= BackingStrokeWidth;
                length -= BackingStrokeWidth;
                paint.Style = SKPaintStyle.Fill;
                paint.Shader = BackingShader;
                DrawBar(paint, start, width, length);
                //DebugCircleAtPoint(start, SKColors.White);
            }
        }

        private void DrawValueBar()
        {
            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Fill;
                paint.Shader = ValueBarShader;
                DrawBar(paint, _barStart, _barWidth * ValueWidthExtent, Convert.ToSingle(GetValuePosition(Value)));
            }
        }

        private void DrawBar(SKPaint paint, SKPoint start, float width, float length)
        {
            using (var path = new SKPath())
            {
                if (IsVertical)
                {
                    var p1 = GaugeHelper.GetRadialPoint(start, width / 2, 180);
                    var p2 = GaugeHelper.GetRadialPoint(p1, length, 90);
                    var p3 = GaugeHelper.GetRadialPoint(p2, width, 0);
                    var p4 = GaugeHelper.GetRadialPoint(p3, length, 270);
                    DrawPoly(paint, path, p1, p2, p3, p4);
                }
                else
                {
                    var p1 = GaugeHelper.GetRadialPoint(start, width / 2, 90);
                    var p2 = GaugeHelper.GetRadialPoint(p1, length, 0);
                    var p3 = GaugeHelper.GetRadialPoint(p2, width, 270);
                    var p4 = GaugeHelper.GetRadialPoint(p3, length, 180);
                    DrawPoly(paint, path, p1, p2, p3, p4);
                }
            }
        }

        /// <summary>
        ///     DrawTicks - draw major or minor ticks
        /// </summary>
        private void DrawTicks(int numberTicks, bool isMajor)
        {
            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = isMajor ? TickStrokeWidth : MinorTickStrokeWidth;
                paint.StrokeCap = SKStrokeCap.Round;
                paint.Shader = isMajor ? TickShader : MinorTickShader;
                paint.IsAntialias = true;
                double tickSpacing = _barLength / (numberTicks - 1);
                for (var i = 0; i < numberTicks; i++) DrawTick(paint, tickSpacing, i, isMajor);
            }
        }

        /// <summary>
        ///     DrawTicks - draw tick as radial line
        /// </summary>
        private void DrawTick(SKPaint paint, double tickSpacing, int i, bool isMajor)
        {
            var angle = GetBarAngle();
            var point = GetTickPoint(tickSpacing, i);
            var extent = isMajor ? TickWidthExtent : MinorTickWidthExtent;
            var start = GaugeHelper.GetRadialPoint(point, _barWidth * extent / 2, angle);
            var end = GaugeHelper.GetRadialPoint(point, -_barWidth * extent / 2, angle);
            Canvas.DrawLine(start, end, paint);
        }

        /// <summary>
        ///     DrawNeedle - draw needle using extent
        /// </summary>
        private void DrawNeedle()
        {
            var location = Convert.ToSingle(GetValuePosition(Value));
            var angle = GetBarAngle();
            var point = GetBarStartCenterPoint(location);

            var start = GaugeHelper.GetRadialPoint(point, _barWidth * NeedleStartExtent, angle);
            var end = GaugeHelper.GetRadialPoint(point, _barWidth * NeedleEndExtent, angle);
            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Fill;
                paint.IsAntialias = true;
                paint.Shader = NeedleShader;
                DrawNeedlePoly(paint, start, end, NeedleStartWidth, NeedleEndWidth, angle);
            }
        }

        /// <summary>
        ///     DrawSetNeedle - draw set needle using extent
        /// </summary>
        private void DrawSetNeedle()
        {
            var location = Convert.ToSingle(GetValuePosition(SetNeedleValue));
            var angle = GetBarAngle();
            var point = GetBarStartCenterPoint(location);

            var start = GaugeHelper.GetRadialPoint(point, _barWidth * SetNeedleStartExtent, angle);
            var end = GaugeHelper.GetRadialPoint(point, _barWidth * SetNeedleEndExtent, angle);
            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Fill;
                paint.IsAntialias = true;
                paint.Shader = SetNeedleShader;
                DrawNeedlePoly(paint, start, end, SetNeedleStartWidth, SetNeedleEndWidth, angle);
            }
        }


        /// <summary>
        ///     DrawLabelNumbers - draw number labels at intervals
        /// </summary>
        private void DrawLabelNumbers()
        {
            var majorTicks = GaugeHelper.GetTicks(MinValue, MaxValue, LabelInterval);
            double tickSpacing = _barLength / (majorTicks - 1);

            using (var paint = new SKPaint())
            {
                SetLabelPaint(paint);
                for (var i = 0; i < majorTicks; i++) DrawLabelNumber(paint, tickSpacing, i);
            }
        }

        /// <summary>
        ///     DrawLabelNumber - draw number label at position
        /// </summary>
        private void DrawLabelNumber(SKPaint paint, double tickSpacing, int i)
        {
            var angle = GetBarAngle();
            var point = GetTickPoint(tickSpacing, i);
            var label = GetLabelFormattedValue(MinValue + i * LabelInterval);
            point.Y += GaugeHelper.GetTextBounds(paint, label).Height / 2;
            var labelPoint = GaugeHelper.GetRadialPoint(point, _barWidth * LabelExtent / 2, angle);
            Canvas.DrawText(label, labelPoint, paint);
        }

        /// <summary>
        ///     DrawValueLabel - draw value label
        /// </summary>
        private void DrawValueLabel()
        {
            using (var paint = new SKPaint())
            {
                SetValuePaint(paint);
                var point = new SKPoint(_start.X, _start.Y);
                //DebugCircleAtPoint(point, SKColors.Red);

                if (IsVertical)
                    switch (ValueLocation)
                    {
                        case GaugeValueLocation.TopCenter:
                            point.Y = 0 + _vertical * ValueExtent;
                            break;
                        case GaugeValueLocation.LeftCenter:
                            point.X -= _horizontal * ValueExtent;
                            point.Y -= _vertical / 2;
                            break;
                        case GaugeValueLocation.RightCenter:
                            point.X += _horizontal * ValueExtent;
                            point.Y -= _vertical / 2;
                            break;
                        case GaugeValueLocation.BottomCenter:
                        default:
                            point.Y -= _vertical * ValueExtent;
                            break;
                    }
                else
                    switch (ValueLocation)
                    {
                        case GaugeValueLocation.TopCenter:
                            point.X += _horizontal / 2;
                            point.Y = 0 + _vertical * ValueExtent;
                            break;
                        case GaugeValueLocation.LeftCenter:
                            point.X += _horizontal * ValueExtent;
                            break;
                        case GaugeValueLocation.RightCenter:
                            point.X = _horizontal - _horizontal * ValueExtent;
                            break;
                        case GaugeValueLocation.BottomCenter:
                        default:
                            point.X += _horizontal / 2;
                            point.Y += _vertical * ValueExtent;
                            break;
                    }

                var valueStr = GetValueFormattedValue(Convert.ToSingle(Value));
                point.Y += GaugeHelper.GetTextBounds(paint, valueStr).Height / 2;
                Canvas.DrawText(valueStr, point, paint);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        ///     CalcDimensions - calc radius and center
        /// </summary>
        private void CalcDimensions()
        {
            _vertical = Convert.ToSingle(SurfaceHeight);
            _horizontal = Convert.ToSingle(SurfaceWidth);
            if (IsVertical)
            {
                _start = new SKPoint(SurfaceWidth / 2, Convert.ToSingle(SurfaceHeight));
                _barWidth = _horizontal - _horizontal * LeftExtent - _horizontal * RightExtent;
                _barLength = _vertical - _vertical * TopExtent - _vertical * BottomExtent;
                _barStart = new SKPoint(_start.X, _start.Y - _vertical * BottomExtent);
            }
            else
            {
                _start = new SKPoint(0, SurfaceHeight / 2);
                _barWidth = _vertical - _vertical * TopExtent - _vertical * BottomExtent;
                _barLength = _horizontal - _horizontal * LeftExtent - _horizontal * RightExtent;
                _barStart = new SKPoint(_start.X + _horizontal * LeftExtent, _start.Y);
            }

            GradientOffset = _barStart;
            GradientWidth = _barWidth;
            GradientHeight = _barLength;
            DimensionsUpdate();
        }

        /// <summary>
        ///     GetValuePosition
        /// </summary>
        private double GetValuePosition(double value)
        {
            double valueRange = MaxValue - MinValue;
            var valueRatio = (value - MinValue) / valueRange;
            return _barLength * valueRatio;
        }

        /// <summary>
        ///     GetBarStartPoint - Start of bar centered based on IsVertical
        /// </summary>
        private SKPoint GetBarStartCenterPoint(float location)
        {
            var point = new SKPoint(_barStart.X, _barStart.Y);
            if (IsVertical)
            {
                point.X -= _barWidth / 2;
                point.Y -= location;
            }
            else
            {
                point.X += location;
                point.Y += _barWidth / 2;
            }

            return point;
        }

        /// <summary>
        ///     GetTickPoint - Get tick point based on IsVertical
        /// </summary>
        private SKPoint GetTickPoint(double tickSpacing, int i)
        {
            var point = new SKPoint(_barStart.X, _barStart.Y);
            if (IsVertical)
                point.Y -= Convert.ToSingle(tickSpacing * i);
            else
                point.X += Convert.ToSingle(tickSpacing * i);

            return point;
        }

        /// <summary>
        ///     GetBarAngle - 0 if IsVertical, else 90
        /// </summary>
        /// <returns></returns>
        private float GetBarAngle()
        {
            return IsVertical ? 0 : 90;
        }

        #endregion
    }
}