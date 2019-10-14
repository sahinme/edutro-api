using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Services.EventService;
using Microsoft.EgitimAPI.ApplicationCore.Services.EventService.Dto;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class EventController:BaseApiController
    {
        private readonly IEventAppService _eventAppService;

        public EventController(IEventAppService eventAppService)
        {
            _eventAppService = eventAppService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventDto input)
        {
            try
            {
                var eventModel =  await _eventAppService.CreateEvent(input);
                return Ok(eventModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetEventsByName(string eventName)
        {
            var events =  await _eventAppService.GetEventsByName(eventName);
            return Ok(events);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetEventsByCategory(long categoryId)
        {
            var events = await _eventAppService.GetEventsByCategory(categoryId);
            return Ok(events);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventAppService.GetAllEvents();
            return Ok(events);
        }
        
        [HttpDelete]
        public async Task DeleteEvent(long eventId)
        {
            try
            {
                await _eventAppService.DeleteEvents(eventId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut]
        public async Task UpdateEvent(EventDto input)
        {
//            try
//            {
//                await _eventAppService.UpdateCourse(input);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                throw;
//            }
        }
    }
}