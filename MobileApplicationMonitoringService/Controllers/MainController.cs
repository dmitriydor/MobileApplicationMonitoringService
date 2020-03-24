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
        public IActionResult Post([FromBody] CreateIdentificationDataRequest createData)
        {
            if (createData == null)
            {
                return BadRequest("Data object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var identificationData = new IdentificationData
            {
                UserName = createData.UserName,
                AppVersion = createData.AppVersion,
                OperationSystem = createData.OperationSystem
            };
            var data = repository.Create(identificationData);
            logger.Debug($"A request to create data about {data.ToString()}");

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.IdentificationData.Get.Replace("{id:Guid}", data.Id.ToString());
            var response = new IdentificationDataResponse
            {
                Id = data.Id,
                AppVersion = data.AppVersion,
                OperationSystem = data.OperationSystem,
                UserName = data.UserName
            };


            return Created(locationUri, response);

        }
        [HttpPut(ApiRoutes.IdentificationData.Update)]
        public IActionResult Put([FromRoute]Guid id, [FromBody] IdentificationData data)
        {
            if (data == null)
            {
                return BadRequest("Data object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var identificationData = repository.Update(id, data);
            if (identificationData == null)
            {
                return NotFound();
            }
            logger.Debug($"A request to update data about {identificationData.ToString()}");
            return Ok(identificationData);
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
