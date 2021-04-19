using System.ComponentModel.DataAnnotations;
using ReservationProject.Core;

namespace ReservationProject.Data
{
    public class Room: BaseEntityData, IEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]

        [Display(Name = "Room name")]
        public string RoomName { get; set; }
        [Display(Name = "Building address")]
        [StringLength(50, MinimumLength = 3)]
        public string BuildingAddress { get; set; }
    }
}
