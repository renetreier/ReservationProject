using ReservationProject.Core;
using System.ComponentModel.DataAnnotations;

namespace ReservationProject.Facade {
    public class WorkerView : IEntity
    {
        public string Id { get; set; }

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
        public string FullName { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
