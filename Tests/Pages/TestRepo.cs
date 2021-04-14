using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationProject.Core;

namespace ReservationProject.Tests.Pages
{
    public abstract class TestRepo<T> where T : IEntity
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

        public async Task Add(T obj) => await Perform($"Add {obj.Id}");

        public Task Update(T obj) => throw new System.NotImplementedException();

        public T GetById(string id) => throw new System.NotImplementedException();
        
    }
}
