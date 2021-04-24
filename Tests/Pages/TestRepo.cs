using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationProject.Core;
using ReservationProject.Domain.Repos;

namespace ReservationProject.Tests.Pages
{
    public abstract class TestRepo<TClass> : IRepo<TClass> where TClass : IBaseEntity
    {
        public string ErrorMessage { get; set; }
        public TClass EntityInDb { get; set; }
        public object Result { get; set; } = null;
        public List<string> Actions { get; } = new();
        public async Task<bool> Add(TClass obj) => await complete($"Add {obj?.Id}");
        public async Task<bool> Delete(TClass obj) => await complete($"Delete {obj?.Id}");
        public async Task<bool> Update(TClass obj) => await complete($"Update {obj?.Id}");
        public async Task<List<TClass>> Get() => await list("List");
        public async Task<TClass> Get(string id) => await item($"Get {id}");
        public TClass GetById(string id) => get($"GetById {id}");
        private async Task<TClass> item(string v) => await complete(v, (TClass)Result);
        private async Task<List<TClass>> list(string v) => await complete(v, (List<TClass>)Result);
        private async Task<TResult> complete<TResult>(string s, TResult r)
        {
            await complete(s);
            return r;
        }
        private async Task<bool> complete(string s)
        {
            await Task.CompletedTask;
            Actions.Add(s);
            return Result is not null;
        }
        private TClass get(string s)
        {
            Actions.Add(s);
            return (TClass)Result;
        }
        public int? PageIndex { get; set; }
        public int TotalPages { get; } = 0;
        public bool HasNextPage { get; } = false;
        public bool HasPreviousPage { get; } = false;
        public int PageSize { get; set; }
        public string CurrentFilter { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }
    }
}
