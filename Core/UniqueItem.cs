using System;

namespace ReservationProject.Core {
    public abstract class UniqueItem {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
