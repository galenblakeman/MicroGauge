using SkiaSharp;

namespace MicroGauge
{
    public class GaugeBrushStop
    {
        public SKColor Color { get; set; }
        public float Offset { get; set; }

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
    }
}