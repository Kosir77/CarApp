using AutoMapper;
using CarApp.Models;
using CarApp.Models.Dto;
using CarApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarApp.Controllers
{
    [Route("api/CarsAPI")]

    [ApiController]
    public class CarsAPIController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public CarsAPIController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars() 
        {
            IEnumerable<Car> carList = await _carService.GetAllAsync();
            return Ok(_mapper.Map<List<CarDTO>>(carList));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CarDTO>> CreateCar([FromBody] CarCreateDTO createDTO)
        {
            if (await _carService.GetAsync(u => u.Registration == createDTO.Registration) != null)
            {
                ModelState.AddModelError("CustomError", "Car Already Exists!");
            }
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
            Car model = _mapper.Map<Car>(createDTO);
            await _carService.CreateAsync(model);
            return CreatedAtRoute("GetCar", new { registration = model.Registration }, _mapper.Map<CarDTO>(model));
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{registration}", Name = "DeleteCar")]
        public async Task<IActionResult> DeleteCar(string registration)
        {
            if (registration == null)
            {
                return BadRequest();
            }
            var car = await _carService.GetAsync(u => u.Registration == registration);
            if (car == null)
            {
                return NotFound();
            }
            await _carService.DeleteAsync(car);
            return NoContent();
        }

        [HttpGet("{registration}", Name = "GetCar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CarDTO>> GetCar(string registration)
        {
            if (registration == null)
            {
                return BadRequest();
            }
            var car = await _carService.GetAsync(u => u.Registration == registration);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CarDTO>(car));
        }
    }
}
