using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WINTEX.DAL
{
    /// <summary>
    /// Creates a generic repo for all other repos to be based on.
    /// </summary>
    public class GenericRepo<T> : IGenericRepository<T> where T : class
    {
        internal FagElGamousDbContext _context;
        internal DbSet<T> _dbSet;
        private ILogger<T> _logger;
        
        public GenericRepo(FagElGamousDbContext context, ILogger<T> logger)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _logger = logger;
            _logger.LogInformation("{Type} repo with DbContext {ContextID} created", typeof(T), _context.ContextId);
        }
        
        // Create Methods
        /// <summary>
        /// Generic method to add a <typeparamref name="T"/> to the DbSet.
        /// </summary>
        /// <param name="insertObject"><typeparamref name="T"/> to add.</param>
        public virtual void Insert(T addObject)
        {
            _logger.LogInformation("{@AddObject} passed int", addObject);
            _dbSet.Add(addObject);
            _logger.LogInformation("{@AddObject} was added", addObject);
        }

        /// <summary>
        /// Generic method to add multiple <typeparamref name="T"/>s to the DB.
        /// </summary>
        /// <param name="addObjects">IEnumerable of <typeparamref name="T"/> to add.</param>
        public virtual void BulkInsert(IEnumerable<T> addObjects)
        {
            _logger.LogInformation("{TotalObjects} were passed in", addObjects.Count());
            _dbSet.AddRange(addObjects);
            _logger.LogInformation("{TotalObjects} were added", addObjects.Count());
        }

        // Read Methods
        /// <summary>
        /// Generic method to retrieves all <typeparamref name="T"/> records.
        /// </summary>
        /// <returns>IEnumerable of type T.</returns>
        public virtual IEnumerable<T> GetAll()
        {
            _logger.LogInformation("{ReturnedObjectsCount} {ObjectType}s returned", _dbSet.Count(), typeof(T));
            return _dbSet.AsEnumerable();
        }

        /// <summary>
        /// Generic method for returning all <typeparamref name="T"/>s with any Include calls.
        /// </summary>
        /// <param name="includes">Array of objects associated with <typeparamref name="T"/> to include in the results.</param>
        /// <returns>IEnumerable of <typeparamref name="T"/>s with objects included.</returns>
        public virtual IEnumerable<T> GetAll(
            params Expression<Func<T, object>>[] includes)
        {
            _logger.LogInformation("{IncludeFuncs} were passed in", includes);
            IQueryable<T> query = _dbSet.Include(includes[0]);
            foreach(var includeObject in includes.Skip(1))
            {
                query = query.Include(includeObject);
                _logger.LogInformation("{Include} processed", includeObject);
            }
            _logger.LogInformation("{ReturnedObjectsCount} {ObjectType}s returned with {IncludeFuncs} included",
                query.Count(), typeof(T), includes);
            return query.AsEnumerable();
        }

        /// <summary>
        /// Finds a specific <typeparamref name="T"/> by PK.
        /// </summary>
        /// <param name="id">PK of the <typeparamref name="T"/> to find.</param>
        /// <returns><typeparamref name="T"/> with PK of <paramref name="id"/>.</returns>
        public virtual T GetByID(long id)
        {
            _logger.LogInformation("{FindId} passed in", id);
            return _dbSet.Find(id);
        }

        // Update Methods
        /// <summary>
        /// Generic method to update a <typeparamref name="T"/> in the DB.
        /// </summary>
        /// <param name="updateObject"><typeparamref name="T"/> to update.</param>
        public virtual void Update(T updateObject)
        {
            _logger.LogInformation("{@UpdateObject} passed in", updateObject);
            _dbSet.Attach(updateObject);
            _context.Entry(updateObject).State = EntityState.Modified;
            _logger.LogInformation("{@UdpateObject} updated sucessfully", updateObject);
        }

        // Delete Methods
        /// <summary>
        /// Generic method to delete a <typeparamref name="T"/>.
        /// </summary>
        /// <param name="deleteObject"><typeparamref name="T"/> to delete from the DB.</param>
        // TODO Add error handling if an invalid object is passed.
        public virtual void Delete(T deleteObject)
        {
            _logger.LogInformation("{@DeleteObject} was passed in", deleteObject);
            if(_context.Entry(deleteObject).State == EntityState.Detached)
            {
                _dbSet.Attach(deleteObject);
                _logger.LogInformation("{@DeleteObject} attached", deleteObject);
            }
            _dbSet.Remove(deleteObject);
            _logger.LogInformation("{@DeleteObject} removed", deleteObject);
        }

        /// <summary>
        /// Generic method to delete a <typeparamref name="T"/> given the PK.
        /// </summary>
        /// <param name="id">PK of the <typeparamref name="T"/> to delete.</param>
        // TODO Add error handling if a wrong id is passed.
        public virtual void Delete(long id)
        {
            _logger.LogInformation("{DeleteId} was passed in", id);
            T deleteObject = _dbSet.Find(id);
            _dbSet.Remove(deleteObject);
            _logger.LogInformation("{@DeleteObject} was removed", deleteObject);
        }
    }
}
