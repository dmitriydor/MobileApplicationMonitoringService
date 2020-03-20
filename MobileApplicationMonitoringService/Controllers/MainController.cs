using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Repositories;
using Serilog;

namespace MobileApplicationMonitoringService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MainController:Controller
    {
        private ILogger logger;
        private readonly IIdentificationDataRepository repository;

        public MainController(ILogger logger, IIdentificationDataRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<IdentificationData> Get()
        {
            return repository.DataRepository;
        }

        [HttpGet("Id")]
        public IdentificationData Get(long id)
        {
            return repository.DataRepository.First(x => x.Id == id);
        }

        [HttpPost]
        public void Post(IdentificationData data)
        {
            repository.Create(data);
        }
        [HttpPut]
        public void Put(IdentificationData data)
        {
            repository.Update(data);
        }

        [HttpDelete]
        public void Delete(IdentificationData data)
        {
            repository.Delete(data);
        }
    }
}
