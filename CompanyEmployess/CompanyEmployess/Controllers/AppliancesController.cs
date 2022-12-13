using AutoMapper;
using CompanyEmployess.ActionFilters;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployess.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/appliances")]
    [ApiController]
    public class AppliancesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<ApplianceDto> _dataShaper;
        public AppliancesController(IRepositoryManager repository, ILoggerManager logger,
         IMapper mapper, IDataShaper<ApplianceDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetAppliances([FromQuery] AppliancesParameters appliancesParameters) {
            var appliances = await _repository.Appliance.GetAllAppliances(appliancesParameters, trackChanges: false);
            var appliancesDto = _mapper.Map<IEnumerable<ApplianceDto>>(appliances);
            return Ok(appliancesDto);
        }

        [HttpGet("{id}", Name = "ApplianceById")]
        [HttpHead]
        public async Task<IActionResult> GetAppliance(Guid id) {
            var appliance = await _repository.Appliance.GetAppliance(id, trackChanges: false);
            if (appliance == null) {
                _logger.LogInfo($"Appliance with id: {id} doesn't exist in the database.");
                return NotFound();
            } else {
                var applianceDto = _mapper.Map<ApplianceDto>(appliance);
                return Ok(applianceDto);
            }
        }

        [HttpGet("collection/({ids})", Name = "ApplianceCollection")]
        [HttpHead]
        public async Task<IActionResult> GetApplianceCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            IEnumerable<Appliance> applianceEntities = await _repository.Appliance.GetByIds(ids, trackChanges: false);

            if (ids.Count() != applianceEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var applianceToReturn = _mapper.Map<IEnumerable<ApplianceDto>>(applianceEntities);
            return Ok(applianceToReturn);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAppliance([FromBody] ApplianceForCreationDto appliance)
        {
            var applianceEntity = _mapper.Map<Appliance>(appliance);
            _repository.Appliance.CreateAppliance(applianceEntity);
            _repository.SaveAsync();
            var applianceToReturn = _mapper.Map<ApplianceDto>(applianceEntity);
            return CreatedAtRoute("ApplianceById", new { id = applianceToReturn.Id }, applianceToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateAppliancesExistsAttribute))]
        public async Task<IActionResult> DeleteAppliance(Guid applianceId)
        {

            var appliance = HttpContext.Items["appliance"] as Appliance;
            _repository.Appliance.DeleteAppliance(appliance);
            _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateAppliancesExistsAttribute))]
        public async Task<IActionResult> UpdateAppliance(Guid applianceId, [FromBody] ApplianceForUpdateDto appliance)
        {
            var applianceEntity = HttpContext.Items["appliance"] as Appliance;
            _mapper.Map(appliance, applianceEntity);
            _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateAppliance(Guid applianceId, [FromBody] JsonPatchDocument<ApplianceForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var applianceEntity = _repository.Appliance.GetAppliance(applianceId, trackChanges: true);
            if (applianceEntity == null)
            {
                _logger.LogInfo($"Appliance with id: {applianceId} doesn't exist in the database.");
                return NotFound();
            }

            var applianceToPatch = _mapper.Map<ApplianceForUpdateDto>(applianceEntity);
            patchDoc.ApplyTo(applianceToPatch);
            _mapper.Map(applianceToPatch, applianceEntity);
            _repository.SaveAsync();

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetApplianceOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

    }
}
