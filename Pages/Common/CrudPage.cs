using System.Collections.Generic;
using ReservationProject.Core;
using ReservationProject.Domain.Repos;
using ReservationProject.Infra;

namespace ReservationProject.Pages.Common {

    public abstract class CrudPage<TEntity, TView> :BasePageModel<TEntity, TView>
        where TEntity : class, IBaseEntity, new()
        where TView : class, IEntityData, new() {
        protected CrudPage(IRepo<TEntity> r, ApplicationDbContext c = null) :base(r, c) { }

        public IList<TView> ItemList { get; set; }
        
    }
}