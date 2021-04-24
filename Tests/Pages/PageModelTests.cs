﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Core;


//TODO Vaja lisada Igale poole Assemblyinfo, et testid ligi saaks
namespace ReservationProject.Tests.Pages
{
    public class PageModelTests<TData, TView>
       where TData : IBaseEntity, new()
       where TView : IEntityData
    {
        protected dynamic pageModel;
        protected TestRepo<TData> mockRepo;


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
            mockRepo.Result = new TData();
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
            mockRepo.Result = new TData();
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
            mockRepo.Result = new TData();
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
            OnPostDeleteAsync(o.Id, o);
            Assert.AreEqual($"Delete {o.Id}", mockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnPostEditTestIsCallingUpdate()
        {
            var o = CreateNew.Instance<TView>();
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
            pageModel.OnGetIndexAsync().GetAwaiter().GetResult();
            Assert.AreEqual("Get all 1", mockRepo.Actions[0]);
        }

    }
}