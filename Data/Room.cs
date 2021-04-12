using System.ComponentModel.DataAnnotations;
using ReservationProject.Core;

namespace ReservationProject.Data
{
    public class Room:IEntity
    {
        
        public string Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Room Name")]
        public string RoomName { get; set; }
        [Display(Name = "Address")]
        [StringLength(50, MinimumLength = 3)]
        public string BuildingAddress { get; set; }


    }
}
