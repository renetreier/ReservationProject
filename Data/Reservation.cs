using System;
using System.ComponentModel.DataAnnotations;

using ReservationProject.Core;

namespace ReservationProject.Data
{
    public class Reservation:BaseEntityData,IEntity
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }
        [Required]
        public string RoomId { get; set; }
        public Room ReservedRoom { get; set; }
        [Required]
        public string WorkerId { get; set; }
        public Worker ReservedWorker { get; set; }

    }
}
