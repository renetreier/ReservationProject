using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ReservationProject.Core;

namespace ReservationProject.Data
{ 
    public class Worker: BaseEntityData, IEntity
    {
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Currency)]
        public double Salary { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

    }
}
