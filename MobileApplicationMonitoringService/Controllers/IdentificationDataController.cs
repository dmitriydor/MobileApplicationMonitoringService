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
using MobileApplicationMonitoringService.Sevices;
using Serilog;

namespace MobileApplicationMonitoringService.Controllers
{
    [ApiController]
    public class IdentificationDataController:Controller
    {
        private static readonly ILogger logger = Log.ForContext<IdentificationDataController>();
        private readonly IIdentificationRepository repository;
        private readonly IMapper mapper;
        private readonly IEventService eventService;

        public IdentificationDataController(IIdentificationRepository repository,IMapper mapper, IEventService eventService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.eventService = eventService;
        }

        [HttpGet(ApiRoutes.IdentificationData.GetAll)]
        public async Task<IEnumerable<IdentificationData>> Get()
        {
            logger.Debug("There was a request to receive all data");
            return await repository.GetAllAsync();
        }

        [HttpGet(ApiRoutes.IdentificationData.Get)]
        public IActionResult Get([FromRoute] Guid id)
        {
            var identificationData = repository.GetByIdAsync(id);
            if (identificationData == null)
            {
                return NotFound();
            }
            logger.Debug("A request for data about {@IdentificationData}",identificationData);
            return Ok(identificationData);
        }

        [HttpPost(ApiRoutes.IdentificationData.Create)]
        public async Task<IActionResult> Post([FromBody] CreateIdentificationDataRequest createRequest)
        {
            //TODO: вынести валидацию в фильтр
            if (createRequest == null)
            {
                return BadRequest("Data object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var created = await eventService.CreateEventAndIdentificationData(createRequest);

            logger.Debug("A request to create data about {@IdentificationData}",created);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.IdentificationData.Get.Replace("{id:Guid}", created.Id.ToString());
            var response = mapper.Map<IdentificationDataResponse>(created);
            
            return Created(locationUri, response);
        }
        [HttpPut(ApiRoutes.IdentificationData.Update)]
        public async Task<IActionResult> Put([FromRoute]Guid id, [FromBody] UpdateIdentificationDataRequest updateRequest)
        {
            //TODO: вынести валидацию в фильтр
            if (updateRequest == null)
            {
                return BadRequest("Data object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var identificationData = await repository.GetByIdAsync(id);
            if (identificationData == null)
            {
                return NotFound();
            }
            identificationData = mapper.Map<IdentificationData>(updateRequest);
            var updated = await repository.UpdateAsync(identificationData);
            
            logger.Debug("A request to update data about {@IdentificationData}",updated);
            return Ok(updated);
        }

        [HttpDelete(ApiRoutes.IdentificationData.Delete)]
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
