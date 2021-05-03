using ReservationProject.Core;

namespace ReservationProject.Facade.Common
{
    public abstract class BaseView : UniqueItem, IBaseEntityView  {
        public byte[] RowVersion { get; set; }
    }
}




