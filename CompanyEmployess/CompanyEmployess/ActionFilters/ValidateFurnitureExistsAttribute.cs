using Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace CompanyEmployess.ActionFilters
{
    public class ValidateFurnitureExistsAttribute
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateFurnitureExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["id"];
            var furniture = await _repository.Furniture.GetFurniture(id, trackChanges);
            if (furniture == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");

                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("appliance", furniture);
                await next();
            }
        }
    }
}
