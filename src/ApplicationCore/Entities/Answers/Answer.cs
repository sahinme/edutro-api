using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Questions;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Answers
{
    public class Answer:BaseEntity,IAggregateRoot
    {
        public string Description { get; set; }
        public long QuestionId { get; set; }
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        
        [ForeignKey(nameof(QuestionId))]
        public virtual Question Question { get; set; } 
    }
}