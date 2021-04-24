using System;

namespace ReservationProject.Facade.Common {
    public abstract class BaseEntityView :IBaseEntityView {
        public string Id { get; set; }
        public byte[] RowVersion { get; set; }
    }
}




