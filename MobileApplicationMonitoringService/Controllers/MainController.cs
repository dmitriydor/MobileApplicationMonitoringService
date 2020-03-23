using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Repositories;
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

        [HttpGet]
        public IEnumerable<IdentificationData> Get()
        {
            logger.Debug("There was a request to receive all data");
            return repository.DataRepository;
        }

        [HttpGet("Id")]
        public IdentificationData Get(long id)
        {
            var item = repository.DataRepository.FirstOrDefault(x => x.Id == id);
            logger.Debug("A request for data about " + item?.ToString());
            return item;
        }

        [HttpPost]
        public IdentificationData Post([FromBody] IdentificationData data)
        {
            logger.Debug("A request to create data about" + data.ToString());
            return repository.Create(data);
        }
        [HttpPut]
        public IdentificationData Put([FromBody] IdentificationData data)
        {
            logger.Debug("A request to update data about" + data.ToString());
            return repository.Update(data);
        }

        [HttpDelete]
        public void Delete(IdentificationData data)
        {
            logger.Debug("A request to delete data about" + data.ToString());
            repository.Delete(data);
        }
    }
}
