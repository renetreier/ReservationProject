using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Infra.Common;

namespace ReservationProject.Tests.Infra.Common
{
    [TestClass]
    public class PagedRepoTests :AbstractClassTests<PagedRepo<RoomEntity, RoomData>
        , OrderedRepo<RoomEntity, RoomData>>
    {
        [TestMethod] public void PageIndexTest() => IsProperty<int?>();
        [TestMethod] public void TotalPagesTest() => IsReadOnlyProperty<int>();
        [TestMethod] public void HasNextPageTest() => IsReadOnlyProperty<bool>();
        [TestMethod] public void HasPreviousPageTest() => IsReadOnlyProperty<bool>();
        [TestMethod] public void PageSizeTest() => IsProperty<int>();
    }
}