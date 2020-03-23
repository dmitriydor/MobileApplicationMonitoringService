using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Repositories;
using MobileApplicationMonitoringService.Contracts;
using Serilog;
using Serilog.Events;

namespace MobileApplicationMonitoringService.Controllers
{
    [Route("[controller]")]
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
        public IActionResult Get(Guid id)
        {
            var identificationData = repository.GetById(id);
            //TODO: NotFound case
            logger.Debug($"A request for data about {identificationData?.ToString()}");
            return Ok(identificationData);
        }

        [HttpPost(ApiRoutes.IdentificationData.Create)]
        public IActionResult Post([FromBody] IdentificationData data)
        {
            if (data == null)
            {
                return BadRequest("Data object is null");
            }
            logger.Debug($"A request to create data about {data.ToString()}");
            var identificationData = repository.Create(data);
            return Ok(identificationData);

        }
        [HttpPut(ApiRoutes.IdentificationData.Update)]
        public IActionResult Put(Guid id, [FromBody] IdentificationData data)
        {
            if (data == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            repository.Update(id, data);
            logger.Debug($"A request to update data about {data.ToString()}");
            return Ok(data);
        }

        [HttpDelete(ApiRoutes.IdentificationData.Delete)]
        public void Delete(Guid id)
        {
            var identificationData = repository.GetById(id);
            logger.Debug($"A request to delete data about {identificationData.ToString()}");
            repository.Delete(id);
        }
    }
}
