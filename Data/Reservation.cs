using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationProject.Data
{
    public class Reservation
    {
        //TODO vaja scaffoldida 
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }
        [Required]
        public string RoomId { get; set; }
        [Required]
        public int WorkerId { get; set; }
        
    }
}
