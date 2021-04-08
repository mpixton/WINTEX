using WINTEX.Models;

namespace WINTEX.DAL
{
    /// <summary>
    /// Defines the members for the Unit Of Work class. 
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Generic Repo for Bowlers.
        /// </summary>
        

        /// <summary>
        /// Saves the changes made to the Repos in this Unit Of Work to the Db.
        /// </summary>
        void Save();
    }
}