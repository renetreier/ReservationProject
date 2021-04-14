using System;
using System.ComponentModel.DataAnnotations;
using ReservationProject.Data;


namespace ReservationProject.Facade
{
    public class ReservationView
    {
        public string Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }
        public string RoomId { get; set; }
        public string WorkerId { get; set; }
        public Room ReservedRoom { get; set; }
        public Worker ReservedWorker { get; set; }
    }
}
