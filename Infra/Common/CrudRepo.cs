using Microsoft.EntityFrameworkCore;
using ReservationProject.Core;
using ReservationProject.Data.Common;

namespace ReservationProject.Infra.Common {
    public abstract class CrudRepo<TEntity, TData> :BaseRepo<TEntity, TData>
        where TData : BaseEntityData, IEntityData, new() {
        protected CrudRepo(DbContext c = null, DbSet<TData> s = null) : base(c, s) { }
    }
}