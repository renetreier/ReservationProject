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
            room = new Lazy<RoomEntity>(GetRoom);
            worker = new Lazy<WorkerEntity>(GetWorker);
        }

        public DateTime ReservationDate => Data?.ReservationDate ?? DateTime.MaxValue;
        public string RoomId => Data?.RoomId ?? "Unspecified";
        public string WorkerId => Data?.WorkerId ?? "Unspecified";
        public RoomEntity ReservedRoom => room.Value;
        internal Lazy<RoomEntity> room { get; }
        private RoomEntity GetRoom()
            => new GetRepo().Instance<IRoomsRepo>()?.GetById(RoomId);

        public WorkerEntity ReservedWorker => worker.Value;
        internal Lazy<WorkerEntity> worker { get; }
        private WorkerEntity GetWorker()
            => new GetRepo().Instance<IWorkersRepo>()?.GetById(WorkerId);
    }
}
