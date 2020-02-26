using System;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.Question.Dto
{
    public class QuestionDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}   