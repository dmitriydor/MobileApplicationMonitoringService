using Microsoft.AspNetCore.Mvc;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Services;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : Controller
    {
        private static readonly ILogger logger = Log.ForContext<EventController>();
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet("descriptions")]
        public async Task<List<EventDescription>> GetAllEventDescriptions()
        {
            logger.Debug("There was a request to receive all event-descriptions data");
            return await eventService.GetAllEventsAsync();
        }
        [HttpPost("descriptions")]
        public async Task AddEventDescriptions(List<EventDescription> descriptions)
        {
            logger.Debug("A request to create data about {@EventDescriptions}", descriptions);
            foreach (var description in descriptions)
            {
                await eventService.UpdateEventAsync(description);
            }
        }
    }
}
