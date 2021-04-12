using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WINTEX.Models.ViewModels
{
    public class PaginationInfo<T>
    {
        public PaginationInfo(int currentPage, int totalPages, IEnumerable<T> paginatedList)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PaginatedList = paginatedList;
        }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<T> PaginatedList { get; set; }
    }
}
