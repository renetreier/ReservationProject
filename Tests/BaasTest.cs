using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject;
using ReservationProject.Data;

namespace ReservationProject.Tests
{
    [TestClass]
    public class BaasTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var aa = new Room();
            Assert.AreEqual(9, Room.Liida(3, 6));
        }
        [TestMethod]
        public void TestMethod3()
        {
            //var aa = new Room();
            //Assert.AreEqual(9, Room.Liida(3, 6));
        }
    }
}