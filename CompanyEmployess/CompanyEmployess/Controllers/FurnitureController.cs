﻿using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CompanyEmployess.Controllers
{
    [Route("api/furniture")]
    [ApiController]
    public class FurnitureController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public FurnitureController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAppliances()
        {
            var furniture = _repository.Furniture.GetAllFurniture(trackChanges: false);
            var furnitureDto = _mapper.Map<IEnumerable<FurnitureDto>>(furniture);
            return Ok(furnitureDto);
        }
    }
}
