using System;
using System.Collections.Generic;
using Microsoft.EgitimAPI.ApplicationCore.Services.AnswerService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.QuestionService.Dto
{
    public class QuestionDetail
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}