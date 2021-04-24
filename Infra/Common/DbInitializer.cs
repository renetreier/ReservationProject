using System;
using System.Linq;
using ReservationProject.Data;

namespace ReservationProject.Infra.Common {
    public static class DbInitializer {
        public static void Initialize(ApplicationDbContext dataBase) {
            if (dataBase.Workers.Any()) return;

            var workers = new WorkerData[]
            {
                new WorkerData { Id = "1", FirstName = "Tiit",LastName = "Kask",
                    Salary = 1500, Email = "tka@gmail.com" },
                new WorkerData { Id = "2",FirstName = "Kalle",LastName = "Pilli",
                    Salary = 1200, Email = "kastan23@gmail.com" },
                new WorkerData { Id = "3",FirstName = "Rene",LastName = "Treier",
                    Salary = 10000.05, Email = "Rene@gmail.com" }
            };
            dataBase.Workers.AddRange(workers);
            dataBase.SaveChanges();

            var rooms = new RoomData[]
            {
                new RoomData {Id ="1", RoomName = "Hiina Tuba", BuildingAddress = "Sütiste tee 14, Tallinn"},
                new RoomData {Id ="2", RoomName = "Aroomi Tuba", BuildingAddress = "Sütiste tee 14, Tallinn"},
                new RoomData {Id ="3", RoomName = "India Tuba", BuildingAddress = "Vaksali 13, Tartu"}
            };

            dataBase.Rooms.AddRange(rooms);
            dataBase.SaveChanges();
            var reservations = new ReservationData[]
            {
                new ReservationData
                {
                    ReservationDate = DateTime.Parse("2021-04-01"),
                    RoomId = rooms.Single(r=>r.RoomName=="Hiina Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Treier").Id,
                    Id="1001"
                },
                new ReservationData
                {
                    ReservationDate = DateTime.Parse("2021-04-02"),
                    RoomId = rooms.Single(r=>r.RoomName=="Hiina Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Treier").Id,
                    Id="1002"
                },
                new ReservationData
                {
                    ReservationDate = DateTime.Parse("2021-04-03"),
                    RoomId = rooms.Single(r=>r.RoomName=="Hiina Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Treier").Id,
                    Id="1003"
                },
                new ReservationData
                {
                    ReservationDate = DateTime.Parse("2021-04-01"),
                    RoomId = rooms.Single(r=>r.RoomName=="Aroomi Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Pilli").Id,
                    Id="2001"
                },
                new ReservationData
                {
                    ReservationDate = DateTime.Parse("2021-04-02"),
                    RoomId = rooms.Single(r=>r.RoomName=="Aroomi Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Pilli").Id,
                    Id="2002"
                },
                new ReservationData
                {
                    ReservationDate = DateTime.Parse("2021-04-03"),
                    RoomId = rooms.Single(r=>r.RoomName=="Aroomi Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Pilli").Id,
                    Id="2003"
                },
                new ReservationData
                {
                    ReservationDate = DateTime.Parse("2021-04-04"),
                    RoomId = rooms.Single(r=>r.RoomName=="India Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Kask").Id,
                    Id="3001"
                },

            };

            dataBase.Reservations.AddRange(reservations);
            dataBase.SaveChanges();
            
        }
    }
}