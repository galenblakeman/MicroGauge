using System.Collections.Generic;
using MicroGauge.Constant;
using Xamarin.Forms;

namespace MicroGauge.Forms
{
    /// <summary>
    ///     XfGaugeRadial - Radial Gauge with tags and bindings for Xamarin Forms
    /// </summary>
    public class XfGaugeRadial : XfGaugeBase
    {
        #region Constructor

        /// <summary>
        ///     Constructor
        /// </summary>
        public XfGaugeRadial()
        {
            Gauge = new GaugeRadial();
            PaintSurface += OnPaintCanvas;
        }

        #endregion


        #region Gauge Specific Properties

        /// <summary>
        ///     RadialStyle
        /// </summary>
        public GaugeRadialStyle RadialStyle
        {
            get => (GaugeRadialStyle)GetValue(RadialStyleProperty);
            set => SetValue(RadialStyleProperty, value);
        }

        public static readonly BindableProperty RadialStyleProperty = Create(nameof(RadialStyle),
            typeof(GaugeRadialStyle), GaugeRadialStyle.Full,
            (gaugeBase, newValue) => { GetRadial(gaugeBase).RadialStyle = (GaugeRadialStyle)newValue; });

        /// <summary>
        ///     ScaleStartAngle
        /// </summary>
        public float ScaleStartAngle
        {
            get => (float)GetValue(ScaleStartAngleProperty);
            set => SetValue(ScaleStartAngleProperty, value);
        }

        public static readonly BindableProperty ScaleStartAngleProperty = Create(nameof(ScaleStartAngle),
            typeof(float), 210f,
            (gaugeBase, newValue) => { GetRadial(gaugeBase).ScaleStartAngle = (float)newValue; });

        /// <summary>
        ///     ScaleEndAngle
        /// </summary>
        public float ScaleEndAngle
        {
            get => (float)GetValue(ScaleEndAngleProperty);
            set => SetValue(ScaleEndAngleProperty, value);
        }

        public static readonly BindableProperty ScaleEndAngleProperty = Create(nameof(ScaleEndAngle),
            typeof(float), 330f,
            (gaugeBase, newValue) => { GetRadial(gaugeBase).ScaleEndAngle = (float)newValue; });

        /// <summary>
        ///     TickStartExtent
        /// </summary>
        public float TickStartExtent
        {
            get => (float)GetValue(TickStartExtentProperty);
            set => SetValue(TickStartExtentProperty, value);
        }

        public static readonly BindableProperty TickStartExtentProperty = Create(nameof(TickStartExtent),
            typeof(float), 0.6f,
            (gaugeBase, newValue) => { GetRadial(gaugeBase).TickStartExtent = (float)newValue; });

        /// <summary>
        ///     TickEndExtent
        /// </summary>
        public float TickEndExtent
        {
            get => (float)GetValue(TickEndExtentProperty);
            set => SetValue(TickEndExtentProperty, value);
        }

        public static readonly BindableProperty TickEndExtentProperty = Create(nameof(TickEndExtent),
            typeof(float), 0.7f,
            (gaugeBase, newValue) => { GetRadial(gaugeBase).TickEndExtent = (float)newValue; });

        /// <summary>
        ///     MinorTickStartExtent
        /// </summary>
        public float MinorTickStartExtent
        {
            get => (float)GetValue(MinorTickStartExtentProperty);
            set => SetValue(MinorTickStartExtentProperty, value);
        }

        public static readonly BindableProperty MinorTickStartExtentProperty = Create(nameof(MinorTickStartExtent),
            typeof(float), 0.65f,
            (gaugeBase, newValue) => { GetRadial(gaugeBase).MinorTickStartExtent = (float)newValue; });

        /// <summary>
        ///     MinorTickEndExtent
        /// </summary>
        public float MinorTickEndExtent
        {
            get => (float)GetValue(MinorTickEndExtentProperty);
            set => SetValue(MinorTickEndExtentProperty, value);
        }

        public static readonly BindableProperty MinorTickEndExtentProperty = Create(nameof(MinorTickEndExtent),
            typeof(float), 0.7f,
            (gaugeBase, newValue) => { GetRadial(gaugeBase).MinorTickEndExtent = (float)newValue; });

        /// <summary>
        ///     NeedlePivotEndExtent
        /// </summary>
        public float NeedlePivotEndExtent
        {
            get => (float)GetValue(NeedlePivotEndExtentProperty);
            set => SetValue(NeedlePivotEndExtentProperty, value);
        }

        public static readonly BindableProperty NeedlePivotEndExtentProperty = Create(nameof(NeedlePivotEndExtent),
            typeof(float), 0.1f,
            (gaugeBase, newValue) => { GetRadial(gaugeBase).NeedlePivotEndExtent = (float)newValue; });

        /// <summary>
        ///     NeedlePivotBrush
        /// </summary>
        public Brush NeedlePivotBrush
        {
            get => (Brush)GetValue(NeedlePivotBrushProperty);
            set => SetValue(NeedlePivotBrushProperty, value);
        }

        public static readonly BindableProperty NeedlePivotBrushProperty = Create(nameof(NeedlePivotBrush),
            typeof(Brush), new SolidColorBrush(Color.LightGray),
            (gaugeBase, newValue) =>
            {
                GetRadial(gaugeBase).NeedlePivotBrush = XfGaugeHelper.GetGaugeBrush((Brush)newValue);
            });

        /// <summary>
        ///     NeedlePivotOutline
        /// </summary>
        public Brush NeedlePivotOutlineBrush
        {
            get => (Brush)GetValue(NeedlePivotOutlineBrushProperty);
            set => SetValue(NeedlePivotOutlineBrushProperty, value);
        }

        public static readonly BindableProperty NeedlePivotOutlineBrushProperty = Create(
            nameof(NeedlePivotOutlineBrush),
            typeof(Brush), new SolidColorBrush(Color.Black),
            (gaugeBase, newValue) =>
            {
                GetRadial(gaugeBase).NeedlePivotOutlineBrush = XfGaugeHelper.GetGaugeBrush((Brush)newValue);
            });


        /// <summary>
        ///     NeedlePivotOutlineWidth
        /// </summary>
        public float NeedlePivotOutlineWidth
        {
            get => (float)GetValue(NeedlePivotOutlineWidthProperty);
            set => SetValue(NeedlePivotOutlineWidthProperty, value);
        }

        public static readonly BindableProperty NeedlePivotOutlineWidthProperty = Create(
            nameof(NeedlePivotOutlineWidth),
            typeof(float), 2f,
            (gaugeBase, newValue) => { GetRadial(gaugeBase).NeedlePivotOutlineWidth = (float)newValue; });

        /// <summary>
        ///     RangeBrush
        /// </summary>
        public Brush RangeBrush
        {
            get => (Brush)GetValue(RangeBrushProperty);
            set => SetValue(RangeBrushProperty, value);
        }

        public static readonly BindableProperty RangeBrushProperty = Create(nameof(RangeBrush),
            typeof(Brush), new SolidColorBrush(Color.Transparent),
            (gaugeBase, newValue) =>
            {
                GetRadialRange(gaugeBase).Brush = XfGaugeHelper.GetGaugeBrush((Brush)newValue);
            });

        /// <summary>
        ///     RangeInnerStartExtent
        /// </summary>
        public float RangeInnerStartExtent
        {
            get => (float)GetValue(RangeInnerStartExtentProperty);
            set => SetValue(RangeInnerStartExtentProperty, value);
        }

        public static readonly BindableProperty RangeInnerStartExtentProperty = Create(nameof(RangeInnerStartExtent),
            typeof(float), 0f,
            (gaugeBase, newValue) => { GetRadialRange(gaugeBase).InnerStartExtent = (float)newValue; });

        /// <summary>
        ///     RangeInnerEndExtent
        /// </summary>
        public float RangeInnerEndExtent
        {
            get => (float)GetValue(RangeInnerEndExtentProperty);
            set => SetValue(RangeInnerEndExtentProperty, value);
        }

        public static readonly BindableProperty RangeInnerEndExtentProperty = Create(nameof(RangeInnerEndExtent),
            typeof(float), 0f,
            (gaugeBase, newValue) => { GetRadialRange(gaugeBase).InnerEndExtent = (float)newValue; });

        /// <summary>
        ///     RangeOuterStartExtent
        /// </summary>
        public float RangeOuterStartExtent
        {
            get => (float)GetValue(RangeOuterStartExtentProperty);
            set => SetValue(RangeOuterStartExtentProperty, value);
        }

        public static readonly BindableProperty RangeOuterStartExtentProperty = Create(nameof(RangeOuterStartExtent),
            typeof(float), 0f,
            (gaugeBase, newValue) => { GetRadialRange(gaugeBase).OuterStartExtent = (float)newValue; });

        /// <summary>
        ///     RangeOuterEndExtent
        /// </summary>
        public float RangeOuterEndExtent
        {
            get => (float)GetValue(RangeOuterEndExtentProperty);
            set => SetValue(RangeOuterEndExtentProperty, value);
        }

        public static readonly BindableProperty RangeOuterEndExtentProperty = Create(nameof(RangeOuterEndExtent),
            typeof(float), 0f,
            (gaugeBase, newValue) => { GetRadialRange(gaugeBase).OuterEndExtent = (float)newValue; });

        /// <summary>
        ///     Ranges
        /// </summary>
        public List<GaugeRadialRange> Ranges
        {
            get => GetRadial(this).Ranges;
            set => GetRadial(this).Ranges = value;
        }

        #endregion

        #region Helper

        /// <summary>
        ///     GetRadial - Get Radial Gauge from gauge base
        /// </summary>
        private static GaugeRadial GetRadial(XfGaugeBase gaugeBase)
        {
            return (GaugeRadial)gaugeBase.Gauge;
        }
        /// <summary>
        ///     GetRadialRange - Get Radial Range from gauge base (first range)
        /// </summary>
        private static GaugeRadialRange GetRadialRange(XfGaugeBase gaugeBase)
        {
            GaugeRadial radialGauge = GetRadial(gaugeBase);
            if (radialGauge.Ranges.Count == 1) return radialGauge.Ranges[0];
            radialGauge.Ranges.Clear();
            radialGauge.Ranges.Add(new GaugeRadialRange());
            return radialGauge.Ranges[0];
        }
        #endregion
    }
}