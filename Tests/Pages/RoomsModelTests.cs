
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
    public class RoomsModelTests:PageModelTests<Room, RoomView>
    {
        private class TestRoomsRepo : TestRepo<Room>, IRoomsRepo { }
       
        
        [TestInitialize]
        public void TestInitialize()
        {
            mockRepo = new TestRoomsRepo();
            pageModel = new RoomsModel((IRoomsRepo)mockRepo);
        }
    }
}

