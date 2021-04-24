using System;
using System.ComponentModel.DataAnnotations;
using ReservationProject.Data.Common;

namespace ReservationProject.Data
{
    public class ReservationData:BaseEntityData
    {
       
        public DateTime ReservationDate { get; set; }
        public string RoomId { get; set; }
        public string WorkerId { get; set; }

    }
}
