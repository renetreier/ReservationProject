using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Infra.Common;

namespace ReservationProject.Tests.Infra.Common
{
    [TestClass]
    public class OrderedRepoTests :AbstractClassTests<OrderedRepo<Room, RoomData>
        , FilteredRepo<Room, RoomData>> {
    }
}