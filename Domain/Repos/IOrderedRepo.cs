namespace ReservationProject.Domain.Repos {
    public interface IOrderedRepo {
        public string SortOrder { get; set; }
        public string CurrentSort { get; }
    }
}