using System.Linq.Expressions;

namespace ExpenseTracker.Infrastructure.Contracts
{
   /// <summary>
   /// Contains signatures of all generic methods.
   /// </summary>
   /// <typeparam name="T">T is a model class.</typeparam>
   public interface IRepository<T>
   {
      /// <summary>
      /// Creates a row in the table.
      /// </summary>
      /// <param name="entity">Object to be saved in the table as a row.</param>
      /// <returns>Saved object.</returns>
      T Add(T entity);

      /// <summary>
      /// Returns a row from the table as an object if primary key matches.
      /// </summary>
      /// <param name="id">Primary key</param>
      /// <returns>Retrieved object.</returns>
      T Get(Guid id);

      /// <summary>
      /// Returns all rows as a list of objects from the table.
      /// </summary>
      /// <returns>List of objects.</returns>
      IEnumerable<T> GetAll();

        /// <summary>
        /// The method is used to get a list of data.
        /// </summary>
        /// <param name="predicate">Custom LINQ expression.</param>
        /// <returns>Returns matched rows as a list of data.</returns>
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj);

        /// <summary>
        /// The method is used to get a list of data.
        /// </summary>
        /// <param name="predicate">Custom LINQ expression.</param>
        /// <returns>Returns matched rows as a list of data.</returns>
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj, Expression<Func<T, object>> next);
        /// <summary>
        /// Searches using the given criteria.
        /// </summary>
        /// <param name="predicate">Search criteria.</param>
        /// <returns>Object list.</returns>
        Task<IEnumerable<T>> IQueryAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// The method is used to get a list of data.
        /// </summary>
        /// <param name="predicate">Custom LINQ expression.</param>
        /// <returns>Returns matched rows as a list of data.</returns>
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll(string Include);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

      /// <summary>
      /// Updates an existing row in the table.
      /// </summary>
      /// <param name="entity">Object to be updated.</param>
      void Update(T entity);

      /// <summary>
      /// Deletes a row from the table.
      /// </summary>
      /// <param name="entity">Object to be deleted.</param>
      void Delete(T entity);
   }
}