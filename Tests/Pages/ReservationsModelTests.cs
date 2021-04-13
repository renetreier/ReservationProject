using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Infra;
using ReservationProject.Pages;



namespace ReservationProject.Tests.Pages
{
    [TestClass]
    public class ReservationsModelTests : PageModelTests<Reservation, Reservation>
    {
        private class TestReservationRepo : TestRepo<Reservation>, IReservationsRepo { }

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepo = new TestReservationRepo();
            pageModel = new ReservationsModel((IReservationsRepo) mockRepo);
        }
    }
}