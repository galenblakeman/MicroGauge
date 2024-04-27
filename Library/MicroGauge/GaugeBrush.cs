using System.Collections.Generic;
using SkiaSharp;

// ReSharper disable UnusedMember.Global

namespace MicroGauge
{
    /// <summary>
    ///     GaugeBrush - Cross-platform brush that supports solid and linear brushes
    /// </summary>
    public class GaugeBrush
    {
        #region Properties

        /// <summary>
        ///     StartPoint - Start point used for linear gradient
        /// </summary>
        public SKPoint StartPoint { get; set; } = new SKPoint(0, 0);

        /// <summary>
        ///     EndPoint - End point used for linear gradient
        /// </summary>
        public SKPoint EndPoint { get; set; } = new SKPoint(1, 0);

        /// <summary>
        ///     GradientStops - Used for gradient brushes to define color stops
        /// </summary>
        public List<GaugeBrushStop> GradientStops { get; set; } = new List<GaugeBrushStop>();

        #endregion

        #region Constructor

        /// <summary>
        ///     Constructor - Empty
        /// </summary>
        public GaugeBrush()
        {
        }

        /// <summary>
        ///     Constructor - Start & End Points
        /// </summary>
        public GaugeBrush(SKPoint startPoint, SKPoint endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        /// <summary>
        ///     Constructor - Solid Color
        /// </summary>
        public GaugeBrush(SKColor color)
        {
            GradientStops = new List<GaugeBrushStop>
            {
                new GaugeBrushStop(color, 0),
                new GaugeBrushStop(color, 1)
            };
        }

        /// <summary>
        ///     Constructor - List Gradient Stops
        /// </summary>
        public GaugeBrush(List<GaugeBrushStop> gradientStops)
        {
            GradientStops = gradientStops;
        }

        /// <summary>
        ///     AddStop - Chainable Add Gradient Stop
        /// </summary>
        public GaugeBrush AddStop(GaugeBrushStop gradientStop)
        {
            GradientStops.Add(gradientStop);
            return this;
        }

        #endregion
    }
}