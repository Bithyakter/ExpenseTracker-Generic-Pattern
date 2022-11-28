using ExpenseTracker.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTracker.Infrastructure.Repositories
{
   /// <summary>
   /// Implementation of IRepository interface.
   /// </summary>
   /// <typeparam name="T">T is a model class.</typeparam>
   public class Repository<T> : IRepository<T> where T : class
   {
      protected readonly DataContext context;

      public Repository(DataContext context)
      {
         this.context = context;
      }

      public virtual T Add(T entity)
      {
         try
         {
            return context.Set<T>().Add(entity).Entity;
         }
         catch
         {
            throw;
         }
      }

      public virtual T Get(Guid id)
      {
         try
         {
            return context.Set<T>().Find(id);
         }
         catch
         {
            throw;
         }
      }

      IQueryable<T> IRepository<T>.GetAll(string include)
      {
         var entity = context.Set<T>()
            .Include(include)
            .AsNoTracking();

         return entity;
      }

      public virtual IEnumerable<T> GetAll()
      {
         try
         {
            return context.Set<T>()
                .AsQueryable()
                .AsNoTracking()
                .ToList();
         }
         catch
         {
            throw;
         }
      }

      //public virtual IEnumerable<T> GetAllInclude(string value)
      //{
      //   try
      //   {
      //      return context.Set<T>().Include(value)
      //          .AsQueryable()
      //          .AsNoTracking()
      //          .ToList();
      //   }
      //   catch
      //   {
      //      throw;
      //   }
      //}

      public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate)
      {
         try
         {
            return await context.Set<T>()
                .AsQueryable()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
         }
         catch
         {
            throw;
         }
      }

      public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj)
      {
         try
         {
            return await context.Set<T>()
                .AsQueryable()
                .AsNoTracking()
                .Where(predicate)
                .Include(obj)
                .ToListAsync();
         }
         catch
         {
            throw;
         }
      }

      public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> order, Expression<Func<T, object>> obj)
      {
         try
         {
            return await context.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .OrderByDescending(order)
                .Include(obj)
                .ToListAsync();
         }
         catch
         {
            throw;
         }
      }

      public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
      {
         try
         {
            return await context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public virtual void Update(T entity)
      {
         try
         {
            context.Entry(entity).State = EntityState.Modified;
            context.Set<T>().Update(entity);
         }
         catch
         {
            throw;
         }
      }

      public virtual void Delete(T entity)
      {
         try
         {
            context.Set<T>().Remove(entity);
         }
         catch
         {
            throw;
         }
      }
   }
}