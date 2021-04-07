using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Data;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationProject.Infra;
using ReservationProject.Soft.Pages;
using ReservationProject.Soft.Pages.Rooms;


namespace ReservationProject.Tests
{
    [TestClass]
    public class RoomTest:BaasTest
    {
        protected readonly ApplicationDbContext db;
        protected dynamic pageModel;

        [TestInitialize]
        public void TestInitialize()
        {
            var context = db;
            pageModel = new RoomsModel(context);
        }

       
        private object OnGetDeleteAsync(string id="")
        {
            return pageModel.OnGetDeleteAsync(id).GetAwaiter().GetResult();
        }
        [TestMethod]
        public void OnGetDeleteAsyncTestItemNotFound()
        {
            var result = OnGetDeleteAsync("");
            Assert.IsInstanceOfType(result,typeof(NotFoundResult));
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

