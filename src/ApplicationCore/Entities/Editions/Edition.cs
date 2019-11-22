using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Editions
{
    public class Edition:BaseEntity,IAggregateRoot
    {
        public int CourseCount { get; set; }
        public int EventCount { get; set; }
        public bool LiveSupport { get; set; }
        public float Price { get; set; }
    }
}