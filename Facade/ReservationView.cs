using System;
using System.ComponentModel.DataAnnotations;
using ReservationProject.Facade.Common;


namespace ReservationProject.Facade
{
    public sealed class ReservationView : BaseView
    {
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime ReservationDate { get; set; }
        [Display(Name = "Room")]
        [Required]
        public string RoomId { get; set; }
        [Display(Name = "Worker")]
        [Required]
        public string WorkerId { get; set; }
        [Display(Name="Worker Name")]
        public string WorkerName { get; set; }
        [Display(Name = "Room Name")]
        public string RoomName { get; set; }

    }
}
