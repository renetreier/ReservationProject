using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Facade;
using ReservationProject.Pages;

namespace ReservationProject.Tests.Pages
{
    

    [TestClass]
    public class RoomsModelTests:PageModelTests<RoomEntity, RoomView>
    {
        private class TestRoomsRepo : TestRepo<RoomEntity>, IRoomsRepo { }
       
        
        [TestInitialize]
        public void TestInitialize()
        {
            mockRepo = new TestRoomsRepo();
            pageModel = new RoomsPage((IRoomsRepo)mockRepo);
        }
    }
}

