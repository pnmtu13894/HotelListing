using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelListing.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country()
            {
                Id = 1,
                Name = "Vietnam",
                ShortName = "VN"
            },
                new Country()
                {
                    Id = 2,
                    Name = "United States",
                    ShortName = "US"
                },
                new Country()
                {
                    Id = 3,
                    Name = "Australia",
                    ShortName = "AUS"
                });

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel()
                {
                    Id = 1,
                    Name = "Pullman",
                    Address = "Vung Tau",
                    CountryId = 1,
                    Rating = 4
                },
                new Hotel()
                {
                    Id = 2,
                    Name = "Sheraton",
                    Address = "Ho Chi Minh",
                    CountryId = 1,
                    Rating = 4.3
                },
                new Hotel()
                {
                    Id = 3,
                    Name = "Park Hyatt Sydney",
                    Address = "Sydney",
                    CountryId = 3,
                    Rating = 4.1
                },
                new Hotel()
                {
                    Id = 4,
                    Name = "Luxor Hotel & Casino",
                    Address = "Las Vegas",
                    CountryId = 2,
                    Rating = 4.2
                }
            );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Country> Countries { get; set; }
        
        public DbSet<Hotel> Hotels { get; set; }
    }
}