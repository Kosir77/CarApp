using System.Net;
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
        private readonly ICarBrandService _carBrandService;
        protected APIResponse _response;

        public CarsAPIController(ICarService carService, IMapper mapper, ICarBrandService carBrandService)
        {
            _carService = carService;
            _mapper = mapper;
            _carBrandService = carBrandService;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetCars() 
        {
            try
            {
                IEnumerable<Car> carList = await _carService.GetAllAsync();
                _response.Result = _mapper.Map<List<CarDTO>>(carList);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateCar([FromBody] CarCreateDTO createDTO)
        {
            try
            {
                //if (await _carService.GetAsync(u => u.Id == createDTO.Id) != null)
                //{
                //    ModelState.AddModelError("CustomError", "Car Already Exists!");
                //    return BadRequest(ModelState);
                //}
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                var brand = await _carBrandService.GetAsync(u => u.BrandName == createDTO.BrandName);
                if (brand == null)
                {
                    var brandModel = new CarBrand { BrandName = createDTO.BrandName };
                    await _carBrandService.CreateAsync(brandModel);
                    brand = brandModel;
                }
                var model = _mapper.Map<Car>(createDTO);
                model.BrandId = brand.BrandId;
                await _carService.CreateAsync(model);
                _response.Result = _mapper.Map<CarDTO>(model);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCar", new { id = model.Id }, _response);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}", Name = "DeleteCar")]
        public async Task<ActionResult<APIResponse>> DeleteCar(int id)
        {
            try
            {
                if (id == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var car = await _carService.GetAsync(u => u.Id == id);
                if (car == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _carService.DeleteAsync(car);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id}", Name = "GetCar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetCar(int id)
        {
            try
            {
                if (id == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var car = await _carService.GetAsync(u => u.Id == id);
                if (car == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CarDTO>(car);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("bybrand/{brandId}", Name = "GetCarsByBrand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetCarsByBrand(int brandId)
        {
            try
            {
                if (brandId == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                
                //if (car == null)
                //{
                //    _response.StatusCode = HttpStatusCode.NotFound;
                //    return NotFound(_response);
                //}
                IEnumerable<Car> carList = await _carService.GetCarsByBrandAsync(brandId);
                _response.Result = _mapper.Map<List<CarDTO>>(carList);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
