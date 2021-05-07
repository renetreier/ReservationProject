using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Infra;

namespace ReservationProject.Tests.Infra
{
    [TestClass]
    public class WorkersRepoTests
        :InMemoryRepoTests<WorkersRepo, WorkerEntity, WorkerData> {
        protected override WorkerEntity CreateEntity(WorkerData d) => new (d);

        protected override WorkersRepo CreateRepo(ApplicationDbContext c) => new (c);
    }
}