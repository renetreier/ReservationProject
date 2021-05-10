using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Facade;
using ReservationProject.Pages;
using ReservationProject.Tests.Pages.Common;

namespace ReservationProject.Tests.Pages
{
    

    [TestClass]
    public class RoomsPageTests:BasePageTests<Room, RoomView>
    {
        private class TestRoomsRepo : TestRepo<Room>, IRoomsRepo { }
       
        
        [TestInitialize]
        public void TestInitialize()
        {
            MockRepo = new TestRoomsRepo();
            PageModel = new RoomsPage((IRoomsRepo)MockRepo);
        }
    }
}

