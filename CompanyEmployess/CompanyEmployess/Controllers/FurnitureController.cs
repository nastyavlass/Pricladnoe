using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
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
    }
}
