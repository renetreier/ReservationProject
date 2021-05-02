using ReservationProject.Data.Common;

namespace ReservationProject.Data
{
    public sealed class RoomData: BaseData
    {
        public string RoomName { get; set; }
        public string BuildingAddress { get; set; }
    }
}
