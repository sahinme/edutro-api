using Microsoft.AspNetCore.Http;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.PostService.Dto
{
    public class UpdatePost
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public long CategoryId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}