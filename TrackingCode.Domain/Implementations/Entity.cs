using System;
using TrackingCode.Domain.Interfaces;

namespace TrackingCode.Domain.Implementations
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            Id = Comb.Create();
        }

        public Guid Id { get; set; }
    }
}