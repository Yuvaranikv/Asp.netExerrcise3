using LGym.Models;
using Microsoft.EntityFrameworkCore;

namespace LGym.DbContexts
{
    public class LGymContext : DbContext
    {
        public LGymContext(DbContextOptions<LGymContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Member)
                .WithMany(m => m.Sessions)
                .HasForeignKey(s => s.MemberId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Trainer)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.TrainerId);

            // Seed data
            modelBuilder.Entity<Member>().HasData(
                new Member { MemberId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", JoinDate = DateTime.Now },
                new Member { MemberId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", JoinDate = DateTime.Now }
            );

            modelBuilder.Entity<Trainer>().HasData(
                new Trainer { TrainerId = 1, FirstName = "Emily", LastName = "Jones", Speciality = "Yoga", FeePer30Minutes = 20.00m, HireDate = DateTime.Now },
                new Trainer { TrainerId = 2, FirstName = "Michael", LastName = "Brown", Speciality = "Weightlifting", FeePer30Minutes = 25.00m, HireDate = DateTime.Now }
            );

            modelBuilder.Entity<Session>().HasData(
                new Session { SessionId = 1, MemberId = 1, TrainerId = 1, SessionDate = DateTime.Now, Duration = 60 },
                new Session { SessionId = 2, MemberId = 2, TrainerId = 2, SessionDate = DateTime.Now, Duration = 45 }
            );
        }
    }
}
    

