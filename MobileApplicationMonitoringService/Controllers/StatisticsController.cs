using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using MobileApplicationMonitoringService.Contracts;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Filters;
using MobileApplicationMonitoringService.Services;
using Serilog;
using System;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Controllers
{
    [ApiController]
    public class StatisticsController:Controller
    {
        private readonly IApplicationStatisticService statisticService;
        private static readonly ILogger logger = Log.ForContext<StatisticsController>();

        public StatisticsController(IApplicationStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        [ModelValidation]
        [HttpPost(ApiRoutes.Statistics.Create)]
        public async Task Post([FromBody] CreateApplicationDataRequest createRequest)
        {
            var created = await statisticService.SaveApplicationStatisticAsync(createRequest);
            logger.Debug("A request to create data about {@IdentificationData}", created);
        }

        //[ModelValidation]
        //[HttpPut(ApiRoutes.Statistics.Update)]
        //public async Task Put([FromRoute]Guid id, [FromBody] UpdateApplicationDataRequest updateRequest)
        //{
        //    var identificationData = await repository.GetByIdAsync(id);
        //    if (identificationData == null)
        //    {
        //        return NotFound();
        //    }
        //    identificationData = mapper.Map<ApplicationData>(updateRequest);
        //    var updated = await repository.UpdateAsync(identificationData);

        //    logger.Debug("A request to update data about {@IdentificationData}", updated);
        //    return Ok(updated);
        //}
    }
}
