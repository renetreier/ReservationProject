using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;

//TODO Vaja lisada Igale poole Assemblyinfo, et testid ligi saaks
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