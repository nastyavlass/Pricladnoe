﻿using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyEmployess.Controllers
{
    [Route("api/furniture")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class FurnitureV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public FurnitureV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получает список всей мебели
        /// </summary>
        /// <returns> Список мебели</returns>.
        [HttpGet]
        public async Task<IActionResult> GetFurniture([FromQuery] FurnitureParameters furnitureParameters)
        {
            var furniture = await
           _repository.Furniture.GetAllFurniture(furnitureParameters, trackChanges: false);
            return Ok(furniture);
        }
    }
}
