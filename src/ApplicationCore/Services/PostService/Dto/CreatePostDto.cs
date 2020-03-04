using Microsoft.AspNetCore.Http;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Posts;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.PostService.Dto
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public long CategoryId { get; set; }
        public IFormFile ImageFile { get; set; }
        public long? EducatorId { get; set; }
        public long? TenantId { get; set; }
    }
}