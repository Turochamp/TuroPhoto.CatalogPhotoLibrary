using System;
using System.Collections.Generic;
using System.Linq;

namespace TuroPhoto.PhotoLibraryCatalog.Model
{
    public class ImageMetaData : IEquatable<ImageMetaData>
    {
        public DateTime? DateTime { get; set; }

        public IList<Tag> GpsTags { get; set; }
        public GeoLocation? GeoLocation { get; set; }

        public bool Equals(ImageMetaData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DateTime.Equals(other.DateTime) && GpsTags.SequenceEqual(other.GpsTags) && GeoLocation.Equals(other.GeoLocation);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ImageMetaData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = DateTime.GetHashCode();
                hashCode = hashCode * 397 ^ (GpsTags != null ? GpsTags.GetHashCode() : 0);
                hashCode = hashCode * 397 ^ GeoLocation.GetHashCode();
                return hashCode;
            }
        }
    }

    public class Tag : IEquatable<Tag>
    {
        public Tag(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool Equals(Tag other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && string.Equals(Description, other.Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Tag)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name != null ? Name.GetHashCode() : 0) * 397 ^ (Description != null ? Description.GetHashCode() : 0);
            }
        }
    }

}