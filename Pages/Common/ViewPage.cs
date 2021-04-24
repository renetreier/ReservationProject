using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReservationProject.Core;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra;

namespace ReservationProject.Pages.Common {
    public abstract class ViewPage<TEntity, TView> :PagedPage<TEntity, TView>
        where TEntity : class, IBaseEntity, new()
        where TView : class, IEntityData, new() {
        protected ViewPage(IRepo<TEntity> r, ApplicationDbContext c = null) :base(r, c) { }
        public virtual async Task<IActionResult> OnGetIndexAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex) {
            PageIndex = pageIndex;
            SearchString = searchString;
            CurrentFilter = currentFilter;
            SortOrder = sortOrder;
            Items = (await Repo.Get()).Select(ToViewModel).ToList();//TODO siia äkki ItemList hoopis?
            return Page();
        }
    }
}

