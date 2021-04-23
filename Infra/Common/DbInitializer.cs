using System;
using System.Linq;
using ReservationProject.Data;

namespace ReservationProject.Infra.Common {
    public static class DbInitializer {
        public static void Initialize(ApplicationDbContext dataBase) {
            if (dataBase.Workers.Any()) return;

            var workers = new Worker[]
            {
                new Worker { Id = "1", FirstName = "Tiit",LastName = "Kask",
                    Salary = 1500, Email = "tka@gmail.com" },
                new Worker { Id = "2",FirstName = "Kalle",LastName = "Pilli",
                    Salary = 1200, Email = "kastan23@gmail.com" },
                new Worker { Id = "3",FirstName = "Rene",LastName = "Treier",
                    Salary = 10000.05, Email = "Rene@gmail.com" }
            };
            dataBase.Workers.AddRange(workers);
            dataBase.SaveChanges();

            var rooms = new Room[]
            {
                new Room {Id ="1", RoomName = "Hiina Tuba", BuildingAddress = "Sütiste tee 14, Tallinn"},
                new Room {Id ="2", RoomName = "Aroomi Tuba", BuildingAddress = "Sütiste tee 14, Tallinn"},
                new Room {Id ="3", RoomName = "India Tuba", BuildingAddress = "Vaksali 13, Tartu"}
            };

            dataBase.Rooms.AddRange(rooms);
            dataBase.SaveChanges();
            var reservations = new Reservation[]
            {
                new Reservation
                {
                    ReservationDate = DateTime.Parse("2021-04-01"),
                    RoomId = rooms.Single(r=>r.RoomName=="Hiina Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Treier").Id,
                    Id="1001",
                    ReservedRoom = rooms.Single(r=>r.RoomName=="Hiina Tuba"),
                    ReservedWorker = workers.Single(w=>w.LastName=="Treier")
                },
                new Reservation
                {
                    ReservationDate = DateTime.Parse("2021-04-02"),
                    RoomId = rooms.Single(r=>r.RoomName=="Hiina Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Treier").Id,
                    Id="1002",
                    ReservedRoom = rooms.Single(r=>r.RoomName=="Hiina Tuba"),
                    ReservedWorker = workers.Single(w=>w.LastName=="Treier")
                },
                new Reservation
                {
                    ReservationDate = DateTime.Parse("2021-04-03"),
                    RoomId = rooms.Single(r=>r.RoomName=="Hiina Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Treier").Id,
                    Id="1003",
                    ReservedRoom = rooms.Single(r=>r.RoomName=="Hiina Tuba"),
                    ReservedWorker = workers.Single(w=>w.LastName=="Treier")
                },
                new Reservation
                {
                    ReservationDate = DateTime.Parse("2021-04-01"),
                    RoomId = rooms.Single(r=>r.RoomName=="Aroomi Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Pilli").Id,
                    Id="2001",
                    ReservedRoom = rooms.Single(r=>r.RoomName=="Aroomi Tuba"),
                    ReservedWorker = workers.Single(w=>w.LastName=="Pilli")
                },
                new Reservation
                {
                    ReservationDate = DateTime.Parse("2021-04-02"),
                    RoomId = rooms.Single(r=>r.RoomName=="Aroomi Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Pilli").Id,
                    Id="2002",
                    ReservedRoom = rooms.Single(r=>r.RoomName=="Aroomi Tuba"),
                    ReservedWorker = workers.Single(w=>w.LastName=="Pilli")
                },
                new Reservation
                {
                    ReservationDate = DateTime.Parse("2021-04-03"),
                    RoomId = rooms.Single(r=>r.RoomName=="Aroomi Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Pilli").Id,
                    Id="2003",
                    ReservedRoom = rooms.Single(r=>r.RoomName=="Aroomi Tuba"),
                    ReservedWorker = workers.Single(w=>w.LastName=="Pilli")
                },
                new Reservation
                {
                    ReservationDate = DateTime.Parse("2021-04-04"),
                    RoomId = rooms.Single(r=>r.RoomName=="India Tuba").Id,
                    WorkerId = workers.Single(w=>w.LastName=="Kask").Id,
                    Id="3001",
                    ReservedRoom = rooms.Single(r=>r.RoomName=="India Tuba"),
                    ReservedWorker = workers.Single(w=>w.LastName=="Kask")
                },

            };

            dataBase.Reservations.AddRange(reservations);
            dataBase.SaveChanges();
            
        }
    }
}