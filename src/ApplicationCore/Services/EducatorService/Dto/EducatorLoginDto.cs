using Microsoft.EgitimAPI.ApplicationCore.Entities;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService.Dto
{
    public class EducatorLoginDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public EntityType EntityType { get; set; }
    }
}