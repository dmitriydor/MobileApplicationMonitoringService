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
    public class MainController:Controller
    {
        private readonly ILogger logger;
        private readonly IIdentificationDataRepository repository;

        public MainController(ILogger logger, IIdentificationDataRepository repository)
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
            logger.Debug($"A request for data about {identificationData?.ToString()}");
            return Ok(identificationData);
        }

        [HttpPost(ApiRoutes.IdentificationData.Create)]
        public IActionResult Post([FromBody] CreateIdentificationDataRequest requestData)
        {
            if (requestData == null)
            {
                return BadRequest("Data object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var identificationData = new IdentificationData
            {
                UserName = requestData.UserName,
                AppVersion = requestData.AppVersion,
                OperationSystem = requestData.OperationSystem
            };
            var createdData = repository.Create(identificationData);
            logger.Debug($"A request to create data about {createdData.ToString()}");

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.IdentificationData.Get.Replace("{id:Guid}", createdData.Id.ToString());
            var response = new IdentificationDataResponse
            {
                Id = createdData.Id,
                AppVersion = createdData.AppVersion,
                OperationSystem = createdData.OperationSystem,
                UserName = createdData.UserName
            };


            return Created(locationUri, response);

        }
        [HttpPut(ApiRoutes.IdentificationData.Update)]
        public IActionResult Put([FromRoute]Guid id, [FromBody] UpdateIdentificationDataRequest requestData)
        {
            if (requestData == null)
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
            identificationData.AppVersion = requestData.AppVersion;
            identificationData.OperationSystem = requestData.OperationSystem;
            identificationData.UserName = requestData.UserName;
            var updatedData = repository.Update(identificationData);
            
            logger.Debug($"A request to update data about {updatedData.ToString()}");
            return Ok(updatedData);
        }

        [HttpDelete(ApiRoutes.IdentificationData.Delete)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var identificationData = repository.GetById(id);
            if (identificationData == null)
            {
                return NotFound();
            }
            logger.Debug($"A request to delete data about {identificationData.ToString()}");
            repository.Delete(id);
            return Ok();
        }
    }
}
