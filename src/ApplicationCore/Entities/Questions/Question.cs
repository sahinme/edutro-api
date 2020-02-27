using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Answers;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Questions
{
    public class Question:BaseEntity,IAggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public  ICollection<Answer> Answers { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }    
    }
}