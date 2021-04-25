using Microsoft.AspNetCore.Mvc;
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
        protected dynamic PageModel;
        protected TestRepo<TData> MockRepo;


        protected object OnGetDeleteAsync(string id, object result=null)
        {
            MockRepo.Result = result;
            return PageModel.OnGetDeleteAsync(id).GetAwaiter().GetResult();
        }
        protected object OnGetDetailsAsync(string id, object result = null)
        {
            MockRepo.Result = result;
            return PageModel.OnGetDetailsAsync(id).GetAwaiter().GetResult();
        }
        protected object OnGetEditAsync(string id, object result = null)
        {
            MockRepo.Result = result;
            return PageModel.OnGetEditAsync(id).GetAwaiter().GetResult();
        }
        protected object OnPostCreateAsync(dynamic newItem = null)
        {
            PageModel.Item = newItem;
            return PageModel.OnPostCreateAsync().GetAwaiter().GetResult();
        }
        protected object OnPostDeleteAsync(string id, dynamic oldItem = null)
        {
            PageModel.Item = oldItem;
            return PageModel.OnPostDeleteAsync(id).GetAwaiter().GetResult();
        }
        protected object OnPostEditAsync(string id, dynamic oldItem = null)
        {
            PageModel.Item = oldItem;
            return PageModel.OnPostEditAsync(id).GetAwaiter().GetResult();
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
            var result = PageModel.OnGetDeleteAsync(null).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetDeleteAsyncTestIsCallingGet()
        {
            PageModel.OnGetDeleteAsync("12345").GetAwaiter().GetResult();
            Assert.AreEqual("Get 12345", MockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnGetDeleteAsyncTestPageResult()
        {
            MockRepo.Result = new TData();
            var result = PageModel.OnGetDeleteAsync("12345").GetAwaiter().GetResult();
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
            var result = PageModel.OnGetDetailsAsync(null).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetDetailsAsyncTestIsCallingGet()
        {
            PageModel.OnGetDetailsAsync("12345").GetAwaiter().GetResult();
            Assert.AreEqual("Get 12345", MockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnGetDetailsAsyncTestPageResult()
        {
            MockRepo.Result = new TData();
            var result = PageModel.OnGetDetailsAsync("12345").GetAwaiter().GetResult();
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
            var result = PageModel.OnGetEditAsync(null).GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void OnGetEditAsyncTestIsCallingGet()
        {
            PageModel.OnGetEditAsync("12345").GetAwaiter().GetResult();
            Assert.AreEqual("Get 12345", MockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnGetEditAsyncTestPageResult()
        {
            MockRepo.Result = new TData();
            var result = PageModel.OnGetEditAsync("12345").GetAwaiter().GetResult();
            Assert.IsInstanceOfType(result, typeof(PageResult));
        }
        [TestMethod]
        public void OnGetCreatePageResult()
        {
            var result = PageModel.OnGetCreate();
            Assert.IsInstanceOfType(result, typeof(PageResult));
        }
        [TestMethod]
        public void IsNullReturnTrue()
        {
            Assert.AreEqual(true, PageModel.IsNull(null));
        }
        [TestMethod]
        public void IsNullReturnFalse()
        {
            Assert.AreEqual(false, PageModel.IsNull(1));
        }
        [TestMethod]
        public void ToViewModelTestItemIsNull()
        {
            Assert.AreEqual(null, PageModel.ToViewModel(null));
        }
        [TestMethod]
        public void ToEntityTestItemIsNull()
        {
            Assert.AreEqual(null, PageModel.ToEntity(null));
        }
        [TestMethod]
        public void OnPostCreateTestIsCallingAdd()
        {
            var o = CreateNew.Instance<TView>();
            OnPostCreateAsync(o);
            Assert.AreEqual($"Add {o.Id}", MockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnPostDeleteTestIsCallingDelete()
        {
            var o = CreateNew.Instance<TView>();
            OnPostDeleteAsync(o.Id, o);
            Assert.AreEqual($"Delete {o.Id}", MockRepo.Actions[0]);
        }
        [TestMethod]
        public void OnPostEditTestIsCallingUpdate()
        {
            var o = CreateNew.Instance<TView>();
            OnPostEditAsync(o.Id, o);
            Assert.AreEqual($"Update {o.Id}", MockRepo.Actions[0]);
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
        public void IndexPageReturnsRedirectToPageResult()
        {
            var result = PageModel.IndexPage();
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
        }
    }
}