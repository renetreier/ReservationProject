using System;
using ReservationProject.Data;
using ReservationProject.Domain.Common;
using ReservationProject.Domain.Repos;

namespace ReservationProject.Domain
{
    public sealed class Reservation : BaseEntity<ReservationData>
    {
        public Reservation() : this(null) { }

        public Reservation(ReservationData d) : base(d)
        {
            LazyReadRoom = GetLazy<Room, IRoomsRepo>(x => x?.Get(RoomId));
            LazyReadWorker = GetLazy<Worker,IWorkersRepo>(x => x?.Get(WorkerId));
        }
       
        public DateTime ReservationDate => Data?.ReservationDate ?? DateTime.MaxValue;

        public string RoomId => Data?.RoomId ?? "Unspecified";
        public string WorkerId => Data?.WorkerId ?? "Unspecified";

        public Room ReservedRoom => LazyReadRoom.Value;
        internal Lazy<Room> LazyReadRoom { get; }

        public Worker ReservedWorker => LazyReadWorker.Value;
        internal Lazy<Worker> LazyReadWorker { get; }

    }
}
