using ReservationProject.Domain.Repos;
using ReservationProject.Tests.Pages;
using ReservationProject.Domain;

namespace ReservationProject.Tests.Domain.Repos
{
    public class MockWorkersRepo : TestRepo<WorkerEntity>, IWorkersRepo
    {
        public WorkerEntity GetById(string workerId)
        {
            throw new System.NotImplementedException();
        }
    }
}