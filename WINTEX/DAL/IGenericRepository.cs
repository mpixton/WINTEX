using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WINTEX.DAL
{
    /// <summary>
    /// Abstraction layer on top of each repository. Allows for better unit testing.
    /// </summary>
    /// <typeparam name="T">Object the Repository is for.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Gets all <typeparamref name="T"/> objects. Does not return associated objects.
        /// </summary>
        /// <returns>IEnumerable of <typeparamref name="T"/>s.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets all <typeparamref name="T"/> objects and the requested related objects. Allows for eager loading.
        /// </summary>
        /// <param name="includes">Lambda that evaulates to objects related to a <typeparamref name="T"/>.</param>
        /// <returns>IEnumerable of <typeparamref name="T"/>s.</returns>
        IEnumerable<T> GetAll(
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Finds a single <typeparamref name="T"/> by PK.
        /// </summary>
        /// <param name="id">PK of the <typeparamref name="T"/> to return.</param>
        /// <returns>A <typeparamref name="T"/>.</returns>
        T GetByID(long id);

        /// <summary>
        /// Adds a <typeparamref name="T"/> to the in-memory DbSet. Requires .Save to be called to persist to the Database.
        /// </summary>
        /// <param name="entity">A <typeparamref name="T"/> to add to the Database.</param>
        void Insert(T entity);

        /// <summary>
        /// Adds an IEnumerable of <typeparamref name="T"/>s to the in-memory DbSet. Requires .Save to be called to persist to the Database.
        /// </summary>
        /// <param name="entities">IEnumerable of <typeparamref name="T"/>s to add to the Database.</param>
        void BulkInsert(IEnumerable<T> entities);

        /// <summary>
        /// Updates a <typeparamref name="T"/>.
        /// </summary>
        /// <param name="entity"><typeparamref name="T"/> with updated attributes.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes a <typeparamref name="T"/> from the in-memory DbSet. Requires .Save to be called to persist to the Database.
        /// </summary>
        /// <param name="entity"><typeparamref name="T"/> to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes a <typeparamref name="T"/> from the in-memory DbSet that matches the parameter PK. Requires .Save to be called to persist chanages to the Database.
        /// </summary>
        /// <param name="id">PK of the <typeparamref name="T"/> to delete.</param>
        void Delete(long id);
    }
}
