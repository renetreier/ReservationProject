using System;
using System.ComponentModel.DataAnnotations;
using ReservationProject.Data.Common;

namespace ReservationProject.Data
{
    public sealed class ReservationData:BaseData
    {
        public DateTime ReservationDate { get; set; }
        [Required]
        public string RoomId { get; set; }
        [Required]
        public string WorkerId { get; set; }
    }
}
