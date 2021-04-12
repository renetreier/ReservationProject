
using ReservationProject.Data;
using ReservationProject.Domain;

namespace ReservationProject.Infra {
    public interface IWorkersRepo : IRepo<Worker> { }

    public sealed class WorkersRepo : BaseRepo<Worker>, IWorkersRepo
    {
        public WorkersRepo(ApplicationDbContext c) : base(c, c?.Workers) { }
    }
}










