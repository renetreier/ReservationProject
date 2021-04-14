using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Core;


//TODO Vaja lisada Igale poole Assemblyinfo, et testid ligi saaks
namespace ReservationProject.Tests.Pages
{
    public class PageModelTests<TEntity, TView> 
        where TEntity : class, IEntity, new()
        where TView : class, new()
    { 
        protected dynamic pageModel;
        protected TestRepo<TEntity> mockRepo;


        protected object OnGetDeleteAsync(string id, object result=null)
        {
            mockRepo.Result = result;
            return pageModel.OnGetDeleteAsync(id).GetAwaiter().GetResult();
        }
        protected object OnGetDetailsAsync(string id, object result = null)
        {
            mockRepo.Result = result;
            return pageModel.OnGetDetailsAsync(id).GetAwaiter().GetResult();
        }
        protected object OnGetEditAsync(string id, object result = null)
        {
            mockRepo.Result = result;
            return pageModel.OnGetEditAsync(id).GetAwaiter().GetResult();
        }
        //protected object OnGetCreate()
        //{
        //    return pageModel.OnGetCreate();
        //}
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
            pageModel.OnGetDeleteAsync("12345").GetAwaiter().GetResult();
            Assert.AreEqual("Get 12345", mockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnGetDeleteAsyncTestPageResult()
        {
            mockRepo.Result = new TEntity();
            var result = pageModel.OnGetDeleteAsync("12345").GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(PageResult));
        }
        
        [TestMethod]
        public void OnGetDetailsAsyncTestItemNotFound()
        {
            var result = OnGetDetailsAsync("");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetDetailsAsyncTestIdIsNull()
        {
            var result = pageModel.OnGetDetailsAsync(null).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetDetailsAsyncTestIsCallingGet()
        {
            pageModel.OnGetDetailsAsync("12345").GetAwaiter().GetResult();
            Assert.AreEqual("Get 12345", mockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnGetDetailsAsyncTestPageResult()
        {
            mockRepo.Result = new TEntity();
            var result = pageModel.OnGetDetailsAsync("12345").GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(PageResult));
        }
        [TestMethod]
        public void OnGetEditAsyncTestItemNotFound()
        {
            var result = OnGetEditAsync("");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetEditAsyncTestIdIsNull()
        {
            var result = pageModel.OnGetEditAsync(null).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetEditAsyncTestIsCallingGet()
        {
            pageModel.OnGetEditAsync("12345").GetAwaiter().GetResult();
            Assert.AreEqual("Get 12345", mockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnGetEditAsyncTestPageResult()
        {
            mockRepo.Result = new TEntity();
            var result = pageModel.OnGetEditAsync("12345").GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(PageResult));
        }

        [TestMethod]
        public void OnGetCreatePageResult()
        {
            var result = pageModel.OnGetCreate();
            Assert.IsInstanceOfType(result, typeof(PageResult));
        }
        [TestMethod]
        public void IsNullReturnTrue()
        {
            Assert.AreEqual(true, pageModel.IsNull(null));
        }
        [TestMethod]
        public void IsNullReturnFalse()
        {
            Assert.AreEqual(false, pageModel.IsNull(1));
        }
        [TestMethod]
        public void ToViewModelTestItemIsNull()
        {
            Assert.AreEqual(null, pageModel.ToViewModel(null));
        }
        //[TestMethod]
        //public void ToViewModelTestItemIsCorrect()
        //{
        //    mockRepo.Result = new TEntity();
        //    var result = new TView();
        //    Assert.AreEqual(result, pageModel.ToViewModel(mockRepo.Result));
        //} SEE HETKEL MINGI JAMA TEST, ei oska korda panna

    }
}