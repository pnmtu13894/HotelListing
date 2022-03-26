using AutoMapper;
using HotelListing.DataAccess;
using HotelListing.DTO;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.DTO.ExceptionHandler;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Controllers
{
    [Route("[controller]")]
    public class HotelController : BaseApiController
    {
        public HotelController(IUnitOfWork unitOfWork, ILogger<BaseApiController> logger, IMapper mapper) 
            : base(unitOfWork, logger, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _unitOfWork.Hotels.GetAll();
            var results = _mapper.Map<IList<HotelDTO>>(hotels);


            return Ok(results);
        }

        [Authorize]
        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _unitOfWork.Hotels.Get(x => x.Id == id, h => h.Include(x => x.Country).ThenInclude(y => y.Hotels));
            var result = _mapper.Map<HotelDTO>(hotel);

            if (result == null)
            {
                throw new ValidationException($"Hotel id {id} is not found!!!");
            }
            
            return Ok(ApiResonse<HotelDTO>.Success(result));
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid post attempt in {nameof(CreateHotel)}");
                return BadRequest();
            }

            var hotel = _mapper.Map<Hotel>(hotelDTO);
            await _unitOfWork.Hotels.Insert(hotel);
            await _unitOfWork.SaveChanges();

            return CreatedAtRoute("GetHotel", new { id = hotel.Id }, hotel);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateHotel)}");
                return BadRequest();
            }

            var hotel = await _unitOfWork.Hotels.Get(x => x.Id == id);
            if (hotel == null)
            {
                throw new ValidationException($"Hotel ID {id} does not exist in the system.");
                // _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateHotel)}");
                // return BadRequest($"Hotel ID {id} does not exist in the system.");
            }

            _mapper.Map(hotelDTO, hotel);

            _unitOfWork.Hotels.Update(hotel);
            await _unitOfWork.SaveChanges();

            return Ok($"Hotel {hotel.Name} updated successfully!!!");
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHotel(int id)
        {

            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteHotel)}");
                return BadRequest();
            }

            var hotel = await _unitOfWork.Hotels.Get(x => x.Id == id);
            if (hotel == null)
            {
                throw new ValidationException($"Hotel ID {id} does not exist in the system.");
                // _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteHotel)}");
                // return BadRequest($"Hotel ID {id} does not exist in the system.");
            }

            await _unitOfWork.Hotels.Delete(id);
            await _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}
