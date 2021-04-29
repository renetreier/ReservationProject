using System.ComponentModel.DataAnnotations;
using ReservationProject.Facade.Common;

namespace ReservationProject.Facade
{
    public class RoomView: BaseEntityView
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Room Name")]
        public string RoomName { get; set; }
        [Display(Name = "Address")]
        [StringLength(50, MinimumLength = 3)]
        public string BuildingAddress { get; set; }
    }
}
