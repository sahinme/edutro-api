using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Services.LocationService;
using Microsoft.EgitimAPI.ApplicationCore.Services.LocationService.Dto;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class LocationController:BaseApiController
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        
        [HttpGet]
        public  List<LocationDto> GetAllLocations()
        {
               return  _locationService.GetAllLocations();
          
        }
    }
}