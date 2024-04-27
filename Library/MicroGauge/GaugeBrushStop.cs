using SkiaSharp;

namespace MicroGauge
{
    /// <summary>
    ///     GaugeBrushStop - Color stop for gradient brushes
    /// </summary>
    public class GaugeBrushStop
    {
        #region Properties

        /// <summary>
        ///     Color - Stop Color
        /// </summary>
        public SKColor Color { get; set; }

        /// <summary>
        ///     Offset - Stop offset
        /// </summary>
        public float Offset { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        ///     Constructor - Empty
        /// </summary>
        public GaugeBrushStop()
        {
        }

        /// <summary>
        ///     Constructor - color offset
        /// </summary>
        public GaugeBrushStop(SKColor color, float offset)
        {
            Color = color;
            Offset = offset;
        }

        #endregion
    }
}