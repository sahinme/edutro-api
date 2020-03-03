using System.Collections.Generic;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Comments;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Posts
{
    public class Post:BaseEntity,IAggregateRoot
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public long EntityId { get; set; }
        public PostState PostState { get; set; }
        public EntityType EntityType { get; set; }
        
        public Educator Educator { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}