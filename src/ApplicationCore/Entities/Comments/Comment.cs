using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Comments
{
    public class Comment:BaseEntity,IAggregateRoot
    {
           // tenant, user,  vs vs...
           public string EntityName { get; set; }
           public long EntityId { get; set; }
           public string Content { get; set; }
           public long UserId { get; set; }
           
           [ForeignKey(nameof(UserId))]
           public virtual User User { get; set; }        
           public Educator Educator { get; set; }
           public Tenant Tenant { get; set; }
           public Course Course { get; set; }
           
           
    }
}