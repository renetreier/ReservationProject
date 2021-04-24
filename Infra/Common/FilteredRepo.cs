using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Core;
using ReservationProject.Data.Common;

namespace ReservationProject.Infra.Common {
    public abstract class FilteredRepo<TEntity, TData> :CrudRepo<TEntity, TData>
        where TData : BaseEntityData, IEntityData, new() {
        private string currentFilter;
        private string searchString;
        protected FilteredRepo(DbContext c = null, DbSet<TData> s = null) :base(c, s) { }
        protected internal override IQueryable<TData> CreateSql() => ApplyFilters(base.CreateSql());
        protected internal virtual IQueryable<TData> ApplyFilters(IQueryable<TData> query) => query;
        public override string CurrentFilter {
            get => currentFilter;
            set => SetFilter(value, searchString);
        }
        public override string SearchString {
            get => searchString;
            set => SetFilter(currentFilter, value);
        }
        protected internal virtual void SetFilter(string curFilter, string searchStr) {
            SetPageIndex(searchStr);
            SetSearchString(curFilter, searchStr);
            SetCurrentFilter(searchStr);
        }
        protected internal virtual void SetCurrentFilter(string searchStr)
            => currentFilter = searchStr;
        protected internal virtual void SetSearchString(string curFilter, string searchStr)
            => searchString = searchStr ?? curFilter;
        protected internal virtual void SetPageIndex(string searchStr)  
            => PageIndex = (searchStr == null)? PageIndex : 1;
    }
}