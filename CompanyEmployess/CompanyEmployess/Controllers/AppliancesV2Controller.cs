using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyEmployess.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/appliances")]
    [ApiController]
    public class AppliancesV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public AppliancesV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAppliances([FromQuery] AppliancesParameters appliancesParameters)
        {
            var appliances = await
           _repository.Appliance.GetAllAppliances(appliancesParameters, trackChanges: false);
            return Ok(appliances);
        }
    }
}
