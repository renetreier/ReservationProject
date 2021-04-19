using System;
using System.ComponentModel.DataAnnotations;

using ReservationProject.Core;

namespace ReservationProject.Data
{
    public class Reservation:BaseEntityData,IEntity
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Reservation date")]
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
