using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Domain.Common;
using ReservationProject.Domain.Repos;
using ReservationProject.Tests.Domain.Repos;

namespace ReservationProject.Tests.Domain.Common {

    [TestClass] public class GetRepoTests :SealedClassTests<GetRepo, object> {
        private IServiceProvider provider { get; set; }
        protected override GetRepo getObject() => new(provider);
        [TestCleanup] public void TestCleanup() => GetRepo.SetProvider(null);
        [TestMethod] public void ProviderIsNullTest() => isNull(obj._provider);
        [TestMethod] public void InstanceIsNullTest() => isNull(GetRepo.instance);
        [TestMethod]
        public void CanCreateTest()
        {
            initMock();
            areNotEqual(provider, GetRepo.instance);
            areEqual(provider, obj._provider);
        }
        [TestMethod]
        public void SetProviderTest()
        {
            var p = new MockServiceProvider(null);
            GetRepo.SetProvider(p);
            isNull(obj._provider);
            areEqual(p, GetRepo.instance);
        }
        [TestMethod]
        public void CreateAfterSetTest()
        {
            var p = new MockServiceProvider(null);
            GetRepo.SetProvider(p);
            obj = new GetRepo();
            areEqual(p, GetRepo.instance);
            areEqual(p, obj._provider);
        }
        [TestMethod]
        public void InstanceWithTypeTest()
        {
            var repo = initMock();
            var r = obj.Instance(typeof(IRoomsRepo));
            areEqual(repo, r);
        }
        [TestMethod]
        public void InstanceTest()
        {
            var repo = initMock();
            var r = obj.Instance<IRoomsRepo>();
            areEqual(repo, r);
        }
        private MockRoomsRepo initMock()
        {
            var r = new MockRoomsRepo();
            provider = new MockServiceProvider(r);
            obj = getObject();
            return r;
        }
    }
}