using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Data.Common;

namespace ReservationProject.Tests.Data
{
    [TestClass]
    public class RoomDataTests : SealedClassTests<RoomData,BaseData>
    {
        [TestMethod] public void RoomNameTest() => IsReadWriteProperty<string>();
        [TestMethod] public void BuildingAddressTest() => IsReadWriteProperty<string>();
    }
}
