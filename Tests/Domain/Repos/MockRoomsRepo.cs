using ReservationProject.Domain.Repos;
using ReservationProject.Tests.Pages;
using ReservationProject.Domain;

namespace ReservationProject.Tests.Domain.Repos
{
    public class MockRoomsRepo :TestRepo<Room>, IRoomsRepo { }
}