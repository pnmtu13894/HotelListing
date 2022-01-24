using HotelListing.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {

            builder.HasData(
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
        }
    }
}
