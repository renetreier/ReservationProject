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
        public async Task<bool> Add(TClass obj) => await Complete($"Add {obj?.Id}");
        public async Task<bool> Delete(TClass obj) => await Complete($"Delete {obj?.Id}");
        public async Task<bool> Update(TClass obj) => await Complete($"Update {obj?.Id}");
        public async Task<List<TClass>> Get() => await GetList("List");
        public async Task<TClass> Get(string id) => await Item($"Get {id}");
        public TClass GetById(string id) => GetWithId($"GetById {id}");
        private async Task<TClass> Item(string v) => await Complete(v, (TClass)Result);
        private async Task<List<TClass>> GetList(string v) => await Complete(v, (List<TClass>)Result);
        private async Task<TResult> Complete<TResult>(string s, TResult r)
        {
            await Complete(s);
            return r;
        }
        private async Task<bool> Complete(string s)
        {
            await Task.CompletedTask;
            Actions.Add(s);
            return Result is not null;
        }
        private TClass GetWithId(string s)
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
