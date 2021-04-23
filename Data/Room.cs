using System.ComponentModel.DataAnnotations;
using ReservationProject.Core;
using ReservationProject.Data.Common;

namespace ReservationProject.Data
{
    public class Room: BaseEntityData, IEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string RoomName { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string BuildingAddress { get; set; }
    }
}
