using System;
using MicroGauge.Constant;
using SkiaSharp;

// ReSharper disable PossibleLossOfFraction

namespace MicroGauge
{
    public class GaugeRadial : GaugeBase
    {
        #region Properties

        /// <summary>
        ///     RadialStyle - 0 = Full, 1 = Half, 2 = Notch
        /// </summary>
        public GaugeRadialStyle RadialStyle { get; set; }

        /// <summary>
        /// 	ScaleStartAngle - start angle of scale
        /// </summary>
        public float ScaleStartAngle { get; set; } = 210f;

        /// <summary>
        /// 	ScaleEndAngle - stop angle of scale
        /// </summary>
        public float ScaleEndAngle { get; set; } = 330f;

        /// <summary>
        /// 	TickStartExtent - tick start extent of _radius
        /// </summary>
        public float TickStartExtent { get; set; } = 0.6f;

        /// <summary>
        /// 	TickEndExtent - tick end extent of _radius
        /// </summary>
        public float TickEndExtent { get; set; } = 0.7f;

        /// <summary>
        /// 	MinorTickStartExtent - minor tick start extent of _radius
        /// </summary>
        public float MinorTickStartExtent { get; set; } = 0.65f;

        /// <summary>
        /// 	MinorTickEndExtent - minor tick end extent of _radius
        /// </summary>
        public float MinorTickEndExtent { get; set; } = 0.7f;

        /// <summary>
        /// 	NeedlePivotEndExtent - pivot end extent of _radius
        /// </summary>
        public float NeedlePivotEndExtent { get; set; } = 0.1f;

        /// <summary>
        /// 	NeedlePivotShader - background of needle pivot
        /// </summary>
        public SKShader NeedlePivotShader { get; set; } = SKShader.CreateColor(SKColors.LightGray);

        /// <summary>
        /// 	NeedlePivotOutlineShader - background of needle pivot outline
        /// </summary>
        public SKShader NeedlePivotOutlineShader { get; set; } = SKShader.CreateColor(SKColors.Black);

        /// <summary>
        /// 	RangeShader - background drawn behind tick scale
        /// </summary>
        public SKShader RangeShader { get; set; } = SKShader.CreateColor(SKColors.Transparent);

        /// <summary>
        /// 	RangeInnerStartExtent - Drawing range inner boundary start at extent of _radius
        /// </summary>
        public float RangeInnerStartExtent { get; set; }

        /// <summary>
        /// 	RangeInnerEndExtent - Drawing range inner boundary end at extent of _radius
        /// </summary>
        public float RangeInnerEndExtent { get; set; }

        /// <summary>
        /// 	RangeOuterStartExtent - Drawing range outer boundary start at extent of _radius
        /// </summary>
        public float RangeOuterStartExtent { get; set; }

        /// <summary>
        /// 	RangeOuterEndExtent - Drawing range outer boundary end at extent of _radius
        /// </summary>
        public float RangeOuterEndExtent { get; set; }

        /// <summary>
        /// 	Fidelity - fidelity of radial angles
        /// </summary>
        private const double Fidelity = 1;

        /// <summary>
        /// 	_center - if half, bottom center, else center with extend adjustments 
        /// </summary>
        private SKPoint _center;

        /// <summary>
        /// 	_radius - If half, min of surface width or height, else min of surface width or height / 2
        /// </summary>
        private float _radius;

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
            DrawRange();
            int minorTicks = GaugeHelper.GetTicks(MinValue, MaxValue, MinorTickInterval);
            DrawTicks(minorTicks, false);
            int majorTicks = GaugeHelper.GetTicks(MinValue, MaxValue, TickInterval);
            DrawTicks(majorTicks, true);
            DrawNeedle();
            DrawLabelNumbers();
            DrawValueLabel();
        }

        /// <summary>
        ///     DrawGaugeArea - draw background area
        /// </summary>
        private void DrawGaugeArea()
        {
            using (SKPaint paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Fill;
                paint.IsAntialias = true;
                paint.Shader = BackingOutlineShader;
                DrawGaugeArea(paint, _radius);
                paint.Shader = BackingShader;
                DrawGaugeArea(paint, _radius - BackingStrokeWidth);
            }
        }

        /// <summary>
        ///     DrawGaugeArea - draw background area using paint
        /// </summary>
        private void DrawGaugeArea(SKPaint paint, float radius)
        {
            switch (RadialStyle)
            {
                case GaugeRadialStyle.Half:
                    DrawGaugeFitted(paint, radius, 0);
                    break;
                case GaugeRadialStyle.Notch:
                    float leftAngle = Math.Abs(ScaleStartAngle - 180);
                    float rightAngle = Math.Abs(360 - ScaleEndAngle);
                    float maxAngle = Math.Max(leftAngle, rightAngle);
                    DrawGaugeFitted(paint, radius, maxAngle / 2);
                    break;
                case GaugeRadialStyle.Full:
                default:
                    Canvas.DrawCircle(_center, radius, paint);
                    break;
            }
        }

        private void DrawGaugeFitted(SKPaint paint, float radius, float anglePadding)
        {
            using (SKPath path = new SKPath())
            {
                double startAngle = GetValueAngle(MinValue);
                double endAngle = GetValueAngle(MaxValue);
                double angleRange = Math.Abs(startAngle - endAngle);
                double angleDelta = 0;
                double angle = startAngle;
                SKPoint point;
                if (anglePadding > 0)
                {
                    point = GaugeHelper.GetRadialPoint(_center, Convert.ToSingle(radius), angle + anglePadding);
                    path.MoveTo(point);
                }
                else
                {
                    point = GaugeHelper.GetRadialPoint(_center, radius, angle);
                    path.MoveTo(point);
                }


                while (angleDelta <= angleRange)
                {
                    point = GaugeHelper.GetRadialPoint(_center, Convert.ToSingle(radius), angle);
                    path.LineTo(point);
                    angle -= Fidelity;
                    angleDelta += Fidelity;
                }

                if (anglePadding > 0)
                {
                    angle += Fidelity;
                    DrawFittedNotch(radius, anglePadding, angle, path, startAngle);
                    DrawFittedNotchLine(radius, anglePadding, angle, startAngle);
                }
                path.Close();
                Canvas.DrawPath(path, paint);
            }
        }

        private void DrawFittedNotch(float radius, float anglePadding, double angle, SKPath path, double startAngle)
        {
            SKPoint point = GaugeHelper.GetRadialPoint(_center, Convert.ToSingle(radius), angle - anglePadding);
            path.LineTo(point);
            point = GaugeHelper.GetRadialPoint(_center, Convert.ToSingle(radius / 2), angle - anglePadding);
            path.LineTo(point);
            point = GaugeHelper.GetRadialPoint(_center, Convert.ToSingle(radius / 2), startAngle + anglePadding);
            path.LineTo(point);

        }

        private void DrawFittedNotchLine(float radius, float anglePadding, double angle, double startAngle)
        {
            using (SKPaint paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Fill;
                paint.IsAntialias = true;
                paint.Shader = BackingOutlineShader;
                paint.StrokeWidth = BackingStrokeWidth;
                SKPoint startPoint = GaugeHelper.GetRadialPoint(_center, Convert.ToSingle(radius), angle - anglePadding);
                SKPoint endPoint = GaugeHelper.GetRadialPoint(_center, Convert.ToSingle(radius / 2), angle - anglePadding);
                Canvas.DrawLine(startPoint, endPoint, paint);
                startPoint = endPoint;
                endPoint = GaugeHelper.GetRadialPoint(_center, Convert.ToSingle(radius / 2), startAngle + anglePadding);
                Canvas.DrawLine(startPoint, endPoint, paint);
                startPoint = endPoint;
                endPoint = GaugeHelper.GetRadialPoint(_center, Convert.ToSingle(radius), startAngle + anglePadding);
                Canvas.DrawLine(startPoint, endPoint, paint);
            }
        }

        /// <summary>
        ///     DrawRanges - Draw range path using extents
        /// </summary>
        private void DrawRange()
        {
            using (SKPaint paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Fill;
                paint.IsAntialias = true;
                paint.Shader = RangeShader;
                using (SKPath path = new SKPath())
                {
                    double startAngle = GetValueAngle(MinValue);
                    double endAngle = GetValueAngle(MaxValue);
                    double angleRange = Math.Abs(startAngle - endAngle);
                    double angleDelta = 0;
                    double angle = startAngle;
                    DrawRangeInner(path, angleRange, ref angle, ref angleDelta);
                    angle += Fidelity;
                    angleDelta -= Fidelity;
                    DrawRangeOuter(path, angleRange, ref angle, ref angleDelta);
                    path.Close();
                    Canvas.DrawPath(path, paint);
                }
            }
        }

        /// <summary>
        ///     DrawRangeInner - sweep forwards for range inner extent
        /// </summary>
        private void DrawRangeInner(SKPath path, double angleRange, ref double angle, ref double angleDelta)
        {
            double innerRatio = (RangeInnerStartExtent - RangeInnerEndExtent) / angleRange;
            double calcExtent = RangeInnerStartExtent + angleDelta * innerRatio;
            SKPoint start = GaugeHelper.GetRadialPoint(_center, Convert.ToSingle(_radius * calcExtent), angle);
            path.MoveTo(start);
            while (angleDelta <= angleRange)
            {
                calcExtent = RangeInnerStartExtent + angleDelta * innerRatio;
                SKPoint point = GaugeHelper.GetRadialPoint(_center, Convert.ToSingle(_radius * calcExtent), angle);
                path.LineTo(point);
                angle -= Fidelity;
                angleDelta += Fidelity;
            }
        }

        /// <summary>
        ///     DrawRangeOuter - sweep backwards for range outer extent
        /// </summary>
        private void DrawRangeOuter(SKPath path, double angleRange, ref double angle, ref double angleDelta)
        {
            double outerRatio = (RangeOuterStartExtent - RangeOuterEndExtent) / angleRange;
            while (angleDelta >= 0)
            {
                double calcExtent = RangeOuterStartExtent - angleDelta * outerRatio;
                if (calcExtent > 0)
                {
                    SKPoint point = GaugeHelper.GetRadialPoint(_center, Convert.ToSingle(_radius * calcExtent), angle);
                    path.LineTo(point);
                }

                angle += Fidelity;
                angleDelta -= Fidelity;
            }
        }

        /// <summary>
        ///     DrawTicks - draw major or minor ticks
        /// </summary>
        private void DrawTicks(int tickCount, bool isMajor)
        {
            using (SKPaint paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = isMajor ? TickStrokeWidth : MinorTickStrokeWidth;
                paint.StrokeCap = SKStrokeCap.Round;
                paint.Shader = isMajor ? TickShader : MinorTickShader;
                paint.IsAntialias = true;
                double sliceAngle = GetAngleRange(ScaleStartAngle, ScaleEndAngle) / (tickCount - 1);
                for (int i = 0; i < tickCount; i++) DrawTick(paint, sliceAngle, i, isMajor);
            }
        }

        /// <summary>
        ///     DrawTicks - draw tick as radial line
        /// </summary>
        private void DrawTick(SKPaint paint, double sliceAngle, int i, bool isMajor)
        {
            double angle = ScaleStartAngle - i * sliceAngle; //Always clockwise
            if (angle < 0) angle += 360;
            float startExtent = isMajor ? TickStartExtent : MinorTickStartExtent;
            float endExtent = isMajor ? TickEndExtent : MinorTickEndExtent;
            SKPoint start = GaugeHelper.GetRadialPoint(_center, _radius * startExtent, angle);
            SKPoint end = GaugeHelper.GetRadialPoint(_center, _radius * endExtent, angle);
            Canvas.DrawLine(start, end, paint);
        }

        /// <summary>
        ///     DrawNeedle - draw needle using extent
        /// </summary>
        private void DrawNeedle()
        {
            using (SKPaint paint = new SKPaint())
            {
                paint.IsAntialias = true;
                DrawNeedleArm(paint);
                if (SetNeedleShader != null)
                    DrawSetNeedleArm(paint);
                DrawNeedlePivot(paint);
            }
        }

        /// <summary>
        ///     DrawNeedleArm - draw needle arm using extent
        /// </summary>
        private void DrawNeedleArm(SKPaint paint)
        {
            double angle = GetValueAngle(Value);
            SKPoint start = GaugeHelper.GetRadialPoint(_center, _radius * NeedleStartExtent, angle);
            SKPoint end = GaugeHelper.GetRadialPoint(_center, _radius * NeedleEndExtent, angle);
            paint.Style = SKPaintStyle.Fill;
            paint.Shader = NeedleShader;
            DrawNeedlePoly(paint, start, end, NeedleStartWidth, NeedleEndWidth, angle);
        }

        /// <summary>
        ///     DrawSetNeedleArm - draw set needle arm using extent
        /// </summary>
        private void DrawSetNeedleArm(SKPaint paint)
        {
            double angle = GetValueAngle(SetNeedleValue);
            SKPoint start = GaugeHelper.GetRadialPoint(_center, _radius * SetNeedleStartExtent, angle);
            SKPoint end = GaugeHelper.GetRadialPoint(_center, _radius * SetNeedleEndExtent, angle);
            paint.Style = SKPaintStyle.Fill;
            paint.Shader = SetNeedleShader;
            DrawNeedlePoly(paint, start, end, SetNeedleStartWidth, SetNeedleEndWidth, angle);
        }

        /// <summary>
        ///     DrawNeedlePivot - draw needle pivot using extent
        /// </summary>
        private void DrawNeedlePivot(SKPaint paint)
        {
            float pivotRadius = _radius * NeedlePivotEndExtent;
            paint.Style = SKPaintStyle.Fill;
            paint.Shader = NeedlePivotShader;
            Canvas.DrawCircle(_center, pivotRadius, paint);
            paint.Style = SKPaintStyle.Stroke;
            paint.StrokeWidth = 2;
            paint.Shader = NeedlePivotOutlineShader;
            Canvas.DrawCircle(_center, pivotRadius, paint);
        }

        /// <summary>
        ///     DrawLabelNumbers - draw number labels at intervals
        /// </summary>
        private void DrawLabelNumbers()
        {
            int majorTicks = GaugeHelper.GetTicks(MinValue, MaxValue, LabelInterval);
            double sliceAngle = GetAngleRange(ScaleStartAngle, ScaleEndAngle) / (majorTicks - 1);
            using (SKPaint paint = new SKPaint())
            {
                SetLabelPaint(paint);
                for (int i = 0; i < majorTicks; i++)
                    if (DrawLabelNumber(paint, sliceAngle, i))
                        break;
            }
        }

        /// <summary>
        ///     DrawLabelNumber - draw number label radial position
        /// </summary>
        private bool DrawLabelNumber(SKPaint paint, double sliceAngle, int i)
        {
            double angle = ScaleStartAngle - i * sliceAngle; //Always clockwise
            if (angle < 0) angle += 360;
            if (i > 0 && Math.Abs(angle - ScaleStartAngle) < 0.01) return true;
            SKPoint labelPoint = GaugeHelper.GetRadialPoint(_center, _radius * LabelExtent, angle);
            string label = GetLabelFormattedValue(MinValue + i * LabelInterval);
            if (RadialStyle != GaugeRadialStyle.Half)
                labelPoint.Y += GaugeHelper.GetTextBounds(paint, label).Height / 2;
            else 
                labelPoint.Y -= GaugeHelper.GetTextBounds(paint, label).Height / 2;
            Canvas.DrawText(label, labelPoint, paint);
            return false;
        }

        /// <summary>
        ///     DrawValueLabel - draw value label
        /// </summary>
        private void DrawValueLabel()
        {
            using (SKPaint paint = new SKPaint())
            {
                SetValuePaint(paint);
                SKPoint point;
                switch (ValueLocation)
                {
                    case GaugeValueLocation.TopCenter:
                        point = GaugeHelper.GetRadialPoint(_center, _radius * ValueExtent, 90);
                        break;
                    case GaugeValueLocation.LeftCenter:
                        point = GaugeHelper.GetRadialPoint(_center, _radius * ValueExtent, 180);
                        break;
                    case GaugeValueLocation.RightCenter:
                        point = GaugeHelper.GetRadialPoint(_center, _radius * ValueExtent, 0);
                        break;
                    case GaugeValueLocation.BottomCenter:
                    default:
                        point = GaugeHelper.GetRadialPoint(_center, _radius * ValueExtent, 270);
                        break;
                }
                string valueStr = GetValueFormattedValue(Convert.ToSingle(Value));
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
            float verticalPadding = SurfaceHeight * TopExtent + SurfaceHeight * BottomExtent;
            float horizontalPadding = SurfaceWidth * LeftExtent + SurfaceHeight * RightExtent;
            float maxPadding = Math.Max(verticalPadding, horizontalPadding);
            if (RadialStyle != GaugeRadialStyle.Half)
            {
                _radius = Convert.ToSingle(Math.Min(SurfaceWidth, SurfaceHeight)) / 2;
                _radius -= maxPadding / 2;
                _center = new SKPoint(SurfaceWidth / 2, SurfaceHeight / 2);
            }
            else //Half
            {
                float vertical = Convert.ToSingle(SurfaceHeight);
                float horizontal = Convert.ToSingle(SurfaceWidth) / 2;
                _radius = Math.Min(horizontal, vertical);
                _radius -= maxPadding / 2;
                _center = new SKPoint(SurfaceWidth / 2,
                    Convert.ToSingle(SurfaceHeight));
            }

            _center.X += SurfaceWidth * LeftExtent / 2;
            _center.X -= SurfaceWidth * RightExtent / 2;
            _center.Y += SurfaceHeight * TopExtent / 2;
            _center.Y -= SurfaceHeight * BottomExtent / 2;
        }

        /// <summary>
        ///     GetValueAngle
        /// </summary>
        private double GetValueAngle(double value)
        {
            if (value < MinValue) return ScaleStartAngle;
            if (value > MaxValue) return ScaleEndAngle;
            double valueRange = MaxValue - MinValue;
            double angleRange = GetAngleRange(ScaleStartAngle, ScaleEndAngle);
            double valueRatio = (value - MinValue) / valueRange;
            return ScaleStartAngle - valueRatio * angleRange; //Always clockwise
        }

        /// <summary>
        ///     GetAngleRange
        /// </summary>
        private static double GetAngleRange(double startAngle, double endAngle)
        {
            if (Math.Abs(startAngle - endAngle) == 0) return 360;
            if (startAngle < endAngle)
                return 360 - (endAngle - startAngle);
            return startAngle - endAngle;
        }

        #endregion
    }
}