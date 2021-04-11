using WINTEX.Models;
using Microsoft.Extensions.Logging;

namespace WINTEX.DAL
{
    /// <summary>
    /// Unit Of Work class. Implements the IUnitOfWork members.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// DbContext to be shared by all Repos in this Unit Of Work. Prevents concurrency issues.
        /// </summary>
        private FEGBExcavationContext _context;
        private readonly ILogger _logger;
        private ILoggerFactory _loggerFactory;

        /// <summary>
        /// Generic Repo for interacting with all Bowlers.
        /// </summary>

        public UnitOfWork(FEGBExcavationContext context, ILoggerFactory loggerFactory)
        {

            _context = context;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<UnitOfWork>();
            _logger.LogInformation("Unit of Work with DbContext {DbContext} created}", _context.ContextId);
        }


        // If the generic repo doesn't exists, create a new one with the DbContext provided by dependency 
        // injection. If already exists, return the current one. Ensures that all Repos are sharing
        // the same DbContext so that changes to mulitple Repos are saved at the same time.


        /// <summary>
        /// Saves the chagnes across all Repos in one go.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
