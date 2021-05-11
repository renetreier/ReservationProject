using ReservationProject.Core;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra;

namespace ReservationProject.Pages.Common {
    public abstract class PagedPage<TEntity, TView> :OrderedPage<TEntity, TView>
        where TEntity : class, IBaseEntity, new()
        where TView : class, IEntityData, new() {
        protected PagedPage(IRepo<TEntity> r, ApplicationDbContext c = null) :base(r, c) { }
        public override bool HasNextPage => Repo.HasNextPage;
        public override bool HasPreviousPage => Repo.HasPreviousPage;
        public override int? PageIndex 
        {
            get => Repo.PageIndex;
            set => Repo.PageIndex = value;
        }
    }
}
