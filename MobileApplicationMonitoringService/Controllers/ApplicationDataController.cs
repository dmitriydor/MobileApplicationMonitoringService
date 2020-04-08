using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Repositories;
using MobileApplicationMonitoringService.Contracts;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Contracts.Responses;
using MobileApplicationMonitoringService.Exceptions;
using MobileApplicationMonitoringService.Filters;
using MobileApplicationMonitoringService.Services;
using Serilog;

namespace MobileApplicationMonitoringService.Controllers
{
    [ApiController]
    public class ApplicationDataController:Controller
    {
        private static readonly ILogger logger = Log.ForContext<ApplicationDataController>();
        private readonly IApplicationDataRepository repository;
        private readonly IMapper mapper;
        private readonly IApplicationStatisticService eventService;

        public ApplicationDataController(IApplicationDataRepository repository,IMapper mapper, IApplicationStatisticService eventService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.eventService = eventService;
        }

        [HttpGet(ApiRoutes.ApplicationData.GetAll)]
        public async Task<IEnumerable<ApplicationData>> Get()
        {
            logger.Debug("There was a request to receive all data");
            return await repository.GetAllAsync();
        }

        [HttpGet(ApiRoutes.ApplicationData.Get)]
        public async Task<ApplicationData> Get([FromRoute] Guid id)
        {
            var identificationData = await repository.GetByIdAsync(id);
            if (identificationData == null)
            {
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound);
            }
            logger.Debug("A request for data about {@IdentificationData}",identificationData);
            return identificationData;
        }
        [ModelValidation]
        [HttpPost(ApiRoutes.ApplicationData.Create)]
        public async Task<IActionResult> Post([FromBody] CreateApplicationDataRequest createRequest)
        {
            var created = await eventService.SaveApplicationStatistic(createRequest);

            logger.Debug("A request to create data about {@IdentificationData}",created);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.ApplicationData.Get.Replace("{id:Guid}", created.Id.ToString());
            var response = mapper.Map<ApplicationDataResponse>(created);
            
            return Created(locationUri, response);
        }
        [ModelValidation]
        [HttpPut(ApiRoutes.ApplicationData.Update)]
        public async Task<IActionResult> Put([FromRoute]Guid id, [FromBody] UpdateApplicationDataRequest updateRequest)
        {
            var identificationData = await repository.GetByIdAsync(id);
            if (identificationData == null)
            {
                return NotFound();
            }
            identificationData = mapper.Map<ApplicationData>(updateRequest);
            var updated = await repository.UpdateAsync(identificationData);
            
            logger.Debug("A request to update data about {@IdentificationData}",updated);
            return Ok(updated);
        }

        [HttpDelete(ApiRoutes.ApplicationData.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var identificationData = repository.GetByIdAsync(id);
            if (identificationData == null)
            {
                return NotFound();
            }
            logger.Debug("A request to delete data about {@IdentificationData}",identificationData);
            await repository.DeleteAsync(id);
            return Ok();
        }
    }
}
