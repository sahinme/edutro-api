using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Events;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.EventService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EventService
{
    public class EventAppService:IEventAppService
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<GivenEvent> _givenEventRepository;

        public EventAppService(IAsyncRepository<Event> eventRepository,
                IAsyncRepository<GivenEvent> givenEventRepository
            )
        {
            _eventRepository = eventRepository;
            _givenEventRepository = givenEventRepository;
        }
        
        public async Task<Event> CreateEvent(CreateEventDto input)
        {
            var model = new Event
            {
                Title = input.Title,
                Description = input.Description,
                Quota = input.Quota,
                Price = input.Price,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                CategoryId = input.CategoryId,
                Address = input.Address,
                LocationId = input.LocationId,
                EventType = input.EventType,
                OwnerId = input.OwnerId,
                OwnerType = input.OwnerType
            };

            await _eventRepository.AddAsync(model);
            
            var count = (input.TenantId.Length > input.EducatorId.Length)
                ? input.TenantId.Length
                : input.EducatorId.Length;
            
            for (var i = 0; i <count; i++)
            {
                var givenEvent = new GivenEvent
                {
                    EventId = model.Id,
                };
                if (input.TenantId.Length > i)
                {
                    givenEvent.TenantId = input.TenantId[i];
                }

                if (input.EducatorId.Length > i)
                {
                    givenEvent.EducatorId = input.EducatorId[i];
                }
                await _givenEventRepository.AddAsync(givenEvent);
            }
            
            return model;
        }

        public async Task<List<EventDto>> GetEventsByName(string courseName)
        {
            var events = await _eventRepository.GetAll().Include(x=>x.Category)
                .Include(x=>x.Location)
                .Include(x => x.Owners).ThenInclude(x => x.Tenant)
                .Include(x=>x.Owners).ThenInclude(x=>x.Educator)
                .Where(x => x.Title.Contains(courseName) || x.Description.Contains(courseName))
                .Select(x => new EventDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Quota = x.Quota,
                    Price = x.Price,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Address = x.Address,
                    EventType = x.EventType,
                    LocationName = x.Location.Name,
                    OwnerType = x.OwnerType,
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        Description = x.Category.Description,
                        DisplayName = x.Category.DisplayName,
                        ParentCategory = new ParentCategoryDto()
                    },
                    Tenants = x.Owners.Select(t => new EventTenantDto
                    {
                        TenantId = t.Tenant.Id,
                        TenantName = t.Tenant.TenantName,
                        LogoPath = t.Tenant.LogoPath
                    }).ToList(),
                    Educators = x.Owners.Select(e=>new EventEducatorDto
                    {
                        EducatorId = e.Educator.Id,
                        EducatorName=e.Educator.Name,
                        Profession = e.Educator.Profession,
                        ProfileImgPath = e.Educator.ProfileImagePath
                    }).ToList(),
                    OwnerInfo = x.OwnerType=="Tenant" ? x.Owners.Where(p=>p.Tenant.Id==x.OwnerId).Select(p=>new OwnerInfo
                    {
                        Id = p.Tenant.Id,
                        Name = p.Tenant.TenantName,
                        Profession = null
                    }).ToList() : 
                         x.Owners.Where(a=>a.Educator.Id==x.OwnerId).Select(a=>new OwnerInfo
                        {
                            Id = a.Educator.Id,
                            Name = a.Educator.Name + " "+ a.Educator.Surname,
                            Profession = a.Educator.Profession
                        }).ToList()
                }).ToListAsync();
            return events;
        }

        public async Task<List<EventDto>> GetEventsByCategory(long categoryId)
        {
            var events = await _eventRepository.GetAll().Include(x=>x.Category)
                .Include(x=>x.Location)
                .Include(x => x.Owners).ThenInclude(x => x.Tenant)
                .Include(x=>x.Owners).ThenInclude(x=>x.Educator)
                .Where(x => x.CategoryId==categoryId)
                .Select(x => new EventDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Quota = x.Quota,
                    Price = x.Price,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Address = x.Address,
                    EventType = x.EventType,
                    LocationName = x.Location.Name,
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        Description = x.Category.Description,
                        DisplayName = x.Category.DisplayName,
                        ParentCategory = new ParentCategoryDto()
                    },
                    Tenants = x.Owners.Select(t => new EventTenantDto
                    {
                        TenantId = t.Tenant.Id,
                        TenantName = t.Tenant.TenantName,
                        LogoPath = t.Tenant.LogoPath
                    }).ToList(),
                    Educators = x.Owners.Select(e=>new EventEducatorDto
                    {
                        EducatorId = e.Educator.Id,
                        EducatorName=e.Educator.Name,
                        Profession = e.Educator.Profession,
                        ProfileImgPath = e.Educator.ProfileImagePath
                    }).ToList(),
                    OwnerInfo = x.OwnerType=="Tenant" ? x.Owners.Where(p=>p.Tenant.Id==x.OwnerId).Select(p=>new OwnerInfo
                        {
                            Id = p.Tenant.Id,
                            Name = p.Tenant.TenantName,
                            Profession = null
                        }).ToList() : 
                        x.Owners.Where(a=>a.Educator.Id==x.OwnerId).Select(a=>new OwnerInfo
                        {
                            Id = a.Educator.Id,
                            Name = a.Educator.Name + " "+ a.Educator.Surname,
                            Profession = a.Educator.Profession
                        }).ToList()
                }).ToListAsync();
            return events;
        }

        public async Task DeleteEvents(long id)
        {
            var eventData = await _eventRepository.GetByIdAsync(id);
            eventData.IsDeleted = true;
            await _eventRepository.UpdateAsync(eventData);
        }

        public async Task<List<EventDto>> GetAllEvents()
        {
            var events = await _eventRepository.GetAll().Include(x=>x.Category)
                .Include(x=>x.Location)
                .Include(x => x.Owners).ThenInclude(x => x.Tenant)
                .Include(x=>x.Owners).ThenInclude(x=>x.Educator)
                .Select(x => new EventDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Quota = x.Quota,
                    Price = x.Price,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Address = x.Address,
                    EventType = x.EventType,
                    LocationName = x.Location.Name,
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        Description = x.Category.Description,
                        DisplayName = x.Category.DisplayName,
                        ParentCategory = new ParentCategoryDto()
                    },
                    Tenants = x.Owners.Select(t => new EventTenantDto
                    {
                        TenantId = t.Tenant.Id,
                        TenantName = t.Tenant.TenantName,
                        LogoPath = t.Tenant.LogoPath
                    }).ToList(),
                    Educators = x.Owners.Select(e=>new EventEducatorDto
                    {
                        EducatorId = e.Educator.Id,
                        EducatorName=e.Educator.Name,
                        Profession = e.Educator.Profession,
                        ProfileImgPath = e.Educator.ProfileImagePath
                    }).ToList(),
                    OwnerInfo = x.OwnerType=="Tenant" ? x.Owners.Where(p=>p.Tenant.Id==x.OwnerId).Select(p=>new OwnerInfo
                        {
                            Id = p.Tenant.Id,
                            Name = p.Tenant.TenantName,
                            Profession = null,
                            LogoPath = p.Tenant.LogoPath
                        }).ToList() : 
                        x.Owners.Where(a=>a.Educator.Id==x.OwnerId).Select(a=>new OwnerInfo
                        {
                            Id = a.Educator.Id,
                            Name = a.Educator.Name + " "+ a.Educator.Surname,
                            Profession = a.Educator.Profession,
                            LogoPath = a.Educator.ProfileImagePath
                        }).ToList()
                }).ToListAsync();
            return events;
        }
    }
}