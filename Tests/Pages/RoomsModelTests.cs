
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using ReservationProject.Infra;
using ReservationProject.Pages;

namespace ReservationProject.Tests.Pages
{
    

    [TestClass]
    public class RoomsModelTests:PageModelTests<Room, Room>
    {
        private class TestRoomsRepo : TestRepo<Room>, IRoomsRepo { }
       
        
        [TestInitialize]
        public void TestInitialize()
        {
            mockRepo = new TestRoomsRepo();
            pageModel = new RoomsModel((IRoomsRepo)mockRepo);
        }
        
        //public async Task<IActionResult> OnGetDeleteAsync(string id = "")
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Room = await db.Rooms.FirstOrDefaultAsync(m => m.RoomId == id);

        //    if (Room == null)
        //    {
        //        return NotFound();
        //    }
        //    return Page();
        //}






        //    [TestMethod]
        //    public void OnGetDeleteAsyncTestPageResult()
        //        => Assert.IsInstanceOfType(
        //            onGetDeleteAsync(GetRandom.Int32(), new TData()), typeof(PageResult));

        //    [TestMethod]
        //    public void OnGetDetailsAsyncTestItemNotFound()
        //        => Assert.IsInstanceOfType(onGetDetailsAsync(GetRandom.Int32()), typeof(NotFoundResult));
        //    [TestMethod]
        //    public void OnGetDetailsAsyncTestIsCallingGet()
        //    {
        //        var i = GetRandom.Int32();
        //        onGetDetailsAsync(i);
        //        Assert.AreEqual($"Get {i}", mockRepo.Actions[0]);
        //    }
        //    [TestMethod]
        //    public void OnGetDetailsAsyncTestPageResult()
        //        => Assert.IsInstanceOfType(
        //            onGetDetailsAsync(GetRandom.Int32(), new TData()), typeof(PageResult));

        //    [TestMethod]
        //    public void OnGetEditAsyncTestItemNotFound()
        //        => Assert.IsInstanceOfType(onGetEditAsync(GetRandom.Int32()), typeof(NotFoundResult));
        //    [TestMethod]
        //    public void OnGetEditAsyncTestIsCallingGet()
        //    {
        //        var i = GetRandom.Int32();
        //        onGetEditAsync(i);
        //        Assert.AreEqual($"Get {i}", mockRepo.Actions[0]);
        //    }
        //    [TestMethod]
        //    public void OnGetEditAsyncTestPageResult()
        //        => Assert.IsInstanceOfType(
        //            onGetEditAsync(GetRandom.Int32(), new TData()), typeof(PageResult));

        //    [TestMethod]
        //    public void OnGetCreateTestPageResult()
        //        => Assert.IsInstanceOfType(pageModel.OnGetCreate(), typeof(PageResult));
    }
}

