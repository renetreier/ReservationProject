using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Core;
using ReservationProject.Data.Common;

namespace ReservationProject.Infra.Common {
    public abstract class FilteredRepo<TEntity, TData> :CrudRepo<TEntity, TData>
        where TData : BaseData, IEntityData, new() {
        private string _currentFilter;
        private string _searchString;
        protected FilteredRepo(DbContext c = null, DbSet<TData> s = null) :base(c, s) { }
        public override IQueryable<TData> CreateSql() => ApplyFilters(base.CreateSql());
        public virtual IQueryable<TData> ApplyFilters(IQueryable<TData> query) => query;
        public override string CurrentFilter {
            get => _currentFilter;
            set => SetFilter(value, _searchString);
        }
        public override string SearchString {
            get => _searchString;
            set => SetFilter(_currentFilter, value);
        }
        protected internal virtual void SetFilter(string curFilter, string searchStr) {
            SetPageIndex(searchStr);
            SetSearchString(curFilter, searchStr);
            SetCurrentFilter(searchStr);
        }
        protected internal virtual void SetCurrentFilter(string searchStr)
            => _currentFilter = searchStr;
        protected internal virtual void SetSearchString(string curFilter, string searchStr)
            => _searchString = searchStr ?? curFilter;
        protected internal virtual void SetPageIndex(string searchStr)  
            => PageIndex = (searchStr == null)? PageIndex : 1;
    }
}