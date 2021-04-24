using System;
using System.ComponentModel.DataAnnotations;
using ReservationProject.Facade.Common;


namespace ReservationProject.Facade
{
    public class ReservationView:BaseEntityView
    {
        [DataType(DataType.Date)]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }
        [Display(Name = "Room")]
        public string RoomId { get; set; }
        [Display(Name = "Worker")]
        public string WorkerId { get; set; }
        [Display(Name="Worker Name")]
        public string WorkerName { get; set; }
        [Display(Name = "Room Name")]
        public string RoomName { get; set; }

    }
}
