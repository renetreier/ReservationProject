using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Pages;



namespace ReservationProject.Tests
{
    [TestClass]
    public class WorkerTests : BaasTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
            var context = db;
            pageModel = new WorkersModel(context);
        }

    }
}