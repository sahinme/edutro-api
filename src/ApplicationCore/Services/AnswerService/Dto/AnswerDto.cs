using System;
using Microsoft.EgitimAPI.ApplicationCore.Entities;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.AnswerService.Dto
{
    public class AnswerDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public EntityType EntityType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}