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
        public async Task<bool> AddAsync(TClass obj) => await Complete($"Add {obj?.Id}");
        public async Task<bool> DeleteAsync(TClass obj) => await Complete($"Delete {obj?.Id}");
        public async Task<bool> UpdateAsync(TClass obj) => await Complete($"Update {obj?.Id}");

        public List<TClass> Get()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<TClass>> GetAsync() => await GetList("List");
        public async Task<TClass> GetAsync(string id) => await Item($"Get {id}");
        public TClass Get(string id) => GetWithId($"Get {id}");
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
        public string CurrentSort { get; } = "";
    }
}
