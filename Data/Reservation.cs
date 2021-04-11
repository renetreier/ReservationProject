using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

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
        [Display(Name = "Room")]
        public string RoomId { get; set; }

        public Room ReservedRoom { get; set; }
        [Required]
        [Display(Name = "Worker")]
        public string WorkerId { get; set; }
        public Worker ReservedWorker { get; set; }

    }
}
