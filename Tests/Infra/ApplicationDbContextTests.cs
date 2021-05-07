using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Infra;

namespace ReservationProject.Tests.Infra {
    [TestClass]
    public class ApplicationDbContextTests
        : ClassTests<ApplicationDbContext, IdentityDbContext> {
    }
}
