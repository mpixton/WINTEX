using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WINTEX.Infrastructure
{
    /// <summary>
    /// Generic Paginator class to paginate a list of objects of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of objects to paginate.</typeparam>
    public class Paginator<T> where T : class
    {
        /// <summary>
        /// Initializes all properties and creates a new Paginator.
        /// </summary>
        /// <param name="pageSize">Number of <typeparamref name="T"/>s to appear on one page.</param>
        /// <param name="currentPage">Index of current page.</param>
        /// <param name="currentTeam">If provided, current team.</param>
        /// <param name="list">List of <typeparamref name="T"/>s to paginate.</param>
        public Paginator(int pageSize, int currentPage, string currentTeam, IEnumerable<T> list)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            CurrentTeam = currentTeam;
            ListToPage = list;
            TotalItems = list.Count();
            TotalPages = (int) Math.Ceiling((decimal) TotalItems / PageSize);
        }

        /// <summary>
        /// Number of <typeparamref name="T"/>s to appear on one page.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Count of all items in the <seealso cref="ListToPage"/>.
        /// </summary>
        public int TotalItems { get; }

        /// <summary>
        /// TotalItems divided by PageSize, rounded up.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// List of <typeparamref name="T"/>s to paginate.
        /// </summary>
        public IEnumerable<T> ListToPage { get; }

        /// <summary>
        /// Index of the current page in the <seealso cref="ListToPage"/>.
        /// </summary>
        public int CurrentPage { get; }
        
        /// <summary>
        /// If selected, the current team filter.
        /// </summary>
        public string CurrentTeam { get;  }

        /// <summary>
        /// Performs the pagination based on the arguments passed when constructed.
        /// </summary>
        /// <param name="pageNum">Current page number.</param>
        /// <returns>Current set of <typeparamref name="T"/>s to display.</returns>
        public IEnumerable<T> GetItems(int pageNum)
        {
            return ListToPage.Skip((pageNum - 1) * PageSize)
                   .Take(PageSize);
        }
    }
}
