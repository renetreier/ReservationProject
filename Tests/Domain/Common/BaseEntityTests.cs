using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain.Common;

namespace ReservationProject.Tests.Domain.Common {

    [TestClass] public class BaseEntityTests : AbstractClassTests<BaseEntity<RoomData>, object>
    {
        private class TestClass :BaseEntity<RoomData>
        {
            public TestClass(RoomData d = null) : base(d) { }
        }

        protected override BaseEntity<RoomData> GetObject() => new TestClass(GetRandom.ObjectOf<RoomData>());
        
        [TestMethod] public void DataTest() 
        {
            IsReadOnlyProperty<RoomData>();
            Assert.AreNotSame(Obj.Data, Obj.Data);
            Assert.AreNotEqual(Obj.Data, Obj.Data);
            ArePropertiesEqual(Obj.Data, Obj.Data);
            var actual = Obj.Data;
            var expected = GetRandom.ObjectOf<RoomData>();
            Copy.Members(expected, actual);
            ArePropertiesEqual(expected, actual);
            ArePropertiesNotEqual(expected, Obj.Data);
        }

        [TestMethod] public void IdTest() => IsReadOnlyProperty(Obj.Data.Id);
        [TestMethod] public void RowVersionTest() => IsReadOnlyProperty(Obj.Data.RowVersion);
    }
}
