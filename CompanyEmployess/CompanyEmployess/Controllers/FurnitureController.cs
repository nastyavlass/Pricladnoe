using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyEmployess.Controllers
{
    [Route("api/furniture")]
    [ApiController]
    public class FurnitureController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public FurnitureController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAppliances() {
            var furniture = _repository.Furniture.GetAllFurniture(trackChanges: false);
            var furnitureDto = _mapper.Map<IEnumerable<FurnitureDto>>(furniture);
            return Ok(furnitureDto);
        }

        [HttpGet("{id}", Name = "FurnitureById")]
        public IActionResult GetFurniture(Guid id) {
            var furniture = _repository.Furniture.GetFurniture(id, trackChanges: false);
            if (furniture == null) {
                _logger.LogInfo($"Furniture with id: {id} doesn't exist in the database.");
                return NotFound();
            } else {
                var furnitureDto = _mapper.Map<FurnitureDto>(furniture);
                return Ok(furnitureDto);
            }
        }

        [HttpGet("collection/({ids})", Name = "FurnitureCollection")]
        public IActionResult GetFurnitureCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            IEnumerable<Furniture> furnitureEntities = _repository.Furniture.GetByIds(ids, trackChanges: false);

            if (ids.Count() != furnitureEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var furnitureToReturn = _mapper.Map<IEnumerable<ApplianceDto>>(furnitureEntities);
            return Ok(furnitureToReturn);
        }

        [HttpPost]
        public IActionResult CreateFurniture([FromBody] FurnitureForCreationDto furniture)
        {
            if (furniture == null)
            {
                _logger.LogError("FurnitureForCreationDto object sent from client is null.");
                return BadRequest("FurnitureForCreationDto object is null");
            }

            var furnitureEntity = _mapper.Map<Furniture>(furniture);
            _repository.Furniture.CreateFurniture(furnitureEntity);
            _repository.Save();
            var furnitureToReturn = _mapper.Map<FurnitureDto>(furnitureEntity);
            return CreatedAtRoute("FurnitureById", new { id = furnitureToReturn.Id }, furnitureToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFurniture(Guid furnitureId)
        {
            var furniture = _repository.Furniture.GetFurniture(furnitureId, trackChanges: false);
            if (furniture == null)
            {
                _logger.LogInfo($"Furniture with id: {furnitureId} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Furniture.DeleteFurniture(furniture);
            _repository.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFurniture(Guid furnitureId, [FromBody] FurnitureForUpdateDto furniture)
        {
            if (furniture == null)
            {
                _logger.LogError("FurnitureForUpdateDto object sent from client is null.");
                return BadRequest("FurnitureForUpdateDto object is null");
            }

            var furnitureEntity = _repository.Furniture.GetFurniture(furnitureId, trackChanges: false);
            if (furnitureEntity == null)
            {
                _logger.LogInfo($"Furniture with id: {furnitureId} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(furniture, furnitureEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateFurniture(Guid furnitureId, [FromBody] JsonPatchDocument<FurnitureForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var furnitureEntity = _repository.Furniture.GetFurniture(furnitureId, trackChanges: true);
            if (furnitureEntity == null)
            {
                _logger.LogInfo($"Furniture with id: {furnitureId} doesn't exist in the database.");
                return NotFound();
            }

            var furnitureToPatch = _mapper.Map<FurnitureForUpdateDto>(furnitureEntity);
            patchDoc.ApplyTo(furnitureToPatch);
            _mapper.Map(furnitureToPatch, furnitureEntity);
            _repository.Save();

            return NoContent();
        }
    }
}
