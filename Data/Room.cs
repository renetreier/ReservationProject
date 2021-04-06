﻿using System.ComponentModel.DataAnnotations;

namespace ReservationProject.Data
{
    public class Room
    {
        [Display(Name = "Room Number")]
        public int RoomId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Room Name")]
        public string RoomName { get; set; }
        [Display(Name = "Address")]
        [StringLength(50, MinimumLength = 3)]
        public string BuildingAddress { get; set; }

    }
}
