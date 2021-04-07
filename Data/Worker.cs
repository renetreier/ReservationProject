using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationProject.Data
{
    //TODO "ID" Teha GUID-tüüpi(vaata roomi pealt)
    //TODO vaja scaoffilda 
    public class Worker
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
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
