using ReservationProject.Data;
using ReservationProject.Domain.Common;

namespace ReservationProject.Domain
{
    public sealed class Room : BaseEntity<RoomData>
    {
        public Room() : this(null) { }
        public Room(RoomData d) : base(d) { }

        public string BuildingAddress => Data?.BuildingAddress ?? "Unspecified";
        public string RoomName => Data?.RoomName ?? "Unspecified";
    }
}
