using Microsoft.AspNetCore.Mvc;
using MobileApplicationMonitoringService.Contracts.Responses;
using MobileApplicationMonitoringService.Exceptions;
using MobileApplicationMonitoringService.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Controllers
{
    [ApiController]
    [Route("api/application")]
    public class ApplicationDataController : Controller
    {
        private static readonly ILogger logger = Log.ForContext<ApplicationDataController>();
        private readonly IApplicationStatisticsService statisticService;
        public ApplicationDataController(IApplicationStatisticsService statisticService)
        {
            this.statisticService = statisticService;
        }

        [HttpGet]
        public async Task<List<ApplicationStatisticsResponse>> Get()
        {
            logger.Debug("There was a request to receive all application data");
            return await statisticService.GetAllApplicationStatisticsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ApplicationStatisticsResponse> Get([FromRoute] Guid id)
        {
            var applicationStatistics = await statisticService.GetApplicationStatisticsByIdAsync(id);
            if (applicationStatistics == null)
            {
                logger.Error("Data not found");
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound);
            }
            logger.Debug("A request for data about {@ApplicationStatistics}", applicationStatistics);
            return applicationStatistics;
        }
        [HttpDelete("{id}")]
        public async Task DeleteEvents([FromRoute] Guid id)
        {
            var applicationStatistics = await statisticService.GetApplicationStatisticsByIdAsync(id);
            if (applicationStatistics == null)
            {
                logger.Error("Data to delete not found");
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound);
            }
            logger.Debug("A request to delete data about {@ApplicationStatistics}", applicationStatistics);
            await statisticService.DeleteEventsByApplicationId(id);
        }
    }
}
