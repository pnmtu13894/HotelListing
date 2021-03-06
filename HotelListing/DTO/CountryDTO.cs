using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelListing.DataAccess;

namespace HotelListing.DTO
{
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        
        public IList<HotelDTO> Hotels { get; set; }
    }

    public class CreateCountryDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country name is too long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 3, ErrorMessage = "Short name is too long")]
        public string ShortName { get; set; }
    }

    public class UpdateCountryDTO : CreateCountryDTO
    {

    }
}