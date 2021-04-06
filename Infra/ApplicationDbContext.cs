﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;

namespace ReservationProject.Infra
{
    public class ApplicationDbContext :IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        
        
        //TODO VAJA DBINITIALIZER KA TEHA
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<Room>().ToTable("Room");
            modelBuilder.Entity<Worker>().ToTable("Worker");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            //Todo Kas teha võtmeks kolmene komplekt worker/room/date?
            modelBuilder.Entity<Reservation>().HasKey(r => new { r.WorkerId, r.RoomId });
        }
    }
}
