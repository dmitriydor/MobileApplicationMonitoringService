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
    public class EventController
    {
        private readonly IEventRepository repository;
        private static readonly ILogger logger = Log.ForContext<EventController>();
        private readonly IMapper mapper;
        public EventController(IEventRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet(ApiRoutes.Event.GetAllFor)]
        public async Task<IEnumerable<Event>> GetAllFor(Guid identificationId)
        {
            logger.Debug("");
            return await repository.GetAllForAsync(identificationId);
        }
        //[HttpPost(ApiRoutes.Event.Create)]
        //public async Task<IActionResult> Create([FromBody] CreateEventRequest createRequest)
        //{
        //    //TODO: вынести валидацию в фильтр

        //}
    }
}
