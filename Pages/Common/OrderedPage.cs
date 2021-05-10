using ReservationProject.Core;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra;

namespace ReservationProject.Pages.Common
{
    public abstract class OrderedPage<TEntity, TView> : FilteredPage<TEntity, TView>
        where TEntity : class, IBaseEntity, new()
        where TView : class, IEntityData, new()
    {
        protected OrderedPage(IRepo<TEntity> r, ApplicationDbContext c = null) : base(r, c) { }

        public override string SortOrder
        {
            get => Repo.SortOrder;
            set => Repo.SortOrder = value;
        }
        public override string CurrentSort => Repo.CurrentSort;
    }
}