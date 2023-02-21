using Core.Entities;
using System.Linq.Expressions;

namespace Core.Repositories
{
    public interface IBaseRepository<T> : IDisposable where T : BaseEntity
    {
        IList<T> FindAll();
        IQueryable<T> FindAllAsQueryable();
        T? Find(int id, Expression<Func<T, object>>? include = null);
        T Add(T entity);
        void AddRange(List<T> entity);
        T? Update(T entity);
        T Update(T source, T target);
        bool Delete(long Id);
        bool Delete(T entityToDelete);
        void Save();
    }
}
