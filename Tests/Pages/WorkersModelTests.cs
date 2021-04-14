using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Facade;
using ReservationProject.Infra;
using ReservationProject.Pages;



namespace ReservationProject.Tests.Pages
{
    [TestClass]
    public class WorkersModelTests : PageModelTests<Worker, WorkerView>
    {
        private class TestWorkerRepo : TestRepo<Worker>, IWorkersRepo { }

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepo = new TestWorkerRepo();
            pageModel = new WorkersModel((IWorkersRepo)mockRepo);
        }
    }
}