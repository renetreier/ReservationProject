using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationProject.Data
{
    public class Reservation
    {
        [Display(Name = "Reservation number")]
        public string ReservationId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }
        [Required]
        public string RoomId { get; set; }
        [Required]
        public string WorkerId { get; set; }
    }
}
