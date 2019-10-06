using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Courses
{
    public class FavoriteCourse:BaseEntity,IAggregateRoot
    {
        public long UserId { get; set; }
        
        public long CourseId { get; set; }
        
        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}