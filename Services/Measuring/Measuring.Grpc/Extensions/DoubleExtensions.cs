using System;

namespace Measuring.Grpc.Extensions
{
    /// <summary>
    /// Math extensions for common double type.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Convert degrees to radians.
        /// </summary>
        public static double DegreesToRadians(this double deg) => deg * Math.PI / 180.0;

        /// <summary>
        /// Convert radians to degrees.
        /// </summary>
        public static double RadiansToDegrees(this double rad) => rad / Math.PI * 180.0;

        /// <summary>
        /// Get sinus.
        /// </summary>
        public static double Sin(this double num) => Math.Sin(num);

        /// <summary>
        /// Get cosinus.
        /// </summary>
        public static double Cos(this double num) => Math.Cos(num);

    }
}
