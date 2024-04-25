using Xamarin.Forms;

namespace MicroGauge.Forms
{
    public class XfGaugeLinear : XfGaugeBase
    {
        #region Constructor

        public XfGaugeLinear()
        {
            Gauge = new GaugeLinear();
            Gauge.DimensionsUpdated += OnDimensionsUpdated;
            PaintSurface += OnPaintCanvas;
        }

        #endregion

        #region Dimensions Update

        /// <summary>
        ///     OnDimensionsUpdated - calculate shaders (in case linear gradient)
        /// </summary>
        private void OnDimensionsUpdated()
        {
            var linearGauge = (GaugeLinear)Gauge;
            linearGauge.ValueBarShader = GetSkShader(Gauge, ValueBarBrush);
            UpdateShaders(linearGauge);
        }

        #endregion

        #region Gauge Specific Properties

        /// <summary>
        ///     IsVertical
        /// </summary>
        public bool IsVertical
        {
            get => (bool)GetValue(IsVerticalProperty);
            set => SetValue(IsVerticalProperty, value);
        }

        public static readonly BindableProperty IsVerticalProperty = Create(nameof(IsVertical),
            typeof(bool), false,
            (gaugeBase, newValue) => { GetLinear(gaugeBase).IsVertical = (bool)newValue; });

        /// <summary>
        ///     ValueWidthExtent
        /// </summary>
        public float ValueWidthExtent
        {
            get => (float)GetValue(ValueWidthExtentProperty);
            set => SetValue(ValueWidthExtentProperty, value);
        }

        public static readonly BindableProperty ValueWidthExtentProperty = Create(nameof(ValueWidthExtent),
            typeof(float), 0.5f,
            (gaugeBase, newValue) => { GetLinear(gaugeBase).ValueWidthExtent = (float)newValue; });

        /// <summary>
        ///     TickWidthExtent
        /// </summary>
        public float TickWidthExtent
        {
            get => (float)GetValue(TickWidthExtentProperty);
            set => SetValue(TickWidthExtentProperty, value);
        }

        public static readonly BindableProperty TickWidthExtentProperty = Create(nameof(TickWidthExtent),
            typeof(float), 0.7f,
            (gaugeBase, newValue) => { GetLinear(gaugeBase).TickWidthExtent = (float)newValue; });

        /// <summary>
        ///     MinorTickWidthExtent
        /// </summary>
        public float MinorTickWidthExtent
        {
            get => (float)GetValue(MinorTickWidthExtentProperty);
            set => SetValue(MinorTickWidthExtentProperty, value);
        }

        public static readonly BindableProperty MinorTickWidthExtentProperty = Create(nameof(MinorTickWidthExtent),
            typeof(float), 0.5f,
            (gaugeBase, newValue) => { GetLinear(gaugeBase).MinorTickWidthExtent = (float)newValue; });

        /// <summary>
        ///     ValueBarBrush
        /// </summary>
        public Brush ValueBarBrush
        {
            get => (Brush)GetValue(ValueBarBrushProperty);
            set => SetValue(ValueBarBrushProperty, value);
        }

        public static readonly BindableProperty ValueBarBrushProperty = Create(nameof(ValueBarBrush),
            typeof(Brush), new SolidColorBrush(Color.Black),
            (gaugeBase, newValue) =>
            {
                GetLinear(gaugeBase).ValueBarShader = GetSkShader(GetLinear(gaugeBase), (Brush)newValue);
            });

        #endregion

        #region Helper

        /// <summary>
        ///     GaugeLinear - Get Linear Gauge from gauge base
        /// </summary>
        private static GaugeLinear GetLinear(XfGaugeBase gaugeBase)
        {
            return (GaugeLinear)gaugeBase.Gauge;
        }

        #endregion
    }
}