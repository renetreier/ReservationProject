using System.ComponentModel.DataAnnotations;
using ReservationProject.Core;

namespace ReservationProject.Data.Common {
    public abstract class BaseData:UniqueItem, IEntityData {
        [Timestamp] public byte[] RowVersion { get; set; }
    }
}
