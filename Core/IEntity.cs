
namespace ReservationProject.Core
{
    public interface IEntity {
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
    }
}






