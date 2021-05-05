using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Core;
using ReservationProject.Data.Common;
using ReservationProject.Domain.Repos;

namespace ReservationProject.Infra.Common {

    public abstract class BaseRepo<TEntity, TData> : BaseRepo<TData>, IRepo<TEntity>
        where TData : BaseData, IEntityData, new()
    {
        protected abstract TEntity ToEntity(TData d);
        protected abstract TData ToData(TEntity e);
        protected BaseRepo(DbContext c = null, DbSet<TData> s = null) : base(c, s) { }
        public new TEntity EntityInDb => ToEntity(base.EntityInDb);
        public new async Task<List<TEntity>> GetAsync() => (await base.GetAsync()).Select(ToEntity).ToList();
        public new async Task<TEntity> GetAsync(string id) => ToEntity(await base.GetAsync(id));
        public virtual async Task<bool> DeleteAsync(TEntity e) => await DeleteAsync(ToData(e));
        public virtual async Task<bool> AddAsync(TEntity e) => await AddAsync(ToData(e));

        public virtual async Task<bool> UpdateAsync(TEntity e) => await UpdateAsync(ToData(e));
        public new TEntity Get(string id) => ToEntity(base.Get(id));
        public List<TEntity> Get() => GetDropDownList().Select(ToEntity).ToList();
    }
    public abstract class BaseRepo<T>  where T : BaseData, IEntityData, new() {
        protected internal readonly DbSet<T> Set;
        protected internal readonly DbContext Db;
        public T EntityInDb { get; protected set; }
        public string ErrorMessage { get; protected set; }

        protected BaseRepo(DbContext c = null, DbSet<T> s = null) {
            Set = s;
            Db = c;
        }
        public async Task<List<T>> GetAsync() => await CreateSql().ToListAsync();
        protected internal virtual IQueryable<T> CreateSql() => Set.AsNoTracking();

        public async Task<T> GetAsync(string id)
        {
            if (id is null) return null;
            if (Set is null) return null;
            return await Set.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> DeleteAsync(T obj)
        {
            var isOk = await IsEntityOk(obj, ErrorMessages.ConcurrencyOnDelete);
            if (isOk) Set.Remove(obj);
            await Db.SaveChangesAsync();
            return isOk;
        }

        public async Task<bool> AddAsync(T obj)
        {
            var isOk = await IsEntityOk(obj, true);
            if (isOk)
            {
                await Set.AddAsync(obj);
                await Db.SaveChangesAsync();
            }
            return isOk;
        }

        public async Task<bool> UpdateAsync(T obj)
        {
            var isOk = await IsEntityOk(obj, ErrorMessages.ConcurrencyOnEdit);
            if (isOk)
            {
                Set.Update(obj);
                await Db.SaveChangesAsync();
            }
            return isOk;
        }
        
        internal static bool ByteArrayCompare(ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2)
            => a1.SequenceEqual(a2);

        private bool SetErrorMessage(string msg)
        {
            ErrorMessage = msg;
            return false;
        }

        internal async Task<bool> IsEntityOk(T obj,
            string concurrencyErrorMsg)
        {
            return await IsEntityOk(obj, false)
                   && IsCorrectVersion(obj, concurrencyErrorMsg);
        }

        private async Task<bool> IsEntityOk(T obj, bool isNewItem)
        {
            if (obj is null) return SetErrorMessage("Item is null");
            if (Set is null) return SetErrorMessage("DbSet is null");
            EntityInDb = await GetAsync(obj.Id);
            return (EntityInDb is null) == isNewItem
                   || SetErrorMessage(
                       isNewItem
                           ? $"Item with id = <{obj.Id}> already in database"
                           : $"No item with id = <{obj.Id}> in database");
        }

        internal bool IsCorrectVersion(T obj,
            string concurrencyErrorMsg)
        {
            return ByteArrayCompare(obj?.RowVersion, EntityInDb?.RowVersion)
                   || SetErrorMessage(concurrencyErrorMsg);
        }

        public T Get(string id) => GetAsync(id).GetAwaiter().GetResult();
        protected internal List<T> GetDropDownList() => CreateSql().ToList();
        public abstract int? PageIndex { get; set; }
        public abstract int TotalPages { get; }
        public abstract bool HasNextPage { get; }
        public abstract bool HasPreviousPage { get; }
        public abstract int PageSize { get; set; }
        public abstract string CurrentFilter { get; set; }
        public abstract string SearchString { get; set; }
        public abstract string SortOrder { get; set; }
        public abstract string CurrentSort { get; }
    }

}



