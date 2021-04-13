using System.ComponentModel.DataAnnotations;
using ReservationProject.Core;

namespace ReservationProject.Data {
    public abstract class BaseEntityData {
        public  string Id { get; set; }
        [Timestamp] public byte[] RowVersion { get; set; }
    }
}
