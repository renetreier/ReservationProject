using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationProject.Tests.Pages
{
    public abstract class TestRepo<T>
    {
        public List<string> Actions { get; } = new();
        public object Result { get; set; } = null;
        private async Task<T> Perform(string v)
        {
            await Task.CompletedTask;
            Actions.Add(v);
            return (T) Result;
        }
        public async Task<T> Get(string id) => await Perform($"Get {id}");
        public Task<List<T>> Get() => throw new System.NotImplementedException();

        public Task Delete(T obj) => throw new System.NotImplementedException();

        public Task Add(T obj) => throw new System.NotImplementedException();

        public Task Update(T obj) => throw new System.NotImplementedException();

        public T GetById(string id) => throw new System.NotImplementedException();
        
    }
}
