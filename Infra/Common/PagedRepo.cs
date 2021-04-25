using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Core;
using ReservationProject.Data.Common;

namespace ReservationProject.Infra.Common
{
    public abstract class PagedRepo<TEntity, TData> :OrderedRepo<TEntity, TData>
        where TData : BaseEntityData, IEntityData, new() {
        private int pageIndex;

        public const int DefaultPageSize = 5;
        protected PagedRepo(DbContext c = null, DbSet<TData> s = null) : base(c, s) { }
        public override int? PageIndex {
            get => pageIndex;
            set => pageIndex = value ?? 1;
        }
        public override int TotalPages => GetTotalPages(PageSize);
        public override bool HasNextPage => pageIndex < TotalPages;
        public override bool HasPreviousPage => pageIndex > 1;
        public override int PageSize { get; set; } = DefaultPageSize;
        internal int GetTotalPages(in int pageSize) {
            var count = GetItemsCount();
            var pages = CountTotalPages(count, pageSize);
            return pages;
        }
        internal static int CountTotalPages(int count, in int pageSize) 
            => (int)Math.Ceiling( count / (double)pageSize);
        internal int GetItemsCount() => base.CreateSql().Count();
        protected internal override IQueryable<TData> CreateSql() => AddSkipAndTake(base.CreateSql());
        private IQueryable<TData> AddSkipAndTake(IQueryable<TData> query) {
            if (pageIndex < 1) return query;
            return query
                .Skip((pageIndex - 1) * PageSize)
                .Take(PageSize);
        }
    }
}
