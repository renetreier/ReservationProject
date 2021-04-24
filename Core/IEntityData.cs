namespace ReservationProject.Core
{
    public interface IEntityData : IBaseEntity
    {
        public new string Id { get; set; }
        public new byte[] RowVersion { get; set; }
    }
}






