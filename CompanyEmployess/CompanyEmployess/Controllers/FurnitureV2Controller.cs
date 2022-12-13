using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyEmployess.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/furniture")]
    [ApiController]
    public class FurnitureV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public FurnitureV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFurniture([FromQuery] FurnitureParameters furnitureParameters)
        {
            var furniture = await
           _repository.Furniture.GetAllFurniture(furnitureParameters, trackChanges: false);
            return Ok(furniture);
        }
    }
}
