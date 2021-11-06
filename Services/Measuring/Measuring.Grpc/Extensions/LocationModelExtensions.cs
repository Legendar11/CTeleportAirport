using Measuring.Grpc.Protos;

namespace Measuring.Grpc.Extensions
{
    /// <summary>
    /// Validation extensions for LocationModel.
    /// </summary>
    public static class LocationModelExtensions
    {
        /// <summary>
        /// Is values equal.
        /// </summary>
        public static bool IsEqualValue(this LocationModel model, LocationModel other) =>
            model.Longitude == other.Longitude && model.Latitude == other.Latitude;

        /// <summary>
        /// Check correct for latitude property.
        /// </summary>
        public static bool IsCorrectLatitude(this LocationModel model) =>
            !double.IsNaN(model.Latitude) 
            && !double.IsInfinity(model.Latitude)
            && !double.IsNegativeInfinity(model.Latitude)
            && model.Latitude >= -90.0 && model.Latitude <= 90.0;

        /// <summary>
        /// Check correct for longitude property.
        /// </summary>
        public static bool IsCorrectLongitude(this LocationModel model) =>
            !double.IsNaN(model.Longitude)
            && !double.IsInfinity(model.Longitude)
            && !double.IsNegativeInfinity(model.Longitude)
            && model.Longitude >= -180.0 && model.Longitude <= 180.0;

        /// <summary>
        /// Check correct for latitude and longitude property.
        /// </summary>
        public static bool IsCorrect(this LocationModel model) =>
            model.IsCorrectLatitude() && model.IsCorrectLongitude();
    }
}
