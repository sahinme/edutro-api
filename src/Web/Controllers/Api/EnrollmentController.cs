using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Services.EnrollmentService;
using Microsoft.EgitimAPI.ApplicationCore.Services.EnrollmentService.Dto;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class EnrollmentController:BaseApiController
    {
        private readonly IEnrollmentAppService _enrollmentAppService;

        public EnrollmentController(IEnrollmentAppService enrollmentAppService)
        {
            _enrollmentAppService = enrollmentAppService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateEnrollment(CreateEnrollmentDto input)
        {
            var result = await _enrollmentAppService.CreateEnrollment(input);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEntityEnrollments(EntityType entityType, long entityId)
        {
            var result = await _enrollmentAppService.GetEntityEnrollments(entityType, entityId);
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetEnrollmentById( long entityId)
        {
            var result = await _enrollmentAppService.GetEnrollmentById(entityId);
            return Ok(result);
        }
    }
}