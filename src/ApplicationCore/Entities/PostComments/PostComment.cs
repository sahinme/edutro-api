using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Posts;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.PostComments
{
    public class PostComment:BaseEntity,IAggregateRoot
    {
        public string Content { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
           
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }     
        
        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
        
    }
}