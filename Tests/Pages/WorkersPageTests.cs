using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Facade;
using ReservationProject.Pages;
using ReservationProject.Tests.Pages.Common;


namespace ReservationProject.Tests.Pages
{
    [TestClass]
    public class WorkersPageTests : BasePageTests<WorkerEntity, WorkerView>
    {
        private class TestWorkerRepo : TestRepo<WorkerEntity>, IWorkersRepo
        {
            public WorkerEntity GetById(string workerId)
            {
                throw new System.NotImplementedException();
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            MockRepo = new TestWorkerRepo();
            PageModel = new WorkersPage((IWorkersRepo)MockRepo);
        }
    }
}