using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Facade;
using ReservationProject.Pages;


namespace ReservationProject.Tests.Pages
{
    [TestClass]
    public class WorkersModelTests : PageModelTests<WorkerEntity, WorkerView>
    {
        private class TestWorkerRepo : TestRepo<WorkerEntity>, IWorkersRepo { }

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepo = new TestWorkerRepo();
            pageModel = new WorkersPage((IWorkersRepo)mockRepo);
        }
    }
}