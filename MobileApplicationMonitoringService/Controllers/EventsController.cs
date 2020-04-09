using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Repositories;
using MobileApplicationMonitoringService.Contracts;
using MobileApplicationMonitoringService.Contracts.Requests;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Controllers
{
    [ApiController]
    public class EventsController:Controller
    {
        private readonly IApplicationEventRepository repository;
        private static readonly ILogger logger = Log.ForContext<EventsController>();
        private readonly IMapper mapper;
        public EventsController(IApplicationEventRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet(ApiRoutes.ApplicationEvent.GetAllFor)]
        public async Task<IEnumerable<ApplicationEvent>> GetAllFor(Guid identificationId)
        {
            logger.Debug("There was a request to receive all events");
            return await repository.GetAllForAsync(identificationId);
        }
        //[HttpPost(ApiRoutes.Event.Create)]
        //public async Task<IActionResult> Create([FromBody] CreateEventRequest createRequest)
        //{
        //    //TODO: вынести валидацию в фильтр

        //}
    }
}
