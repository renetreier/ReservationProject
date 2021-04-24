using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Facade;
using ReservationProject.Pages;



namespace ReservationProject.Tests.Pages
{
    [TestClass]
    public class ReservationsModelTests : PageModelTests<ReservationEntity, ReservationView>
    {
        private class TestReservationRepo : TestRepo<ReservationEntity>, IReservationsRepo { }

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepo = new TestReservationRepo();
            pageModel = new ReservationsPage((IReservationsRepo) mockRepo);
        }
    }
}