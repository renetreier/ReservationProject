using System.ComponentModel.DataAnnotations;
using ReservationProject.Facade.Common;

namespace ReservationProject.Facade {
    public class WorkerView : BaseEntityView
    {

        [StringLength(50)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
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
