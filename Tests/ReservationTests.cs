using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Infra;
using ReservationProject.Pages;



namespace ReservationProject.Tests
{
    [TestClass]
    public class ReservationTests : BaasTest
    {
       

        [TestInitialize]
        public void TestInitialize()
        {
            var context = db;
            pageModel = new ReservationsModel(context);
        }
        protected object LoadReservationsAsync(string id = "")
        {
            return pageModel.LoadReservation(id).GetAwaiter().GetResult();
        }
        [TestMethod]
        public void LoadReservationAsyncTestItemNotFound()
        {
            var result = LoadReservationsAsync("");
            Assert.AreEqual(result, false);
        }

    }
}