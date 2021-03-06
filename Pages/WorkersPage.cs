using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Domain;
using ReservationProject.Domain.Repos;
using ReservationProject.Facade;
using ReservationProject.Infra;
using ReservationProject.Pages.Common;

namespace ReservationProject.Pages
{
    public class WorkersPage : ViewPage<Worker, WorkerView>
    {
        public override string PageTitle => "Workers";

        public WorkersPage(ApplicationDbContext c) : this(new WorkersRepo(c), c) { }
        protected internal WorkersPage(IWorkersRepo r, ApplicationDbContext c = null) : base(r, c) { }
        protected internal override WorkerView ToViewModel(Worker w)
            => IsNull(w) ? null : Copy.Members(w, new WorkerView());

        protected internal override Worker ToEntity(WorkerView c)
        {
            if (IsNull(c)) return null;
            var d = Copy.Members(c, new WorkerData());
            return new Worker(d);
        }
    }
}
