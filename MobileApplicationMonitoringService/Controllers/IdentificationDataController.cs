using System;
using System.Collections.Generic;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Repositories;
using MobileApplicationMonitoringService.Contracts;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Contracts.Responses;
using Serilog;

namespace MobileApplicationMonitoringService.Controllers
{
    [ApiController]
    public class IdentificationDataController:Controller
    {
        private readonly ILogger<IdentificationDataController> logger;
        private readonly IIdentificationDataRepository repository;
        private readonly IMapper mapper;

        public IdentificationDataController(ILogger<IdentificationDataController> logger, IIdentificationDataRepository repository,IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet(ApiRoutes.IdentificationData.GetAll)]
        public IEnumerable<IdentificationData> Get()
        {
            logger.LogDebug("There was a request to receive all data");
            return repository.GetAll().Values;
        }

        [HttpGet(ApiRoutes.IdentificationData.Get)]
        public IActionResult Get([FromRoute] Guid id)
        {
            var identificationData = repository.GetById(id);
            if (identificationData == null)
            {
                return NotFound();
            }
            logger.LogDebug ("A request for data about {@IdentificationData}",identificationData);
            return Ok(identificationData);
        }

        [HttpPost(ApiRoutes.IdentificationData.Create)]
        public IActionResult Post([FromBody] CreateIdentificationDataRequest createRequest)
        {
            if (createRequest == null)
            {
                return BadRequest("Data object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var identificationData = mapper.Map<IdentificationData>(createRequest);
            var created = repository.Create(identificationData);
            logger.LogDebug("A request to create data about {@IdentificationData}",created);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.IdentificationData.Get.Replace("{id:Guid}", created.Id.ToString());
            var response = mapper.Map<IdentificationDataResponse>(created);
            return Created(locationUri, response);

        }
        [HttpPut(ApiRoutes.IdentificationData.Update)]
        public IActionResult Put([FromRoute]Guid id, [FromBody] UpdateIdentificationDataRequest updateRequest)
        {
            if (updateRequest == null)
            {
                return BadRequest("Data object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var identificationData = repository.GetById(id);
            if (identificationData == null)
            {
                return NotFound();
            }
            identificationData = mapper.Map<IdentificationData>(updateRequest);
            var updated = repository.Update(identificationData);
            
            logger.LogDebug("A request to update data about {@IdentificationData}",updated);
            return Ok(updated);
        }

        [HttpDelete(ApiRoutes.IdentificationData.Delete)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var identificationData = repository.GetById(id);
            if (identificationData == null)
            {
                return NotFound();
            }
            logger.LogDebug("A request to delete data about {@IdentificationData}",identificationData);
            repository.Delete(id);
            return Ok();
        }
    }
}
