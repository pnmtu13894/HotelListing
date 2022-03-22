using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.DataAccess;
using HotelListing.DTO;
using HotelListing.DTO.Paging;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HotelListing.Controllers
{
    [Route("[controller]")]
    public class CountryController : BaseApiController
    {
        public CountryController(IUnitOfWork unitOfWork, ILogger<BaseApiController> logger, IMapper mapper) : base(unitOfWork, logger, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCountries([FromQuery] RequestParam requestParams)
        {
            var countries = await _unitOfWork.Countries.GetPagedList(requestParams);
            var results = _mapper.Map<IList<CountryDTO>>(countries);

            return Ok(results);
        }

        [Authorize]
        [HttpGet("{id:int}", Name = "GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var country = await _unitOfWork.Countries.Get(x => x.Id == id, new List<string> { "Hotels" });
            var result = _mapper.Map<CountryDTO>(country);

            return Ok(result);
        }

        [Authorize (Roles = "Administration")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid post attempt in {nameof(CreateCountry)}");
                return BadRequest();
            }
            
            var country = _mapper.Map<Country>(countryDTO);
            await _unitOfWork.Countries.Insert(country);
            await _unitOfWork.SaveChanges();

            return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
        {

            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCountry)}");
                return BadRequest();
            }
            var country = await _unitOfWork.Countries.Get(x => x.Id == id);
            if(country == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCountry)}");
                return BadRequest("Country does not exists in the system");
            }

            _mapper.Map(countryDTO, country);

            _unitOfWork.Countries.Update(country);
            await _unitOfWork.SaveChanges();

            return Ok($"Country {country.Name} updated successfully!!!");
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {

            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCountry)}");
                return BadRequest();
            }
            var country = await _unitOfWork.Countries.Get(x => x.Id == id, new List<string> { "Hotels" });
            if (country == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCountry)}");
                return BadRequest("Country does not exists in the system");
            }

            if(country.Hotels != null &&
                country.Hotels.Count > 0)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCountry)}");
                return BadRequest("Country has hotels associated with it. Cannot delete.");
            }

            await _unitOfWork.Countries.Delete(id);
            await _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}