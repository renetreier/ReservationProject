using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Common;

namespace ReservationProject.Tests.Domain
{
    [TestClass]
    public class ReservationEntityTests:SealedClassTests<Reservation,BaseEntity<ReservationData>>
    {
        protected override Reservation GetObject() => new(GetRandom.ObjectOf<ReservationData>());
        
        [TestMethod]
        public void LazyReadRoomTest() => LazyTest(() => Obj.LazyReadRoom.IsValueCreated,
            () => Obj.ReservedRoom);
        [TestMethod]
        public void LazyReadWorkerTest() => LazyTest(() => Obj.LazyReadRoom.IsValueCreated,
            () => Obj.ReservedRoom);
       
       
        [TestMethod] public void ReservationDateTest() => IsReadOnlyProperty(Obj.Data.ReservationDate);
        [TestMethod] public void WorkerIdTest() => IsReadOnlyProperty(Obj.Data.WorkerId);
        [TestMethod] public void RoomIdTest() => IsReadOnlyProperty(Obj.Data.RoomId);
        
    }
}