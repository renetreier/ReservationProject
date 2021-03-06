using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Infra.Common;

namespace ReservationProject.Tests.Infra.Common
{
    [TestClass]
    public abstract class CrudRepoTests :AbstractClassTests<CrudRepo<Room, RoomData>
        , BaseRepo<Room, RoomData>> { }
}