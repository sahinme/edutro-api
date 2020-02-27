using System;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.QuestionService.Dto
{
    public class QuestionDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}   