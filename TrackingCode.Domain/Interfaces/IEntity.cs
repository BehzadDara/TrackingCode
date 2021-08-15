using System;

namespace TrackingCode.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}