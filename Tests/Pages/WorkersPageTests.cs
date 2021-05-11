using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Facade;
using ReservationProject.Pages;
using ReservationProject.Tests.Pages.Common;


namespace ReservationProject.Tests.Pages
{
    [TestClass] public class WorkersPageTests : BasePageTests<Worker, WorkerView>
    {
        private class TestWorkerRepo : TestRepo<Worker>, IWorkersRepo { }

        [TestInitialize] public void TestInitialize()
        {
            MockRepo = new TestWorkerRepo();
            PageModel = new WorkersPage((IWorkersRepo)MockRepo);
        }
    }
}