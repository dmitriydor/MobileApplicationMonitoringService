using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger logger;
        private readonly IIdentificationDataRepository repository;

        public IdentificationDataController(ILogger logger, IIdentificationDataRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet(ApiRoutes.IdentificationData.GetAll)]
        public IEnumerable<IdentificationData> Get()
        {
            logger.Debug("There was a request to receive all data");
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
            logger.Debug("A request for data about {@IdentificationData}",identificationData);
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

            var identificationData = new IdentificationData
            {
                UserName = createRequest.UserName,
                AppVersion = createRequest.AppVersion,
                OperationSystem = createRequest.OperationSystem
            };
            var created = repository.Create(identificationData);
            logger.Debug("A request to create data about {@IdentificationData}",created);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.IdentificationData.Get.Replace("{id:Guid}", created.Id.ToString());
            var response = new IdentificationDataResponse
            {
                Id = created.Id,
                AppVersion = created.AppVersion,
                OperationSystem = created.OperationSystem,
                UserName = created.UserName
            };


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
            identificationData.AppVersion = updateRequest.AppVersion;
            identificationData.OperationSystem = updateRequest.OperationSystem;
            identificationData.UserName = updateRequest.UserName;
            var updated = repository.Update(identificationData);
            
            logger.Debug("A request to update data about {@IdentificationData}",updated);
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
            logger.Debug("A request to delete data about {@IdentificationData}",identificationData);
            repository.Delete(id);
            return Ok();
        }
    }
}
