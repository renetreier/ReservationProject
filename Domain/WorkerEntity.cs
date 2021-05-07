using ReservationProject.Data;
using ReservationProject.Domain.Common;

namespace ReservationProject.Domain
{
    public sealed class WorkerEntity : BaseEntity<WorkerData>
    {
        public WorkerEntity() : this(null) { }
        public WorkerEntity(WorkerData d) : base(d) { }

        public string FirstName => Data?.FirstName ?? "Unspecified";
        public string LastName => Data?.LastName ?? "Unspecified";
        public string Email => Data?.Email ?? "Unspecified";

        public double Salary => Data?.Salary ?? 0.0;
        public string FullName => LastName + ", " + FirstName;
    }
}
