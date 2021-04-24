using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationProject.Domain.Repos {
    public interface IRepo<T> : IPagedRepo, IFilteredRepo, IOrderedRepo
    {
        string ErrorMessage { get; }
        public T EntityInDb { get; }
        Task<List<T>> Get();
        Task<T> Get(string id);
        Task<bool> Delete(T obj);
        Task<bool> Add(T obj);
        Task<bool> Update(T obj);
        T GetById(string id);
    }
}






