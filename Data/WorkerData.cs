using ReservationProject.Data.Common;

namespace ReservationProject.Data
{ 
    public sealed class WorkerData: BaseData
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }
    }
}
