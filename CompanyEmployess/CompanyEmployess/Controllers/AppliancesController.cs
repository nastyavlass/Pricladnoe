using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

        [HttpGet("{id}")]
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
    }
}
