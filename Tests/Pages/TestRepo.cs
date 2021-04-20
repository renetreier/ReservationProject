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

        private async Task<List<T>> GetList(string v)
        {
            List<string> returnList = new();
            await Task.CompletedTask;
            returnList.Add(v);
            Actions.Add(v + $" {returnList.Count}");
            return (List<T>)Result;
        }

        public async Task<T> Get(string id) => await Perform($"Get {id}");
        public async Task<List<T>> Get() => await GetList("Get all");

        public async Task Delete(T obj) => await Perform($"Delete {obj.Id}");

        public async Task Add(T obj) => await Perform($"Add {obj.Id}");

        public async Task Update(T obj) => await Perform($"Update {obj.Id}");

        public T GetById(string id) => throw new System.NotImplementedException();
        
    }
}
