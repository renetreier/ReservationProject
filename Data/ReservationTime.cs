using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Display(Name = "Room")]
        public string RoomName { get; set; }
        
    }
}
