using System;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CommentService.Dto
{
    public class CommentDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public CommentatorDto Commentator { get; set; }
    }

    public class CommentatorDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
    }
}