using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Core;
using ReservationProject.Facade;


//TODO Vaja lisada Igale poole Assemblyinfo, et testid ligi saaks
namespace ReservationProject.Tests.Pages
{
    public class PageModelTests<TEntity, TView> 
        where TEntity : class, IEntity, new()
        where TView : IEntity
    { 
        protected dynamic pageModel;
        protected TestRepo<TEntity> mockRepo;


        protected object OnGetAsync(object result = null)
        {
            mockRepo.Result = result;
            return pageModel.OnGetAsync().GetAwaiter().GetResult();
        }
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
        protected object OnPostCreateAsync(dynamic newItem = null)
        {
            pageModel.Item = newItem;
            return pageModel.OnPostCreateAsync().GetAwaiter().GetResult();
        }
        protected object OnPostDeleteAsync(string id, dynamic oldItem = null)
        {
            pageModel.Item = oldItem;
            return pageModel.OnPostDeleteAsync(id).GetAwaiter().GetResult();
        }
        protected object OnPostEditAsync(string id, dynamic oldItem = null)
        {
            pageModel.Item = oldItem;
            return pageModel.OnPostEditAsync(id).GetAwaiter().GetResult();
        }
        [TestMethod]
        public void OnGetDeleteAsyncTestItemNotFound()
        {
            var result = OnGetDeleteAsync("");
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
        [TestMethod]
        public void ToEntityTestItemIsNull()
        {
            Assert.AreEqual(null, pageModel.ToEntity(null));
        }
        [TestMethod]
        public void OnPostCreateTestIsCallingAdd()
        {
            var o = CreateNew.Instance<TView>();
            OnPostCreateAsync(o);
            Assert.AreEqual($"Add {o.Id}", mockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnPostDeleteTestIsCallingDelete()
        {
            var o = CreateNew.Instance<TView>();
            o.Id = "1";
            OnPostDeleteAsync(o.Id, o);
            Assert.AreEqual($"Delete {o.Id}", mockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnPostEditTestIsCallingEdit()
        {
            var o = CreateNew.Instance<TView>();
            o.Id = "1";
            OnPostEditAsync(o.Id, o);
            Assert.AreEqual($"Update {o.Id}", mockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnPostDeleteTestItemNotFound()
        {
            var result = OnPostDeleteAsync(null);
            Assert.IsInstanceOfType(result,typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnPostEditTestItemNotFound()
        {
            var result = OnPostEditAsync(null);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetAsyncReturnsList()
        {
            pageModel.OnGetAsync().GetAwaiter().GetResult();
            Assert.AreEqual("Get all 1", mockRepo.Actions[0]);
        }
        //[TestMethod]
        //public void ToViewModelTestItemIsCorrect()
        //{
        //    mockRepo.Result = new TEntity();
        //    var result = new TView();
        //    Assert.AreEqual(result, pageModel.ToViewModel(mockRepo.Result));
        //}

    }
}