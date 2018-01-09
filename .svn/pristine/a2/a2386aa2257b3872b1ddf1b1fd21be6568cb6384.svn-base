using System;
using System.Collections.Generic;

namespace EAM.Data.Comm
{
    public class PagedList<T>
    {
        public List<T> Items { get; private set; }
        public List<T> AllItem { get; private set; }

        public PagedList()
        {
            Items = new List<T>();
            AllItem = new List<T>();
        }

        public PagedList(IList<T> items, long pageIndex, long pageSize):this()
        {
            PageSize = pageSize;
            TotalItemCount = items.Count;
            TotalPageCount = (long) Math.Ceiling(TotalItemCount/(double) PageSize);
            CurrentPageIndex = pageIndex;
            StartRecordIndex = (CurrentPageIndex - 1)*PageSize + 1;
            EndRecordIndex = TotalItemCount > pageIndex*pageSize ? pageIndex*pageSize : TotalItemCount;
            for (long i = StartRecordIndex - 1; i < EndRecordIndex; i++)
            {
                Items.Add(items[(int)i]);
            }
        }

        public PagedList(IEnumerable<T> items, long pageIndex, long pageSize, long totalItemCount)
            : this()
        {
            Items.AddRange(items);
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
            StartRecordIndex = (pageIndex - 1) * pageSize + 1;
            EndRecordIndex = TotalItemCount > pageIndex * pageSize ? pageIndex * pageSize : totalItemCount;
        }

        public PagedList(IEnumerable<T> items, long pageIndex, long pageSize, long totalItemCount, IEnumerable<T> allEntities)
            : this()
        {
            Items.AddRange(items);
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
            StartRecordIndex = (pageIndex - 1) * pageSize + 1;
            EndRecordIndex = TotalItemCount > pageIndex * pageSize ? pageIndex * pageSize : totalItemCount;

            //
            // 所有查询出来的资产
            //
            AllItem.AddRange(allEntities);
        }

        public long CurrentPageIndex { get; set; }
        public long PageSize { get; set; }
        public long TotalItemCount { get; set; }
        public long TotalPageCount { get; private set; }
        public long StartRecordIndex { get; private set; }
        public long EndRecordIndex { get; private set; }

    }
}