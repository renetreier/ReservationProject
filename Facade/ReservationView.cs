using System;
using System.ComponentModel.DataAnnotations;
using ReservationProject.Core;
using ReservationProject.Data;


namespace ReservationProject.Facade
{
    public class ReservationView:IEntity
    {
        public string Id { get; set; }
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }
        [Display(Name = "Room")]
        public string RoomId { get; set; }
        [Display(Name = "Worker")]
        public string WorkerId { get; set; }
        public Room ReservedRoom { get; set; }
        public Worker ReservedWorker { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
