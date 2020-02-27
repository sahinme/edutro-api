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
        public Question Question { get; set; }
        public Educator Educator { get; set; }
        public Tenant Tenant { get; set; }
        public User User { get; set; }
    }
}