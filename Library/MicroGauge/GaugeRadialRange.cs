using SkiaSharp;

namespace MicroGauge
{
    public class GaugeRadialRange
    {


        /// <summary>
        ///     Brush - background drawn behind tick scale
        /// </summary>
        public GaugeBrush Brush { get; set; } = GaugeBrushes.Transparent;

        private string _brushHex = "#00FFFFFF";
        public string BrushHex
        {
            get => _brushHex;
            set {
                if (value == null) value = "#00FFFFFF";
                if (SKColor.TryParse(value, out var color))
                    Brush = new GaugeBrush(color);
                _brushHex = value;
            }
        }

        /// <summary>
        ///     StartValue - value range start
        /// </summary>
        public float StartValue { get; set; } = float.MinValue;

        /// <summary>
        ///     EndValue - value range end
        /// </summary>
        public float EndValue { get; set; } = float.MinValue;

        /// <summary>
        ///     InnerStartExtent - Drawing range inner boundary start at extent of _radius
        /// </summary>
        public float InnerStartExtent { get; set; }

        /// <summary>
        ///     InnerEndExtent - Drawing range inner boundary end at extent of _radius
        /// </summary>
        public float InnerEndExtent { get; set; }

        /// <summary>
        ///     OuterStartExtent - Drawing range outer boundary start at extent of _radius
        /// </summary>
        public float OuterStartExtent { get; set; }

        /// <summary>
        ///     OuterEndExtent - Drawing range outer boundary end at extent of _radius
        /// </summary>
        public float OuterEndExtent { get; set; }
    }
}