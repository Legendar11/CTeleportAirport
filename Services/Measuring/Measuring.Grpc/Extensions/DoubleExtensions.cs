using System;

namespace Measuring.Grpc.Extensions
{
    public static class DoubleExtensions
    {
        public static double DegreesToRadians(this double deg) => deg * Math.PI / 180.0;

        public static double RadiansToDegrees(this double rad) => rad / Math.PI * 180.0;

        public static double Sin(this double num) => Math.Sin(num);

        public static double Cos(this double num) => Math.Cos(num);

    }
}
