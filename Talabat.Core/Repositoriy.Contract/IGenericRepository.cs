using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifcation;

namespace Talabat.Core.Repository.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> Spec);
        Task<T?> GetByIdWithSpecAsync(ISpecification<T> Spec);
        Task<int> CountAsync(ISpecification<T> Spec);
        Task<decimal> GetSumAsync(ISpecification<T> Spec, Expression<Func<T, decimal>> selector);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<int> SaveChangesAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
