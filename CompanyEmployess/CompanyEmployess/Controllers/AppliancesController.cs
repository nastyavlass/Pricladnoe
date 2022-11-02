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
    [Route("api/appliances")]
    [ApiController]
    public class AppliancesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AppliancesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAppliances() {
            var appliances = _repository.Appliance.GetAllAppliances(trackChanges: false);
            var appliancesDto = _mapper.Map<IEnumerable<ApplianceDto>>(appliances);
            return Ok(appliancesDto);
        }

        [HttpGet("{id}", Name = "ApplianceById")]
        public IActionResult GetAppliance(Guid id) {
            var appliance = _repository.Appliance.GetAppliance(id, trackChanges: false);
            if (appliance == null) {
                _logger.LogInfo($"Appliance with id: {id} doesn't exist in the database.");
                return NotFound();
            } else {
                var applianceDto = _mapper.Map<ApplianceDto>(appliance);
                return Ok(applianceDto);
            }
        }

        [HttpGet("collection/({ids})", Name = "ApplianceCollection")]
        public IActionResult GetApplianceCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            IEnumerable<Appliance> applianceEntities = _repository.Appliance.GetByIds(ids, trackChanges: false);

            if (ids.Count() != applianceEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var applianceToReturn = _mapper.Map<IEnumerable<ApplianceDto>>(applianceEntities);
            return Ok(applianceToReturn);
        }

        [HttpPost]
        public IActionResult CreateAppliance([FromBody] ApplianceForCreationDto appliance)
        {
            if (appliance == null)
            {
                _logger.LogError("ApplianceForCreationDto object sent from client is null.");
                return BadRequest("ApplianceForCreationDto object is null");
            }

            var applianceEntity = _mapper.Map<Appliance>(appliance);
            _repository.Appliance.CreateAppliance(applianceEntity);
            _repository.Save();
            var applianceToReturn = _mapper.Map<ApplianceDto>(applianceEntity);
            return CreatedAtRoute("ApplianceById", new { id = applianceToReturn.Id }, applianceToReturn);
        }
    }
}
