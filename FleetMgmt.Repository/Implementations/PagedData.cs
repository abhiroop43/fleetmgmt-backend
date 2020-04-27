using System;
using System.Collections.Generic;
using System.Text;
using FleetMgmt.Repository.Interfaces;

namespace FleetMgmt.Repository.Implementations
{
    public class PagedData<T> : IPagedData<T>
    {
        public dynamic GetPaginationDetails<T>(int pageIndex, int pageSize, int totalCount)
        {
            TotalItemCount = totalCount;
            PageSize = pageSize;
            PageIndex = pageIndex;
            PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;

            HasPreviousPage = (PageIndex > 1);
            HasNextPage = (PageIndex < (PageCount));
            IsFirstPage = (PageIndex == 1);
            IsLastPage = (PageIndex >= (PageCount));

            ItemStart = (PageIndex - 1) * PageSize + 1;
            ItemEnd = Math.Min((PageIndex - 1) * PageSize + PageSize, TotalItemCount);

            return new PagedData<T>
            {
                TotalItemCount = TotalItemCount,
                PageSize = PageSize,
                PageIndex = PageIndex,
                PageCount = PageCount,
                HasPreviousPage = HasPreviousPage,
                HasNextPage = HasNextPage,
                IsFirstPage = IsFirstPage,
                IsLastPage = IsLastPage,
                ItemStart = ItemStart,
                ItemEnd = ItemEnd
            };
        }

        public IList<T> Data { get; set; }

        public int PageCount { get; set; }
        public int TotalItemCount { get; set; }
        public int PageIndex { get; set; }
        public int PageNumber { get { return PageIndex; } }

        public int PageSize { get; set; }

        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public int ItemStart { get; set; }
        public int ItemEnd { get; set; }
    }
}
