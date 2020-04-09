using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Repositories;
using MobileApplicationMonitoringService.Contracts;
using MobileApplicationMonitoringService.Exceptions;
using MobileApplicationMonitoringService.Services;
using Serilog;

namespace MobileApplicationMonitoringService.Controllers
{
    [ApiController]
    public class ApplicationDataController:Controller
    {
        private static readonly ILogger logger = Log.ForContext<ApplicationDataController>();
        private readonly IApplicationDataRepository repository;
        private readonly IApplicationStatisticService statisticService;
        public ApplicationDataController(IApplicationDataRepository repository, IApplicationStatisticService statisticService)
        {
            this.repository = repository;
            this.statisticService = statisticService;
        }

        [HttpGet(ApiRoutes.ApplicationData.GetAll)]
        public async Task<IEnumerable<ApplicationData>> Get()
        {
            logger.Debug("There was a request to receive all application data");
            return await repository.GetAllAsync();
        }

        [HttpGet(ApiRoutes.ApplicationData.Get)]
        public async Task<ApplicationData> Get([FromRoute] Guid id)
        {
            var applicationData = await repository.GetByIdAsync(id);
            if (applicationData == null)
            {
                logger.Error("Data not found");
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound);
            }
            logger.Debug("A request for data about {@IdentificationData}",applicationData);
            return applicationData;
        }

        [HttpDelete(ApiRoutes.ApplicationData.Delete)]
        public async Task Delete([FromRoute] Guid id)
        {
            var applicationData = await repository.GetByIdAsync(id);
            if (applicationData == null)
            {
                logger.Error("Data to delete not found");
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound);
            }
            logger.Debug("A request to delete data about {@IdentificationData}",applicationData);
            await statisticService.DeleteApplicationStatisticsAsync(id);
        }
    }
}
