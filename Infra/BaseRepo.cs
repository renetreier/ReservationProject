using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationProject.Core;
using ReservationProject.Domain;

namespace ReservationProject.Infra {

    
    public abstract class BaseRepo<T> :IRepo<T> where T : class, IEntity, new() {
        protected readonly DbSet<T> Set;
        private readonly DatabaseFacade db;

        protected BaseRepo(DbContext c = null, DbSet<T> s = null) {
            Set = s;
            db = c?.Database;
        }

        public virtual async Task<List<T>> Get() => await Set.AsNoTracking().ToListAsync();

        public async Task<T> Get(string id)
        {
            if (id == "") return null;
            return await Set.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Delete(T obj)
        {
            await Task.CompletedTask;
            Set.Remove(obj);
        } 

        public async Task Add(T obj)
        {
            obj.Id = Guid.NewGuid().ToString();
            await Set.AddAsync(obj);
        }

        public async Task Update(T obj)
        {
            await Task.CompletedTask;
            Set.Update(obj);
        } 
        public T GetById(string id)=> throw new NotImplementedException();

    }

}



