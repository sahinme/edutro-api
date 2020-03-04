using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Categories;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Comments;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.PostComments;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Posts
{
    public class Post:BaseEntity,IAggregateRoot
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string ImagePath { get; set; }
        public long? EducatorId { get; set; }
        public long? TenantId { get; set; }
        public long CategoryId { get; set; }
        
        [DefaultValue(10)]
        public PostState PostState { get; set; }
        public EntityType EntityType { get; set; }
        
        [ForeignKey(nameof(EducatorId))]
        public Educator Educator { get; set; }
        
        [ForeignKey(nameof(TenantId))]
        public Tenant Tenant { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
        
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }   
        
    }
}