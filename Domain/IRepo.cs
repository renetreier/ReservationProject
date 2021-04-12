using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationProject.Domain {
    public interface IRepo<T> {
        //string ErrorMessage { get; }
        //public T EntityInDb { get; }
        Task<List<T>> Get();
        Task<T> Get(string id);
        Task Delete(T obj);
        Task Add(T obj);
        Task Update(T obj);
        T GetById(string id);
    }
}






