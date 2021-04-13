using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationProject.Core;
using ReservationProject.Domain;

namespace ReservationProject.Infra {

    
    public abstract class BaseRepo<T> :IRepo<T> where T : class, IEntity, new() {
        protected readonly DbSet<T> set;
        private readonly DatabaseFacade db;

        protected BaseRepo(DbContext c = null, DbSet<T> s = null) {
            set = s;
            db = c?.Database;
        }

        public virtual async Task<List<T>> Get() => await set.AsNoTracking().ToListAsync();

        public async Task<T> Get(string id)
        {
            if (id == "") return null;
            return await set.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task Delete(T obj) => throw new NotImplementedException();
        public Task Add(T obj) => throw new NotImplementedException();
        public Task Update(T obj) => throw new NotImplementedException();
        public T GetById(string id)=> throw new NotImplementedException();

    }

}



