using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationProject.Domain.Repos {
    public interface IRepo<T> : IPagedRepo, IFilteredRepo, IOrderedRepo
    {
        string ErrorMessage { get; }
        public T EntityInDb { get; }
        Task<List<T>> GetAsync();
        Task<T> GetAsync(string id);
        Task<bool> DeleteAsync(T obj);
        Task<bool> AddAsync(T obj);
        Task<bool> UpdateAsync(T obj);
        T Get(string id);
        List<T> Get();

    }
}






