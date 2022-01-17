using AutoMapper;
using HotelListing.DTO;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;

        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllHotels()
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll();
                var results = _mapper.Map<IList<HotelDTO>>(hotels);


                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetAllHotels)}");
                return StatusCode(500, "Internal Server Error. Please contact to administration team.");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotelById(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(x => x.Id == id);
                var result = _mapper.Map<HotelDTO>(hotel);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetHotelById)}");
                return StatusCode(500, "Internal Server Error. Please contact to administration team.");
            }
        }
    }
}
