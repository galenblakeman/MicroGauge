using System.Collections.Generic;
using SkiaSharp;

// ReSharper disable UnusedMember.Global

namespace MicroGauge
{
    public class GaugeBrush
    {
        public SKPoint StartPoint { get; set; } = new SKPoint(0, 0);
        public SKPoint EndPoint { get; set; } = new SKPoint(1, 0);
        public List<GaugeBrushStop> GradientStops { get; set; } = new List<GaugeBrushStop>();

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
    }
}