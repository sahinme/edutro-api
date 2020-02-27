using Microsoft.EgitimAPI.ApplicationCore.Entities;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.QuestionService.Dto
{
    public class CreateQuestionDto
    {
           public string Title { get; set; }
           public string Description { get; set; }
           public long UserId { get; set; }
           public EntityType EntityType { get; set; }
           public long EntityId { get; set; }
    }
}