using System;
using ReservationProject.Data;
using ReservationProject.Domain.Common;
using ReservationProject.Domain.Repos;

namespace ReservationProject.Domain
{
    public sealed class ReservationEntity : BaseEntity<ReservationData>
    {
        public ReservationEntity() : this(null) { }

        public ReservationEntity(ReservationData d) : base(d)
        {
            LazyReadRoom = new Lazy<RoomEntity>(GetRoom);
            LazyReadWorker = new Lazy<WorkerEntity>(GetWorker);
        }
        //TODO ei tea kas siin või siis reservationDatas,
        //Todo aga ta ei löö errorit kui töötaja puudu, peab ka üle vaatama teised (arvatavasti "REQUIRED" puudu kuskil)
        
        public DateTime ReservationDate => Data?.ReservationDate ?? DateTime.MaxValue;
        public string RoomId => Data?.RoomId ?? "Unspecified";
        public string WorkerId => Data?.WorkerId ?? "Unspecified";
        public RoomEntity ReservedRoom => LazyReadRoom.Value;
        internal Lazy<RoomEntity> LazyReadRoom { get; }
        private RoomEntity GetRoom()
            => new GetRepo().Instance<IRoomsRepo>()?.GetById(RoomId);

        public WorkerEntity ReservedWorker => LazyReadWorker.Value;
        internal Lazy<WorkerEntity> LazyReadWorker { get; }
        private WorkerEntity GetWorker()
            => new GetRepo().Instance<IWorkersRepo>()?.GetById(WorkerId);
    }
}
