using System;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.PostService.Dto
{
    public class PostCommentDto
    {
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}