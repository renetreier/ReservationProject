using ReservationProject.Core;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra;

namespace ReservationProject.Pages.Common {
    public abstract class FilteredPage<TEntity, TView> :CrudPage<TEntity, TView>
        where TEntity : class, IBaseEntity, new()
        where TView : class, IEntityData, new() {
        protected FilteredPage(IRepo<TEntity> r, ApplicationDbContext c = null) :base(r, c) { }
        public override string CurrentFilter 
        {
            get => Repo.CurrentFilter;
            set => Repo.CurrentFilter = value;
        }
        public override string SearchString 
        {
            get => Repo.SearchString;
            set => Repo.SearchString = value;
        }
    }
}
