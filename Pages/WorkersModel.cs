using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReservationProject.Aids;
using ReservationProject.Data;
using ReservationProject.Facade;
using ReservationProject.Infra;

namespace ReservationProject.Pages
{
    public class WorkersModel : BasePageModel<Worker, WorkerView>
    {
        public override string PageTitle => "Worker";

        public WorkersModel(ApplicationDbContext c) : this(new WorkersRepo(c), c) { }
        protected internal WorkersModel(IWorkersRepo r, ApplicationDbContext c = null) : base(r, c) { }
        protected internal override WorkerView ToViewModel(Worker w)
            => IsNull(w) ? null : Copy.Members(w, new WorkerView());
        
        protected internal override Worker ToEntity(WorkerView w)
            => IsNull(w) ? null : Copy.Members(w, new Worker());

    }
}
