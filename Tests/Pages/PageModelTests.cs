using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Infra;
using ReservationProject.Pages;

//TODO Vaja lisada Igale poole Assemblyinfo, et testid ligi saaks
namespace ReservationProject.Tests.Pages
{
    public class PageModelTests<T> where T:new()
    { 
        protected dynamic pageModel;
        protected TestRepo<T> mockRepo;


        protected object OnGetDeleteAsync(string id, object result=null)
        {
            mockRepo.Result = result;
            return pageModel.OnGetDeleteAsync(id).GetAwaiter().GetResult();
        }
        [TestMethod]
        public void OnGetDeleteAsyncTestItemNotFound()
        {
            var result = pageModel.OnGetDeleteAsync("12345").GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetDeleteAsyncTestIdIsNull()
        {
            var result = pageModel.OnGetDeleteAsync(null).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetDeleteAsyncTestIsCallingGet()
        {
            var result = pageModel.OnGetDeleteAsync("12345").GetAwaiter().GetResult();
            Assert.AreEqual("Get 12345", mockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnGetDeleteAsyncTestPageResult()
        {
            mockRepo.Result = new T();
            var result = pageModel.OnGetDeleteAsync("12345").GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(PageResult));
        }
        //    protected object OnPostDeleteAsync(string id = "")
        //    {
        //        return pageModel.OnPostDeleteAsync(id).GetAwaiter().GetResult();
        //    }
        //    protected object OnGetDetailsAsync(string id = "")
        //    {
        //        return pageModel.OnGetDetailsAsync(id).GetAwaiter().GetResult();
        //    }
        //    protected object OnGetEditAsync(string id = "")
        //    {
        //        return pageModel.OnGetEditAsync(id).GetAwaiter().GetResult();
        //    }
        //    protected object OnPostEditAsync(string id = "")
        //    {
        //        return pageModel.OnPostEditAsync(id).GetAwaiter().GetResult();
        //    }
        //    protected object OnGetCreate()
        //    {
        //        return pageModel.OnGetCreate();
        //    }
        //    [TestMethod]
        //    public void OnGetDeleteAsyncTestItemNotFound()
        //    {
        //        var result = pageModel.OnGetDeleteAsync("").GetAwaiter().GetResult();
        //        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //    }
        //    [TestMethod]
        //    public void OnPostDeleteAsyncTestItemNotFound()
        //    {
        //        var result = OnPostDeleteAsync("");
        //        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //    }
        //    [TestMethod]
        //    public void OnGetEditAsyncTestItemNotFound()
        //    {
        //        var result = OnGetEditAsync("");
        //        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //    }
        //    [TestMethod]
        //    public void OnPostEditAsyncTestItemNotFound()
        //    {
        //        var result = OnPostEditAsync("");
        //        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //    }
        //    [TestMethod]
        //    public void OnGetDetailsAsyncTestItemNotFound()
        //    {
        //        var result = OnGetDetailsAsync("");
        //        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        //    }
        //    [TestMethod]
        //    public void OnGetCreatePageResult()
        //    {
        //        var result = OnGetCreate();
        //        Assert.IsInstanceOfType(result, typeof(PageResult));
        //    }
    }
}