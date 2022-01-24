using HotelListing.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Configurations.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {

            builder.HasData(
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
        }
    }
}
