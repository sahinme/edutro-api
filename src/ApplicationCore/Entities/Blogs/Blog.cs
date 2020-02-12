using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Blogs
{
    public class Blog:BaseEntity,IAggregateRoot
    {
        public string Content { get; set; }
    }
}