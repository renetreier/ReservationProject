using System;

namespace ReservationProject.Facade.Common {
    public abstract class BaseEntityView :IBaseEntityView {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public byte[] RowVersion { get; set; }
    }
}




