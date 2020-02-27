using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Locations;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.LocationService.Dto;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.LocationService
{
    public class LocationService:ILocationService
    {
        private readonly IAsyncRepository<Location> _locationRepository;

        public LocationService(IAsyncRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public List<LocationDto> GetAllLocations()
        {
            var locations = _locationRepository.GetAll().Select(x => new LocationDto
            {
                Id = x.Id,
                LocationName = x.Name
            }).ToList();

            return locations;
        }
    }
}