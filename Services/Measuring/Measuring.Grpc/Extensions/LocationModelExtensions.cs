using Measuring.Grpc.Protos;

namespace Measuring.Grpc.Extensions
{
    public static class LocationModelExtensions
    {
        public static bool IsEqualValue(this LocationModel model, LocationModel other) =>
            model.Longitude == other.Longitude && model.Latitude == other.Latitude;

        public static bool IsCorrectLatitude(this LocationModel model) =>
            !double.IsNaN(model.Latitude) 
            && !double.IsInfinity(model.Latitude)
            && !double.IsNegativeInfinity(model.Latitude)
            && model.Latitude >= -90.0 && model.Latitude <= 90.0;

        public static bool IsCorrectLongitude(this LocationModel model) =>
            !double.IsNaN(model.Longitude)
            && !double.IsInfinity(model.Longitude)
            && !double.IsNegativeInfinity(model.Longitude)
            && model.Longitude >= -180.0 && model.Longitude <= 180.0;

        public static bool IsCorrect(this LocationModel model) =>
            model.IsCorrectLatitude() && model.IsCorrectLongitude();
    }
}
