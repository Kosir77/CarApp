using System.Net;
using AutoMapper;
using CarApp.Models;
using CarApp.Models.Dto;
using CarApp.Services;
using CarApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBrandAPIController : ControllerBase
    {
        private readonly ICarBrandService _carBrandService;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public CarBrandAPIController(ICarBrandService carBrandService, IMapper mapper)
        {
            _carBrandService = carBrandService;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetBrands()
        {
            try
            {
                IEnumerable<CarBrand> brandsList = await _carBrandService.GetAllAsync();
                _response.Result = _mapper.Map<List<CarBrandDTO>>(brandsList);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateBrand([FromBody] CarBrandCreateDTO createDTO)
        {
            try
            {
                if (await _carBrandService.GetAsync(u => u.BrandName == createDTO.BrandName) != null)
                {
                    ModelState.AddModelError("CustomError", "Car Brand Already Exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                var model = _mapper.Map<CarBrand>(createDTO);
                model.BrandName = model.BrandName.ToUpper();
                await _carBrandService.CreateAsync(model);
                _response.Result = _mapper.Map<CarBrandDTO>(model);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetBrand", new { id = model.BrandId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}", Name = "DeleteBrand")]
        public async Task<ActionResult<APIResponse>> DeleteBrand(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var brand = await _carBrandService.GetAsync(u => u.BrandId == id);
                if (brand == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _carBrandService.DeleteAsync(brand);
                _response.StatusCode = HttpStatusCode.NoContent;
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
        [HttpGet("{id}", Name = "GetBrand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetBrand(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var brand = await _carBrandService.GetAsync(u => u.BrandId == id);
                if (brand == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CarBrandDTO>(brand);
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
