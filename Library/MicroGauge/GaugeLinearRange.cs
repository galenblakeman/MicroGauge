using SkiaSharp;

namespace MicroGauge
{
    /// <summary>
    ///     GaugeLinearRange - colored section drawn along the linear gauge bar.
    ///     Extents are signed fractions of the bar width measured from the bar
    ///     centerline, so the bar edges sit at -0.5 and 0.5. Extents are sampled
    ///     at the start and end of the value span; unequal start/end extents
    ///     produce a wedge.
    /// </summary>
    public class GaugeLinearRange
    {
        /// <summary>
        ///     Brush - background drawn behind tick scale
        /// </summary>
        public GaugeBrush Brush { get; set; } = GaugeBrushes.Transparent;

        private string _brushHex = "#00FFFFFF";

        public string BrushHex
        {
            get => _brushHex;
            set
            {
                if (value == null) value = "#00FFFFFF";
                if (SKColor.TryParse(value, out var color))
                    Brush = new GaugeBrush(color);
                _brushHex = value;
            }
        }

        /// <summary>
        ///     StartValue - value range start (null uses gauge MinValue)
        /// </summary>
        public float? StartValue { get; set; }

        /// <summary>
        ///     EndValue - value range end (null uses gauge MaxValue)
        /// </summary>
        public float? EndValue { get; set; }

        /// <summary>
        ///     InnerStartExtent - inner boundary at range start, fraction of bar width from centerline
        /// </summary>
        public float InnerStartExtent { get; set; }

        /// <summary>
        ///     InnerEndExtent - inner boundary at range end, fraction of bar width from centerline
        /// </summary>
        public float InnerEndExtent { get; set; }

        /// <summary>
        ///     OuterStartExtent - outer boundary at range start, fraction of bar width from centerline
        /// </summary>
        public float OuterStartExtent { get; set; }

        /// <summary>
        ///     OuterEndExtent - outer boundary at range end, fraction of bar width from centerline
        /// </summary>
        public float OuterEndExtent { get; set; }
    }
}
