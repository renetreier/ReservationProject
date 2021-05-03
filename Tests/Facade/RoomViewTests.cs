using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Facade;
using ReservationProject.Facade.Common;

namespace ReservationProject.Tests.Facade
{
    [TestClass]
    public class RoomViewTests : SealedClassTests<RoomView,BaseView>
    {
        [TestMethod] public void RoomNameTest() => IsReadWriteProperty<string>();
        [TestMethod] public void BuildingAddressTest() => IsReadWriteProperty<string>();
    }
}
