using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Infra;

//TODO Vaja lisada Igale poole Assemblyinfo, et testid ligi saaks
namespace ReservationProject.Tests
{
    [TestClass]
    public class BaasTest
    {
        protected readonly ApplicationDbContext db;
        protected dynamic pageModel;

      
        protected object OnGetDeleteAsync(string id = "")
        {
            return pageModel.OnGetDeleteAsync(id).GetAwaiter().GetResult();
        }
        protected object OnPostDeleteAsync(string id = "")
        {
            return pageModel.OnPostDeleteAsync(id).GetAwaiter().GetResult();
        }
        protected object OnGetDetailsAsync(string id = "")
        {
            return pageModel.OnGetDetailsAsync(id).GetAwaiter().GetResult();
        }
        protected object OnGetEditAsync(string id = "")
        {
            return pageModel.OnGetEditAsync(id).GetAwaiter().GetResult();
        }
        protected object OnPostEditAsync(string id = "")
        {
            return pageModel.OnPostEditAsync(id).GetAwaiter().GetResult();
        }
        protected object OnGetCreate()
        {
            return pageModel.OnGetCreate();
        }
        [TestMethod]
        public void OnGetDeleteAsyncTestItemNotFound()
        {
            var result = OnGetDeleteAsync("");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnPostDeleteAsyncTestItemNotFound()
        {
            var result = OnPostDeleteAsync("");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetEditAsyncTestItemNotFound()
        {
            var result = OnGetEditAsync("");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnPostEditAsyncTestItemNotFound()
        {
            var result = OnPostEditAsync("");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetDetailsAsyncTestItemNotFound()
        {
            var result = OnGetDetailsAsync("");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetCreatePageResult()
        {
            var result = OnGetCreate();
            Assert.IsInstanceOfType(result, typeof(PageResult));
        }
        

    }
}