using Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace CompanyEmployess.ActionFilters
{
    public class ValidateAppliancesExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateAppliancesExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["id"];
            var appliance = await _repository.Appliance.GetAppliance(id, trackChanges);
            if (appliance == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
               
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("appliance", appliance);
                await next();
            }
        }
    }
}
