using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Infra.Common;

namespace ReservationProject.Tests.Infra.Common
{
    [TestClass]
    public class FilteredRepoTests :AbstractClassTests<FilteredRepo<Room, RoomData>
        , CrudRepo<Room, RoomData>> {
    }
}