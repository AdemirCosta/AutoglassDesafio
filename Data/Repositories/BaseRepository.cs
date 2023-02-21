using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AutoglassContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AutoglassContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public DbSet<T> DbSet()
        {
            return _dbSet;
        }

        public IList<T> FindAll()
        {
            return _dbSet.ToList();
        }

        public IQueryable<T> FindAllAsQueryable()
        {
            return _dbSet.AsQueryable<T>();
        }

        public T? Find(int id, Expression<Func<T, object>>? include = null)
        {
            if(include == null)
                return _dbSet.FirstOrDefault(n => n.Id == id);
            else
                return _dbSet.Include(include).FirstOrDefault(n => n.Id == id);
        }

        public T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(typeof(T).FullName);

            var result = _dbSet.Add(entity);

            return result.Entity;
        }

        public void AddRange(List<T> entity)
        {
            if (entity == null || entity.Count == 0)
                throw new ArgumentNullException(typeof(T).FullName);

            _dbSet.AddRange(entity);
        }

        public T? Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(typeof(T).FullName);

            T? exist = _dbSet.SingleOrDefault(t => t.Id == entity.Id);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                return entity;
            }

            return null;
        }

        public T Update(T source, T target)
        {
            if (source == null || target == null)
                throw new ArgumentNullException(typeof(T).FullName);

            foreach (var propertyEntry in _context.Entry(target).Properties)
            {
                var property = propertyEntry.Metadata;
                if (property.IsShadowProperty() || (propertyEntry.EntityEntry.IsKeySet && property.IsKey())) continue;
                propertyEntry.CurrentValue = property.GetGetter().GetClrValue(source);
            }

            return _context.Entry(target).Entity;
        }

        public bool Delete(long Id)
        {
            T? exist = _dbSet.SingleOrDefault(t => t.Id == Id);
            if (exist != null)
            {
                _dbSet.Remove(exist);
                return true;
            }

            return false;
        }

        public bool Delete(T entityToDelete)
        {
            if (entityToDelete == null)
                throw new ArgumentNullException(typeof(T).FullName);

            T? exist = _dbSet.SingleOrDefault(t => t.Id == entityToDelete.Id);
            if (exist != null)
            {
                _dbSet.Remove(exist);
                return true;
            }

            return false;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
