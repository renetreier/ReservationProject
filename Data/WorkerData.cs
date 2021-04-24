using System.ComponentModel.DataAnnotations;
using ReservationProject.Data.Common;

namespace ReservationProject.Data
{ 
    public class WorkerData: BaseEntityData
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }

    }
}
