using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrackingCode.Domain.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Add(T entity);
        void Update(T entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetAsync(Specification<T> spec);
        Task<IList<T>> ListAsync(Specification<T> spec);
        Task<IList<T>> ListAllAsync();
    }
}