namespace Microsoft.EgitimAPI.ApplicationCore.Services.CommentService.Dto
{
    public class CreateCommentDto
    {
        public long EntityId { get; set; }
        public string EntityName { get; set; }
        public string Content { get; set; }
        public long CommentatorId { get; set; }
    }
}