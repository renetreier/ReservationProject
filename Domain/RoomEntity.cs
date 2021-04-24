using ReservationProject.Data;
using ReservationProject.Domain.Common;

namespace ReservationProject.Domain
{
    public sealed class RoomEntity : BaseEntity<RoomData>
    {
        public RoomEntity() : this(null) { }
        public RoomEntity(RoomData d) : base(d) { }

        public string BuildingAddress => Data?.BuildingAddress ?? "Unspecified";
        public string RoomName => Data?.RoomName ?? "Unspecified";
       
    }
}
