using Microsoft.EgitimAPI.ApplicationCore.Entities;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.AnswerService.Dto
{
    public class CreateAnswerDto
    {
        public string Description { get; set; }
        public long QuestionId { get; set; }
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
      
    }
}