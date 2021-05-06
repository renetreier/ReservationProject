namespace ReservationProject.Domain.Repos
{
    public interface IWorkersRepo : IRepo<WorkerEntity>
    {
        WorkerEntity GetById(string workerId);
    }

}
