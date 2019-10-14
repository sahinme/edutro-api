using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Events;
using Microsoft.EgitimAPI.ApplicationCore.Services.EventService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EventService
{
    public interface IEventAppService
    {
        Task<Event> CreateEvent(CreateEventDto input);
        Task<List<EventDto>> GetEventsByName(string courseName);
        Task<List<EventDto>> GetEventsByCategory(long categoryId);
        Task DeleteEvents(long id);
        //Task UpdateCourse(UpdateCourseDto course);
        Task<List<EventDto>> GetAllEvents();
    }
}