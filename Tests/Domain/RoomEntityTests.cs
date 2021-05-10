using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Common;

namespace ReservationProject.Tests.Domain
{
    [TestClass]
    public class RoomEntityTests:SealedClassTests<Room,BaseEntity<RoomData>>
    {
        protected override Room GetObject() => new(GetRandom.ObjectOf<RoomData>());
        [TestMethod] public void RoomNameTest() => IsReadOnlyProperty(Obj.Data.RoomName ?? "Unspecified");
        [TestMethod] public void BuildingAddressTest() => IsReadOnlyProperty(Obj.Data.BuildingAddress ?? "Unspecified");
        
    }
}