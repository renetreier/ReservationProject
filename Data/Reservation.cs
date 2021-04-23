using System;
using System.ComponentModel.DataAnnotations;

using ReservationProject.Core;
using ReservationProject.Data.Common;

namespace ReservationProject.Data
{
    public class Reservation:BaseEntityData,IEntity
    {
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
