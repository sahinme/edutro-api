using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.ApplicationCore.Entities.Courses
{
    public class CourseContent:BaseEntity,IAggregateRoot
    {
        // Video veya gorsel olabilir.
        public string ContentPath { get; set; }
        
        // video, pdf, image
        public ContentType ContentType { get; set; }
    }
}