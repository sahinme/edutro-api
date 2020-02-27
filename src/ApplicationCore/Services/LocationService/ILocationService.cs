using System.Collections.Generic;
using Microsoft.EgitimAPI.ApplicationCore.Services.LocationService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.LocationService
{
    public interface ILocationService
    {
        List<LocationDto> GetAllLocations();
    }
}