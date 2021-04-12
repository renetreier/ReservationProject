using System;
using System.ComponentModel.DataAnnotations;

using ReservationProject.Core;

namespace ReservationProject.Data
{
    public class Reservation:IEntity
    {
        [Display(Name = "Reservation number")]
        public string Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }
        [Required]
        public string RoomId { get; set; }
        public Room ReservedRoom { get; set; }
        [Required]
        public string WorkerId { get; set; }
        public Worker ReservedWorker { get; set; }

    }
}
