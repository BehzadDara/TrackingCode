using System;

namespace TrackingCode.Domain.Interfaces
{
    public interface ITrackableEntity : IEntity, ISoftDeletable
    {
        public DateTime CreatedAtUtc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DeletedAtUtc { get; set; }
        public string DeletedBy { get; set; }
    }
}