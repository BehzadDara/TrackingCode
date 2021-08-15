using System;
using TrackingCode.Domain.Interfaces;

namespace TrackingCode.Domain.Implementations
{
    public abstract class TrackableEntity : Entity, ITrackableEntity
    {
        public DateTime CreatedAtUtc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAtUtc { get; set; }
        public string DeletedBy { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TrackableEntity)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(TrackableEntity left, TrackableEntity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TrackableEntity left, TrackableEntity right)
        {
            return !Equals(left, right);
        }

    }
}