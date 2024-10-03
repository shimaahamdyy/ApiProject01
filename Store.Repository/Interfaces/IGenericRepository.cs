using Store.Data.Entities;
using Store.Repository.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interfaces
{
    public interface IGenericRepository<TEntity , Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<TEntity> GetByIdAsync(Tkey? id);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync();
        Task<TEntity> GetByIdWithSpecificationsAsync(ISpecification<TEntity> specs);
        Task<IReadOnlyList<TEntity>> GetAllWithSpecificationsAsync(ISpecification<TEntity> specs);
        Task<int> GetCountSpecificationsAsync(ISpecification<TEntity> specs);
        Task AddAsync (TEntity entity);
        void Update (TEntity entity);
        void Delete (TEntity entity);

    }
}
