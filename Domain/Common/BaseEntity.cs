using System;
using ReservationProject.Aids;
using ReservationProject.Core;
using ReservationProject.Domain.Repos;

namespace ReservationProject.Domain.Common {
    public abstract class BaseEntity<TData> :IBaseEntity
        where TData : class, IEntityData, new() {
        protected readonly TData data;

        protected BaseEntity() :this(null) { }
        protected BaseEntity(TData d) => data = d;

        public TData Data => Copy.Members(data, new TData()) ?? new TData();
        public string Id => Data?.Id ?? "Unspecified";
        public byte[] RowVersion => Data?.RowVersion ?? Array.Empty<byte>();
        
        internal static Lazy<TEntity> GetLazy<TEntity, TRepo>(Func<TRepo, TEntity> func)
            where TEntity : IBaseEntity where TRepo : IRepo<TEntity> =>
            new(() => func(GetRepo<TRepo>()));

        internal static TRepo GetRepo<TRepo>() => new GetRepo().Instance<TRepo>();
    }
}




