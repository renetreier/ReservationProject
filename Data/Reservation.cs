using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationProject.Data
{
    public class Reservation
    {
        //TODO key vajalik
        [Key]
        public string Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }
        [Required]
        public string RoomId { get; set; }
        [Required]
        public string WorkerId { get; set; }


    }
}
