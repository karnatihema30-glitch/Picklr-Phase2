using Microsoft.EntityFrameworkCore;

namespace Picklr.Models
{
    public class PicklrContext : DbContext
    {
        public PicklrContext(DbContextOptions<PicklrContext> options)
            : base(options)
        {
        }

        public DbSet<Club> Clubs { get; set; } = null!;
        public DbSet<PicklProgram> Programs { get; set; } = null!;
        public DbSet<AppUser> Users { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------------------------
            // Seed Clubs
            // -------------------------
            modelBuilder.Entity<Club>().HasData(

                new Club
                {
                    ClubID = 1,
                    Name = "Picklr Downtown",
                    Location = "123 Main St, Chicago, IL",
                    Description = "Our flagship downtown club with 10 indoor courts."
                },

                new Club
                {
                    ClubID = 2,
                    Name = "Picklr Northside",
                    Location = "456 Oak Ave, Evanston, IL",
                    Description = "A vibrant outdoor facility with 8 courts and a pro shop."
                },

                new Club
                {
                    ClubID = 3,
                    Name = "Picklr New York",
                    Location = "789 Madison Ave, New York, NY",
                    Description = "Modern indoor pickleball club located in New York."
                }

            );

            // -------------------------
            // Seed Programs
            // -------------------------
            modelBuilder.Entity<PicklProgram>().HasData(

                new PicklProgram
                {
                    ProgramID = 1,
                    Name = "Beginner Open Play",
                    Description = "Drop-in open play for new players. No experience needed.",
                    Fee = 10.00m,
                    ClubID = 1,
                    AvailableDays = "Monday, Wednesday, Friday"
                },

                new PicklProgram
                {
                    ProgramID = 2,
                    Name = "Intermediate Clinic",
                    Description = "Weekly skill-building clinic led by a certified coach.",
                    Fee = 25.00m,
                    ClubID = 1,
                    AvailableDays = "Tuesday, Thursday"
                },

                new PicklProgram
                {
                    ProgramID = 3,
                    Name = "Advanced Tournament",
                    Description = "Competitive round-robin tournament for rated players.",
                    Fee = 40.00m,
                    ClubID = 2,
                    AvailableDays = "Saturday, Sunday"
                },

                new PicklProgram
                {
                    ProgramID = 4,
                    Name = "Picklr 101",
                    Description = "The program is designed for beginners.",
                    Fee = 10.00m,
                    ClubID = 3,
                    AvailableDays = "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday"
                },

                new PicklProgram
                {
                    ProgramID = 5,
                    Name = "Picklr Social",
                    Description = "Weekend social play for all skill levels.",
                    Fee = 0.00m,
                    ClubID = 2,
                    AvailableDays = "Saturday"
                }

            );

            // -------------------------
            // Seed Users
            // -------------------------
            modelBuilder.Entity<AppUser>().HasData(

                new AppUser
                {
                    UserID = 1,
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "alice@picklr.com",
                    Role = "Admin"
                },

                new AppUser
                {
                    UserID = 2,
                    FirstName = "Bob",
                    LastName = "Jones",
                    Email = "bob@picklr.com",
                    Role = "Client"
                }

            );
        }
    }
}