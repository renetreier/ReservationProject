using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;

namespace ReservationProject.Infra
{
    public class ApplicationDbContext :IdentityDbContext {
        public ApplicationDbContext() : this(
            new DbContextOptionsBuilder<ApplicationDbContext>().Options)
        { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        public DbSet<WorkerData> Workers { get; set; }
        public DbSet<ReservationData> Reservations { get; set; }
        public DbSet<RoomData> Rooms { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<RoomData>().ToTable("Room");
            modelBuilder.Entity<WorkerData>().ToTable("Worker");
            modelBuilder.Entity<ReservationData>().ToTable("Reservation");

           

        }
    }
}

