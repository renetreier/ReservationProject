using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Data;

namespace Infra
{
    public class ApplicationDbContext :IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<ReservationTime> ReservationTimes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<Room>().ToTable("Room");
            modelBuilder.Entity<Worker>().ToTable("Worker");
            modelBuilder.Entity<ReservationTime>().ToTable("ReservationTime");
        }
    }
}

