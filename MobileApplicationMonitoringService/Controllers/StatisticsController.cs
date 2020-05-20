using Microsoft.AspNetCore.Mvc;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Exceptions;
using MobileApplicationMonitoringService.Filters;
using MobileApplicationMonitoringService.Services;
using Serilog;
using System;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Controllers
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : Controller
    {
        private readonly IApplicationStatisticsService statisticService;
        private static readonly ILogger logger = Log.ForContext<StatisticsController>();

        public StatisticsController(IApplicationStatisticsService statisticService)
        {
            this.statisticService = statisticService;
        }

        [HttpPost]
        [ModelValidation]
        public async Task Post([FromBody] SaveApplicationStatisticsRequest createRequest)
        {
            await statisticService.SaveApplicationStatisticsAsync(createRequest);
            logger.Debug("A request to create data about {@ApplicationStatistics}", createRequest);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] Guid id)
        {
            var applicationStatistics = await statisticService.GetApplicationStatisticsByIdAsync(id);
            if (applicationStatistics == null)
            {
                logger.Error("Data to delete not found");
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound);
            }
            logger.Debug("A request to delete data about {@ApplicationStatistics}", applicationStatistics);
            await statisticService.DeleteApplicationStatisticsAsync(id);
        }
    }
}
