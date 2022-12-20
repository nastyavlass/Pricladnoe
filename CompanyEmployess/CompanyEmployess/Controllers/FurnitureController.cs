using AutoMapper;
using CompanyEmployess.ActionFilters;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployess.Controllers
{
    [Route("api/furniture")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class FurnitureController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<FurnitureDto> _dataShaper;
        public FurnitureController(IRepositoryManager repository, ILoggerManager logger,
         IMapper mapper, IDataShaper<FurnitureDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        /// <summary>
        /// Получает список всей мебели
        /// </summary>
        /// <returns> Список мебели</returns>.
        [HttpGet(Name = "GetFurnitures"), Authorize]
        [HttpHead]
        public async Task<IActionResult> GetFurniture([FromQuery] FurnitureParameters furnitureParameters) {
            var furniture = await _repository.Furniture.GetAllFurniture(furnitureParameters, trackChanges: false);
            var furnitureDto = _mapper.Map<IEnumerable<FurnitureDto>>(furniture);
            return Ok(furnitureDto);
        }

        [HttpGet("{id}", Name = "FurnitureById")]
        [HttpHead]
        public async Task<IActionResult> GetFurniture(Guid id) {
            var furniture = await _repository.Furniture.GetFurniture(id, trackChanges: false);
            if (furniture == null) {
                _logger.LogInfo($"Furniture with id: {id} doesn't exist in the database.");
                return NotFound();
            } else {
                var furnitureDto = _mapper.Map<FurnitureDto>(furniture);
                return Ok(furnitureDto);
            }
        }

        [HttpGet("collection/({ids})", Name = "FurnitureCollection")]
        [HttpHead]
        public async Task<IActionResult> GetFurnitureCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            IEnumerable<Furniture> furnitureEntities = await _repository.Furniture.GetByIds(ids, trackChanges: false);

            if (ids.Count() != furnitureEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var furnitureToReturn = _mapper.Map<IEnumerable<ApplianceDto>>(furnitureEntities);
            return Ok(furnitureToReturn);
        }

        /// <summary>
        /// Создает вновь созданную мебель
        /// </summary>
        /// <param name="furniture"></param>.
        /// <returns>Вновь созданная мебель</returns>.
        /// <response code="201"> Возвращает только что созданный элемент</response>.
        /// <response code="400"> Если элемент равен null</response>.
        /// <код ответа="422"> Если модель недействительна</ответ>.
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFurniture([FromBody] FurnitureForCreationDto furniture)
        {

            var furnitureEntity = _mapper.Map<Furniture>(furniture);
            _repository.Furniture.CreateFurniture(furnitureEntity);
            _repository.SaveAsync();
            var furnitureToReturn = _mapper.Map<FurnitureDto>(furnitureEntity);
            return CreatedAtRoute("FurnitureById", new { id = furnitureToReturn.Id }, furnitureToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateFurnitureExistsAttribute))]
        public async Task<IActionResult> DeleteFurniture(Guid furnitureId)
        {
            var furniture = HttpContext.Items["furniture"] as Furniture;

            _repository.Furniture.DeleteFurniture(furniture);
            _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFurnitureExistsAttribute))]
        public async Task<IActionResult> UpdateFurniture(Guid furnitureId, [FromBody] FurnitureForUpdateDto furniture)
        {
            var furnitureEntity = HttpContext.Items["furniture"] as Furniture;
            _mapper.Map(furniture, furnitureEntity);
            _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateFurniture(Guid furnitureId, [FromBody] JsonPatchDocument<FurnitureForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var furnitureEntity = await _repository.Furniture.GetFurniture(furnitureId, trackChanges: true);
            if (furnitureEntity == null)
            {
                _logger.LogInfo($"Furniture with id: {furnitureId} doesn't exist in the database.");
                return NotFound();
            }

            var furnitureToPatch = _mapper.Map<FurnitureForUpdateDto>(furnitureEntity);
            patchDoc.ApplyTo(furnitureToPatch);
            _mapper.Map(furnitureToPatch, furnitureEntity);
            _repository.SaveAsync();

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetFurnitureOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
    }
}
