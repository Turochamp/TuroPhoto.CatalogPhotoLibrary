using System;

namespace TuroPhoto.PhotoLibraryCatalog.Model
{
    // Code from https://stackoverflow.com/questions/6151625/should-i-use-a-struct-or-a-class-to-represent-a-lat-lng-coordinate
    struct GeoLocation : IEquatable<GeoLocation>
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public GeoLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return $"{Latitude},{Longitude}";
        }

        public override bool Equals(object other)
        {
            return other is GeoLocation location && Equals(location);
        }

        public bool Equals(GeoLocation other)
        {
            return Latitude == other.Latitude && Longitude == other.Longitude;
        }

        public override int GetHashCode()
        {
            return Latitude.GetHashCode() ^ Longitude.GetHashCode();
        }
    }
}
