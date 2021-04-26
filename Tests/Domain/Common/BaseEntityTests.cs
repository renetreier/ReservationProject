using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain.Common;

namespace ReservationProject.Tests.Domain.Common {

    [TestClass]
    public class BaseEntityTests : AbstractClassTests<BaseEntity<RoomData>, object> {
        private class testClass :BaseEntity<RoomData> {
            public testClass(RoomData d = null) : base(d) { }
        }

        protected override BaseEntity<RoomData> getObject() => new testClass(GetRandom.ObjectOf<RoomData>());
        
        [TestMethod] public void DataTest() {
            isReadOnlyProperty<RoomData>();
            Assert.AreNotSame(obj.Data, obj.Data);
            Assert.AreNotEqual(obj.Data, obj.Data);
            arePropertiesEqual(obj.Data, obj.Data);
            var actual = obj.Data;
            var expected = GetRandom.ObjectOf<RoomData>();
            Copy.Members(expected, actual);
            arePropertiesEqual(expected, actual);
            arePropertiesNotEqual(expected, obj.Data);
        }

        [TestMethod] public void IdTest() => isReadOnlyProperty(obj.Data.Id);
        [TestMethod] public void RowVersionTest() => isReadOnlyProperty(obj.Data.RowVersion);
    }
}
