using System.ComponentModel.DataAnnotations;
using ReservationProject.Core;

namespace ReservationProject.Data.Common {
    public abstract class BaseEntityData:IEntityData {
        public  string Id { get; set; }
        [Timestamp] public byte[] RowVersion { get; set; }
    }
}
