using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationProject.Data
{
    public class ReservationTime
    {
   
        public int ReservationTimeId { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Reservation Time")]
        public DateTime ReservationDate { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int WorkerId { get; set; }
        
    }
}
