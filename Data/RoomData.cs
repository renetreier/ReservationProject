using System.ComponentModel.DataAnnotations;
using ReservationProject.Data.Common;

namespace ReservationProject.Data
{
    public class RoomData: BaseEntityData
    {
        public string RoomName { get; set; }
        public string BuildingAddress { get; set; }
    }
}
